using Core;
using Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace UI
{
    public class TilemapView : MonoBehaviour
    {
        [SerializeField] private TileConfig _tileConfig;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Camera _mainCamera;

        private Minefield _field;

        public void DrawField(Minefield field)
        {
            _tilemap.ClearAllTiles();
            _field = field;
            _mainCamera.transform.position = new Vector3(_field.Width / 2f, _field.Height / 2f, -10f);


            for (var x = 0; x < _field.Width; x++)
                for (var y = 0; y < _field.Height; y++)
                {
                    var cell = _field.GetCell(x, y);
                    var tile = GetTile(cell);
                    _tilemap.SetTile(cell.Position, tile);
                }
        }

        private Tile GetTile(Cell cell)
        {
            if (cell.IsRevealed && TryGetRevealedTile(cell, out Tile revealedTile))
            {
                return revealedTile;
            }

            if (_field.IsGameOver && TryGetGameOverTile(cell, out Tile gameOverTile))
            {
                return gameOverTile;
            }

            if (_field.IsWin && TryGetWinTile(cell, out Tile winTile))
            {
                return winTile;
            }

            if (cell.IsFlagged)
            {
                return GetFlaggedTile();
            }

            return _tileConfig.Unknown;
        }

        private bool TryGetRevealedTile(Cell cell, out Tile tile)
        {
            switch (cell.CellType)
            {
                case CellType.Empty:
                    tile = _tileConfig.Empty;
                    return true;
                case CellType.Mine:
                    tile = cell.IsExploded ? _tileConfig.Exploded : _tileConfig.Mine;
                    return true;
                case CellType.Number:
                    tile = GetNumberTile(cell.MinesNumber);
                    return true;
                default:
                    tile = null;
                    return false;
            }
        }

        private bool TryGetGameOverTile(Cell cell, out Tile tile)
        {
            if (cell.CellType == CellType.Mine)
            {
                tile = cell.IsExploded ? _tileConfig.Exploded : _tileConfig.Mine;
                return true;
            }

            tile = null;
            return false;
        }

        private bool TryGetWinTile(Cell cell, out Tile tile)
        {
            if (cell.CellType == CellType.Mine)
            {
                tile = _tileConfig.Flagged;
                return true;
            }

            tile = null;
            return false;
        }

        private Tile GetFlaggedTile()
        {
            return _tileConfig.Flagged;
        }

        private Tile GetNumberTile(int number)
        {
            var index = number - 1;

            if (IsOutOfRange(index))
                throw new System.IndexOutOfRangeException($"Number {number} is out of range in TileNums list");

            return _tileConfig.TileNums[index];
        }

        private bool IsOutOfRange(int index) => index < 0 || index >= _tileConfig.TileNums.Count;
    }
}