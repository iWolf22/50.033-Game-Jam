using UnityEngine;
using UnityEngine.UI;

namespace PianoTiles
{
    public class LaneView : MonoBehaviour
    {
        [SerializeField] Image flash;
        [SerializeField] Text key;
        [SerializeField] Text feedback;
        float remaining;
        public void SetKey(string label) => key.text = label;

        public void Show(Judgement judgement)
        {
            feedback.text = judgement.ToString().ToUpperInvariant();
            flash.color = new Color(0, 0, 0, judgement == Judgement.Miss ? 0.18f : 0.05f);
            remaining = 0.45f;
        }

        void Update()
        {
            if (remaining <= 0) return;
            remaining -= UnityEngine.Time.unscaledDeltaTime;
            if (remaining > 0) return;
            flash.color = Color.clear;
            feedback.text = "";
        }

        void OnDisable()
        {
            remaining = 0;
            flash.color = Color.clear;
            feedback.text = "";
        }
    }
}
