using System;
using UnityEngine;

namespace PianoTiles
{
    public enum GameState { Home, Playing, Remix, Results }

    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] ChartRunner chart;
        public GameState State { get; private set; }
        public int Hearts { get; private set; } = 3;
        public bool GodMode { get; private set; }
        public bool Completed { get; private set; }
        public event Action<GameState> StateChanged;
        public event Action<int> HeartsChanged;

        void Awake() => Instance = this;
        void OnEnable()
        {
            chart.Judged += OnJudged;
            chart.SectionEnded += OnSectionEnded;
        }

        void OnDisable()
        {
            chart.Judged -= OnJudged;
            chart.SectionEnded -= OnSectionEnded;
        }

        void OnSectionEnded()
        {
            if (!chart.IsRemix && chart.HasRemix && Hearts == 3) SetState(GameState.Remix);
            else Finish(true);
        }
        void OnDestroy() { if (Instance == this) Instance = null; }

        public void Play() => StartRun(false);
        public void PlayGodMode() => StartRun(true);

        void StartRun(bool godMode)
        {
            if (State != GameState.Home) return;
            GodMode = godMode;
            Hearts = 3;
            Completed = false;
            chart.Begin(Environment.TickCount);
            SetState(GameState.Playing);
            HeartsChanged?.Invoke(Hearts);
        }

        void OnJudged(int lane, Judgement result)
        {
            if (GodMode || result != Judgement.Miss || State != GameState.Playing) return;
            HeartsChanged?.Invoke(--Hearts);
            if (Hearts == 0) Finish(false);
        }

        public void ContinueRemix()
        {
            if (State != GameState.Remix) return;
            chart.BeginRemix();
            SetState(GameState.Playing);
        }

        public void Finish(bool completed)
        {
            if (State != GameState.Playing) return;
            Completed = completed;
            chart.Stop();
            SetState(GameState.Results);
        }

        public void ReturnHome()
        {
            if (State == GameState.Results) SetState(GameState.Home);
        }

        void SetState(GameState state)
        {
            State = state;
            StateChanged?.Invoke(state);
        }
    }
}
