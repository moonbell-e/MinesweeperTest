using Core;
using Core.Input;

namespace Infrastructure.GameStateMachine.States
{
    public class GameWonState : IGameState
    {
        private readonly IInputService _inputService;
        private readonly MinesweeperMediator _minesweeperMediator;
        private readonly IStateMachine _stateSwitcher;
        
        public GameWonState(IStateMachine stateSwitcher, MinesweeperMediator mediator, IInputService inputService)
        {
            _stateSwitcher = stateSwitcher;
            _minesweeperMediator = mediator;
            _inputService = inputService;
        }

        public void Enter()
        {
            _minesweeperMediator.ShowGameWon(() => _stateSwitcher.SwitchState<GamePlayState>());
        }

        public void Exit()
        {
            _minesweeperMediator.HideGameEnd();
        }

        public void Update()
        {
            if (_inputService.IsRestartKeyPressed())
                _stateSwitcher.SwitchState<GamePlayState>();
        }
    }
}