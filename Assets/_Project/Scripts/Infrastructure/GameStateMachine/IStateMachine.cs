namespace Infrastructure.GameStateMachine
{
    public interface IStateMachine
    {
        void SwitchState<T>() where T : class, IGameState;
        public void Update();
    }
}