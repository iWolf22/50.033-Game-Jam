using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace PianoTiles
{
    public class ChartRunner : MonoBehaviour
    {
        [SerializeField] SongDefinition song, remix;
        [SerializeField] TileView tilePrefab;
        [SerializeField] RectTransform tileRoot;
        [SerializeField] float laneWidth = 120;
        [SerializeField] float judgementY = 80;
        readonly List<TileView> pool = new();
        readonly Dictionary<NoteJudge, TileView> visible = new();
        List<NoteJudge> primaryNotes, remixNotes, notes = new();
        double startedAt, inputOffset;
        bool running;
        public SongDefinition Song { get; private set; }
        public SongDefinition PrimarySong => song;
        public bool HasRemix => remix != null;
        public bool IsRemix { get; private set; }
        public double Time => AudioSettings.dspTime - startedAt;
        public double StartedAt => startedAt;
        public int PoolSize => pool.Count;
        public IReadOnlyList<NoteJudge> Notes => notes;
        public event Action<int, Judgement> Judged;
        public event Action<SongDefinition, double> SectionStarted;
        public event Action SectionEnded, Stopped;

        void Awake() => Song = song;

        public void Begin(int seed)
        {
            Stop();
            primaryNotes = Prepare(song, seed);
            remixNotes = HasRemix ? Prepare(remix, seed + 1) : new();
            while (pool.Count < 10)
            {
                var view = Instantiate(tilePrefab, tileRoot);
                view.gameObject.SetActive(false);
                pool.Add(view);
            }
            StartSection(song, primaryNotes, false);
        }

        static List<NoteJudge> Prepare(SongDefinition track, int seed)
        {
            var prepared = new List<NoteJudge>();
            foreach (var note in track.CreateChart(seed)) prepared.Add(new NoteJudge(note));
            return prepared;
        }

        public void BeginRemix() => StartSection(remix, remixNotes, true);

        void StartSection(SongDefinition track, List<NoteJudge> prepared, bool isRemix)
        {
            ClearTiles();
            Song = track;
            notes = prepared;
            IsRemix = isRemix;
            startedAt = AudioSettings.dspTime + .1;
            inputOffset = AudioSettings.dspTime - InputState.currentTime;
            running = true;
            SectionStarted?.Invoke(track, startedAt);
        }

        void ClearTiles()
        {
            foreach (var view in pool) view.gameObject.SetActive(false);
            visible.Clear();
        }

        public void Stop()
        {
            running = false;
            ClearTiles();
            Stopped?.Invoke();
        }

        public void Press(int lane, double inputTime)
        {
            double time = inputTime + inputOffset - startedAt;
            if (!running || time < 0) return;
            foreach (var note in notes)
                if (note.Data.lane == lane && note.Press(time))
                {
                    Resolve(note, Judgement.Hit);
                    return;
                }
            Judged?.Invoke(lane, Judgement.Miss);
        }

        void Update()
        {
            if (!running) return;
            double time = Time;
            double deadline = time >= Song.duration ? double.PositiveInfinity : time;
            foreach (var note in notes)
            {
                if (!running) break;
                if (note.Expire(deadline)) Resolve(note, Judgement.Miss);
                if (!running || note.Resolved || note.Data.start - time > Song.preview) continue;
                if (!visible.TryGetValue(note, out var view))
                {
                    view = pool.Find(tile => !tile.gameObject.activeSelf);
                    if (view == null) throw new InvalidOperationException("Tile pool capacity exceeded.");
                    visible.Add(note, view);
                    view.gameObject.SetActive(true);
                }
                view.Draw(note.Data, time, laneWidth, judgementY, Song.scrollSpeed);
            }
            if (!running || time < Song.duration) return;
            running = false;
            SectionEnded?.Invoke();
        }

        void Resolve(NoteJudge note, Judgement result)
        {
            if (visible.Remove(note, out var view)) view.gameObject.SetActive(false);
            Judged?.Invoke(note.Data.lane, result);
        }
    }
}
