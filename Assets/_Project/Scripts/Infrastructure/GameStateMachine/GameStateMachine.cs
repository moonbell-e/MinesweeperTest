using System;
using System.Collections.Generic;
using Core;
using Core.Input;
using Infrastructure.GameStateMachine.States;
using Zenject;

namespace Infrastructure.GameStateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _currentState;

        [Inject]
        public GameStateMachine(MinesweeperMediator mediator, IInputService inputService)
        {
            _states = new Dictionary<Type, IGameState>
            {
                { typeof(GamePlayState), new GamePlayState(this, mediator, inputService) },
                { typeof(GameWonState), new GameWonState(this, mediator, inputService) },
                { typeof(GameOverState), new GameOverState(this, mediator, inputService) }
            };

            _currentState = _states[typeof(GamePlayState)];
        }

        public void SwitchState<T>() where T : class, IGameState
        {
            var type = typeof(T);
            
            if (_states.TryGetValue(type, out var state))
            {
                _currentState.Exit();
                _currentState = state;
                _currentState.Enter();
            }
        }

        public void Update()
        {
            _currentState.Update();
        }
    }
}