using System;
using Data;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Minefield
    {
        private readonly ICellFactory _cellFactory;
        private readonly MineGenerator _mineGenerator;
        private Cell[,] _field;
        
        public int Width { get; }
        public int Height { get; }

        public bool IsGameOver { get; private set; }
        public bool IsWin { get; private set; }
        private bool IsFirstClickDone { get; set; }

        [Inject]
        public Minefield(ICellFactory cellFactory, MineGenerator mineGenerator, MinesweeperConfig minesweeperConfig)
        {
            _cellFactory = cellFactory;
            _mineGenerator = mineGenerator;

            Width = minesweeperConfig.FieldWidth;
            Height = minesweeperConfig.FieldHeight;
        }

        public void CreateField()
        {
            _field = new Cell[Width, Height];

            ForEachCell((x, y) =>
            {
                var position = new Vector3Int(x, y, 0);
                _field[x, y] = _cellFactory.CreateEmptyCell(position);
            });

            ResetGameState();
        }

        public void ResetGameState()
        {
            IsGameOver = false;
            IsWin = false;
            IsFirstClickDone = false;
        }

        public void RevealCell(int x, int y)
        {
            if (!IsValidPosition(x, y))
                return;

            if (!IsFirstClickDone) 
                HandleFirstClick(x, y);

            if (_field[x, y].IsRevealed || _field[x, y].IsFlagged)
                return;

            ProcessCellReveal(x, y);
            CheckWinCondition();
        }
        
        public void RevealAllMines(bool isWin)
        {
            ForEachCell((x, y) =>
            {
                if (IsCellMine(x, y))
                {
                    if (isWin)
                        _field[x, y].ToggleFlag();
                    else
                        _field[x, y].Reveal();
                }
            });
        }
        
        public void ToggleFlag(int x, int y)
        {
            if (!IsValidPosition(x, y) || _field[x, y].IsRevealed)
                return;

            _field[x, y].ToggleFlag();
        }
        
        public Cell GetCell(int x, int y)
        {
            return _field[x, y];
        }

        public bool IsOutOfBounds(Vector2Int position)
        {
            return position.x < 0 || position.x >= Width || position.y < 0 || position.y >= Height;
        }

        private void HandleFirstClick(int x, int y)
        {
            _mineGenerator.GenerateMines(_field, x, y);
            _mineGenerator.SetNeighboringMines(_field);
            IsFirstClickDone = true;
        }

        private void ProcessCellReveal(int x, int y)
        {
            _field[x, y].Reveal();

            if (IsCellMine(x, y))
            {
                _field[x, y].Explode();
                IsGameOver = true;
                return;
            }

            if (IsCellEmpty(x, y) && _field[x, y].MinesNumber == 0) 
                RevealNeighbors(x, y);
        }

        private void RevealNeighbors(int x, int y)
        {
            for (var dx = -1; dx <= 1; dx++)
                for (var dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    var nx = x + dx;
                    var ny = y + dy;

                    if (nx >= 0 && nx < Width && ny >= 0 && ny < Height) 
                        RevealCell(nx, ny);
                }
        }

        private void CheckWinCondition()
        {
            var revealedCells = 0;
            var totalCells = 0;

            ForEachCell((x, y) =>
            {
                if (_field[x, y].CellType != CellType.Mine)
                    totalCells++;
                if (_field[x, y].IsRevealed)
                    revealedCells++;
            });

            if (revealedCells == totalCells) 
                IsWin = true;
        }

        private void ForEachCell(Action<int, int> action)
        {
            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                    action(x, y);
        }
        
        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private bool IsCellEmpty(int x, int y)
        {
            return _field[x, y].CellType == CellType.Empty;
        }

        private bool IsCellMine(int x, int y)
        {
            return _field[x, y].CellType == CellType.Mine;
        }
    }
}