namespace CrazyPawn.Infrastructure.GameStateMachine.States
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}