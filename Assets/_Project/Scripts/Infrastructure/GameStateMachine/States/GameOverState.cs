using Core;
using Core.Input;

namespace Infrastructure.GameStateMachine.States
{
    public class GameOverState : IGameState
    {
        private readonly IInputService _inputService;
        private readonly MinesweeperMediator _mediator;
        private readonly IStateMachine _stateSwitcher;

        public GameOverState(IStateMachine stateSwitcher, MinesweeperMediator mediator, IInputService inputService)
        {
            _stateSwitcher = stateSwitcher;
            _mediator = mediator;
            _inputService = inputService;
        }

        public void Enter()
        {
            _mediator.ShowGameOver(() => _stateSwitcher.SwitchState<GamePlayState>());
        }

        public void Exit()
        {
            _mediator.HideGameEnd();
        }

        public void Update()
        {
            if (_inputService.IsRestartKeyPressed())
                _stateSwitcher.SwitchState<GamePlayState>();
        }
    }
}