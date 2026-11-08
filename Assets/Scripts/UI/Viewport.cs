using UnityEngine;

namespace PianoTiles
{
    [RequireComponent(typeof(Camera))]
    public class Viewport : MonoBehaviour
    {
        Camera view;
        void Awake() => view = GetComponent<Camera>();
        void Update()
        {
            var target = view.targetTexture;
            float aspect = target != null ? (float)target.width / target.height : (float)Screen.width / Screen.height;
            float scale = aspect / (16f / 9);
            view.rect = scale < 1 ? new Rect(0, (1 - scale) / 2, 1, scale) : new Rect((1 - 1 / scale) / 2, 0, 1 / scale, 1);
        }
    }
}
