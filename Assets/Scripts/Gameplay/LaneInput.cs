using UnityEngine;
using UnityEngine.InputSystem;

namespace PianoTiles
{
    public class LaneInput : MonoBehaviour
    {
        [SerializeField] ChartRunner chart;
        InputAction[] actions;

        void Awake()
        {
            string[] keys = { "a", "s", "d", "f" };
            actions = new InputAction[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                int lane = i;
                var action = new InputAction(keys[i], InputActionType.Button, $"<Keyboard>/{keys[i]}");
                action.performed += context => chart.Press(lane, context.time);
                actions[i] = action;
            }
        }

        void OnEnable() { foreach (var action in actions) action.Enable(); }
        void OnDisable() { foreach (var action in actions) action.Disable(); }
        void OnDestroy() { foreach (var action in actions) action.Dispose(); }
    }
}
