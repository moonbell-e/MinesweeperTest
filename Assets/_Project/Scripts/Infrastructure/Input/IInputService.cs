using UnityEngine;

namespace Core.Input
{
    public interface IInputService
    {
        bool IsLeftMouseButtonClicked();
        bool IsRightMouseButtonClicked();
        bool IsRestartKeyPressed();

        Vector2Int GetMousePosition();
    }
}