using System;
using UnityEngine;

namespace PianoTiles
{
    [DefaultExecutionOrder(-90)]
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] GameManager game;
        [SerializeField] ChartRunner chart;
        public int Score { get; private set; }
        public event Action Changed;

        void OnEnable()
        {
            chart.Judged += OnJudged;
            game.StateChanged += OnStateChanged;
        }

        void OnDisable()
        {
            chart.Judged -= OnJudged;
            game.StateChanged -= OnStateChanged;
        }

        public string BestKey => $"best.{chart.PrimarySong.songId}.{chart.PrimarySong.chartVersion}.Easy";
        public int Best() => PlayerPrefs.GetInt(BestKey, 0);

        void OnJudged(int lane, Judgement result)
        {
            Score += result == Judgement.Hit ? 100 : 0;
            Changed?.Invoke();
        }

        void OnStateChanged(GameState state)
        {
            if (state == GameState.Playing && !chart.IsRemix) Score = 0;
            if (state == GameState.Results)
            {
                PlayerPrefs.SetInt(BestKey, Mathf.Max(Score, Best()));
                PlayerPrefs.Save();
            }
            Changed?.Invoke();
        }
    }
}
