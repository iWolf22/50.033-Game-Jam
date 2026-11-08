using UnityEngine;
using UnityEngine.UI;

namespace PianoTiles
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] RectTransform body;
        [SerializeField] Text lyric;

        public void Draw(NoteData note, double time, float laneWidth, float line, float speed)
        {
            float y = line + (float)(note.start - time) * speed;
            body.anchoredPosition = new Vector2((note.lane - 1.5f) * laneWidth, y);
            body.sizeDelta = new Vector2(laneWidth, (float)(note.duration * speed));
            lyric.text = note.lyric;
        }
    }
}
