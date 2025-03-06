using UnityEngine;

namespace Core.Input
{
    public class InputService : IInputService
    {
        public bool IsLeftMouseButtonClicked()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }

        public bool IsRightMouseButtonClicked()
        {
            return UnityEngine.Input.GetMouseButtonDown(1);
        }

        public bool IsRestartKeyPressed()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.R);
        }

        public Vector2Int GetMousePosition()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            return Vector2Int.FloorToInt(worldPosition);
        }
    }
}