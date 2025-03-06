using Data;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Core
{
    public class MineGenerator
    {
        private readonly ICellFactory _cellFactory;
        private readonly int _height;
        private readonly int _minesCount;
        private readonly int _width;
        private readonly Random _random = new();

        [Inject]
        public MineGenerator(ICellFactory cellFactory, MinesweeperConfig minesweeperConfig)
        {
            _cellFactory = cellFactory;
            _width = minesweeperConfig.FieldWidth;
            _height = minesweeperConfig.FieldHeight;
            _minesCount = minesweeperConfig.MinesCount;
        }

        public void GenerateMines(Cell[,] field, int safeX, int safeY)
        {
            var width = field.GetLength(0);
            var height = field.GetLength(1);
            var placedMines = 0;

            while (placedMines < _minesCount)
            {
                var (x, y) = GenerateRandomPosition(_random, width, height);

                if (x == safeX && y == safeY)
                    continue;

                if (IsCellEmpty(field, x, y))
                {
                    PlaceMine(field, x, y);
                    placedMines++;
                }
            }
        }

        public void SetNeighboringMines(Cell[,] field)
        {
            for (var x = 0; x < _width; x++)
                for (var y = 0; y < _height; y++)
                {
                    if (field[x, y].CellType == CellType.Mine) continue;

                    var neighboringMines = CountNeighboringMines(field, x, y);
                    if (neighboringMines > 0)
                    {
                        var position = new Vector3Int(x, y, 0);
                        field[x, y] = _cellFactory.CreateNumberCell(position, neighboringMines);
                    }
                }
        }

        private (int, int) GenerateRandomPosition(Random rand, int width, int height)
        {
            var x = rand.Next(0, width);
            var y = rand.Next(0, height);
            return (x, y);
        }

        private void PlaceMine(Cell[,] field, int x, int y)
        {
            var position = new Vector3Int(x, y, 0);
            field[x, y] = _cellFactory.CreateMineCell(position);
        }

        private int CountNeighboringMines(Cell[,] field, int x, int y)
        {
            var mineCount = 0;

            for (var dx = -1; dx <= 1; dx++)
                for (var dy = -1; dy <= 1; dy++)
                {
                    var nx = x + dx;
                    var ny = y + dy;

                    if (nx >= 0 && ny >= 0 && nx < _width && ny < _height && field[nx, ny].CellType == CellType.Mine)
                        mineCount++;
                }

            return mineCount;
        }

        private bool IsCellEmpty(Cell[,] field, int x, int y)
        {
            return field[x, y].CellType == CellType.Empty;
        }
    }
}