using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MinesweeperConfig", menuName = "Configs/MinesweeperConfig")]
    public class MinesweeperConfig : ScriptableObject
    {
        [field: SerializeField] public int FieldWidth { get; private set; } = 4;
        [field: SerializeField] public int FieldHeight { get; private set; } = 4;
        [field: SerializeField] public int MinesCount { get; private set; } = 8;

        private void OnValidate()
        {
            var maxMines = FieldWidth * FieldHeight - 1;
            MinesCount = Mathf.Clamp(MinesCount, 1, maxMines);
        }
    }
}