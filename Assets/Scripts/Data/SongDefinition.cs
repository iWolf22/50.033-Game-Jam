using System;
using System.Collections.Generic;
using UnityEngine;

namespace PianoTiles
{
    [CreateAssetMenu(menuName = "Piano Tiles/Song")]
    public class SongDefinition : ScriptableObject
    {
        public string songId = "rick-astley";
        public string title = "RICK ASTLEY";
        public int chartVersion = 2;
        public AudioClip audioClip;
        [Min(1)] public float duration = 60;
        public double bpm = 113.630956838;
        [Range(1, 4)] public int subdivision = 1;
        [Min(.1f)] public float preview = 2.2666667f;
        [Min(1)] public float scrollSpeed = 300;
        public List<NoteData> notes = new();
        public double Step => 60 / bpm / subdivision;
        public double FirstNote => Math.Ceiling(preview / Step) * Step;

        public List<NoteData> CreateChart(int seed)
        {
            if (notes.Count > 0) return new List<NoteData>(notes);
            var random = new System.Random(seed);
            var chart = new List<NoteData>();
            int lane = random.Next(4);
            for (int i = 0; FirstNote + (i + 1) * Step <= duration; i++)
            {
                double start = FirstNote + i * Step;
                lane = (lane + 1 + random.Next(3)) % 4;
                chart.Add(new NoteData(lane, start, Step));
            }
            return chart;
        }
    }

    [Serializable]
    public struct NoteData
    {
        public int lane;
        public double start;
        public double duration;
        public string lyric;
        public double End => start + duration;

        public NoteData(int lane, double start, double duration)
        {
            this.lane = lane;
            this.start = start;
            this.duration = duration;
            lyric = "";
        }
    }
}
