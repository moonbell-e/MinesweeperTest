using UnityEngine;

namespace Core
{
    public interface ICellFactory
    {
        Cell CreateEmptyCell(Vector3Int position);
        Cell CreateMineCell(Vector3Int position);
        Cell CreateNumberCell(Vector3Int position, int neighboringMines);
    }
}