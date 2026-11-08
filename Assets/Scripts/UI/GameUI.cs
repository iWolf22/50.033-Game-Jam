using UnityEngine;
using UnityEngine.UI;

namespace PianoTiles
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] GameManager game;
        [SerializeField] ScoreManager score;
        [SerializeField] ChartRunner chart;
        [SerializeField] GameObject home, playfield, results;
        [SerializeField] Text scoreText, songText, timer, resultTitle, resultScore, bestText;
        [SerializeField] Text homeBest;
        [SerializeField] Image[] hearts;
        [SerializeField] LaneView[] lanes;
        float returnAt;

        public void Play() => game.Play();
        public void PlayGodMode() => game.PlayGodMode();

        void OnEnable()
        {
            game.StateChanged += ShowState;
            game.HeartsChanged += ShowHearts;
            score.Changed += ShowScore;
            chart.Judged += ShowJudgement;
        }

        void Start() { ShowState(game.State); ShowScore(); }

        void OnDisable()
        {
            game.StateChanged -= ShowState;
            game.HeartsChanged -= ShowHearts;
            score.Changed -= ShowScore;
            chart.Judged -= ShowJudgement;
        }

        void ShowState(GameState state)
        {
            home.SetActive(state == GameState.Home);
            playfield.SetActive(state == GameState.Playing);
            results.SetActive(state == GameState.Results || state == GameState.Remix);
            if (state == GameState.Home)
                homeBest.text = $"BEST  {score.Best():N0}";
            if (state != GameState.Results && state != GameState.Remix) return;
            resultTitle.text = state == GameState.Remix ? "REMIX TIME" : game.Completed ? "YOU WIN" : "GAME OVER";
            resultScore.text = score.Score.ToString("N0");
            bestText.text = $"BEST  {score.Best():N0}";
            returnAt = UnityEngine.Time.unscaledTime + 2.5f;
        }

        void ShowScore()
        {
            scoreText.text = score.Score.ToString("N0");
        }

        void ShowHearts(int count)
        {
            for (int i = 0; i < hearts.Length; i++) hearts[i].color = i < count ? Color.black : new Color(0, 0, 0, 0.12f);
        }

        void ShowJudgement(int lane, Judgement judgement)
        {
            if (game.State == GameState.Playing) lanes[lane].Show(judgement);
        }

        void Update()
        {
            if (game.State == GameState.Playing)
            {
                songText.text = chart.Song.title;
                int elapsed = Mathf.Clamp((int)chart.Time, 0, (int)chart.Song.duration);
                int total = (int)chart.Song.duration;
                timer.text = $"{elapsed / 60}:{elapsed % 60:00} / {total / 60}:{total % 60:00}";
            }
            if (UnityEngine.Time.unscaledTime < returnAt) return;
            if (game.State == GameState.Remix) game.ContinueRemix();
            else if (game.State == GameState.Results) game.ReturnHome();
        }
    }
}
