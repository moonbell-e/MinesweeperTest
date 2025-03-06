using Core;
using Core.Input;

namespace Infrastructure.GameStateMachine.States
{
    public class GamePlayState : IGameState
    {
        private readonly IInputService _inputService;
        private readonly MinesweeperMediator _mediator;
        private readonly IStateMachine _stateSwitcher;

        public GamePlayState(IStateMachine stateSwitcher, MinesweeperMediator mediator, IInputService inputService)
        {
            _stateSwitcher = stateSwitcher;
            _mediator = mediator;
            _inputService = inputService;
        }

        public void Enter()
        {
            _mediator.Initialize();
        }

        public void Exit()
        {
        }

        public void Update()
        {
            HandleInput();
            CheckGameEnd();
        }

        private void HandleInput()
        {
            if (_inputService.IsLeftMouseButtonClicked())
                _mediator.RevealCell(_inputService.GetMousePosition());

            if (_inputService.IsRightMouseButtonClicked())
                _mediator.ToggleFlag(_inputService.GetMousePosition());

            if (_inputService.IsRestartKeyPressed())
                RestartGame();
        }

        private void CheckGameEnd()
        {
            if (_mediator.IsGameOver)
            {
                _mediator.RevealAllMines(false);
                _mediator.ResetGameState();
                _stateSwitcher.SwitchState<GameOverState>();
            }

            if (_mediator.IsWin)
            {
                _mediator.RevealAllMines(true);
                _mediator.ResetGameState();
                _stateSwitcher.SwitchState<GameWonState>();
            }
        }

        private void RestartGame()
        {
            _mediator.Initialize();
        }
    }
}