using Infrastructure.GameStateMachine;
using Infrastructure.GameStateMachine.States;
using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    private IStateMachine _gameStateMachine;
    
    [Inject]
    private void Construct(IStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    private void Awake()
    {
        _gameStateMachine.SwitchState<GamePlayState>();
    }

    private void Update()
    {
        _gameStateMachine.Update();
    }
}