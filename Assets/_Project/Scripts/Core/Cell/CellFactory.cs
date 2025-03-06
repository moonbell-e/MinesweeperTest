using UnityEngine;

namespace Core
{
    public class CellFactory: ICellFactory
    {
        public Cell CreateEmptyCell(Vector3Int position)
        {
            var cell = new Cell(CellType.Empty);
            cell.SetPosition(position);
            return cell;
        }

        public Cell CreateMineCell(Vector3Int position)
        {
            var cell = new Cell(CellType.Mine);
            cell.SetPosition(position);
            return cell;
        }

        public Cell CreateNumberCell(Vector3Int position, int neighboringMines)
        {
            var cell = new Cell(CellType.Number);
            cell.SetPosition(position);
            cell.SetNeighboringMines(neighboringMines);
            return cell;
        }
    }
}