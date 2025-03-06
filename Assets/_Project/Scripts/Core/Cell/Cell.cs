using UnityEngine;

namespace Core
{
    public class Cell
    {
        public CellType CellType { get; }
        public int MinesNumber { get; private set; }
        public bool IsRevealed { get; private set; }
        public bool IsFlagged { get; private set; }
        public bool IsExploded { get; private set; }
        public Vector3Int Position { get; private set; }
        
        public Cell(CellType cellType)
        {
            CellType = cellType;
            MinesNumber = 0;
            IsRevealed = false;
            IsFlagged = false;
        }

        public void Reveal()
        {
            IsRevealed = true;
        }

        public void ToggleFlag()
        {
            IsFlagged = !IsFlagged;
        }

        public void SetNeighboringMines(int count)
        {
            if (CellType != CellType.Mine) 
                MinesNumber = count;
        }

        public void SetPosition(Vector3Int pos)
        {
            Position = pos;
        }

        public void Explode()
        {
            IsExploded = true;
        }
    }

    public enum CellType
    {
        Empty,
        Mine,
        Number
    }
}