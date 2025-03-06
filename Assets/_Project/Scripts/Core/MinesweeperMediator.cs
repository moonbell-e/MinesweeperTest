using System;
using UI;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MinesweeperMediator
    {
        private readonly GameResultView _gameResultView;
        private readonly Minefield _minefield;
        private readonly TilemapView _tilemapView;
        
        public bool IsGameOver => _minefield.IsGameOver;
        public bool IsWin => _minefield.IsWin;

        [Inject]
        public MinesweeperMediator(Minefield minefield, TilemapView tilemapView, GameResultView gameResultView)
        {
            _minefield = minefield;
            _tilemapView = tilemapView;
            _gameResultView = gameResultView;
        }

        public void Initialize()
        {
            _minefield.CreateField();
            _tilemapView.DrawField(_minefield);
        }

        public void RevealCell(Vector2Int position)
        {
            if (_minefield.IsOutOfBounds(position) || _minefield.IsGameOver)
                return;

            _minefield.RevealCell(position.x, position.y);
            _tilemapView.DrawField(_minefield);
        }

        public void ToggleFlag(Vector2Int position)
        {
            if (_minefield.IsOutOfBounds(position) || _minefield.IsGameOver)
                return;

            _minefield.ToggleFlag(position.x, position.y);
            _tilemapView.DrawField(_minefield);
        }

        public void RevealAllMines(bool isWin)
        {
            _minefield.RevealAllMines(isWin);
            _tilemapView.DrawField(_minefield);
        }

        public void ShowGameOver(Action onRestart)
        {
            _gameResultView.ShowGameOverPanel(onRestart);
        }
        
        public void ShowGameWon(Action onRestart)
        {
            _gameResultView.ShowGameWonPanel(onRestart);
        }

        public void ResetGameState()
        {
            _minefield.ResetGameState();
        }

        public void HideGameEnd()
        {
            _gameResultView.HideGameEndPanel();
        }
    }
}