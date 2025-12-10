public class BossMachine
{
    public BossState CurrentState { get; private set; }
    public void Initialize(BossState startingState)
    {
        CurrentState = startingState;
        CurrentState.enter();
    }
    public void ChangeState(BossState newState)
    {
        CurrentState.exit();
        CurrentState = newState;
        CurrentState.enter();
    }
    public void Tick()
    {
        CurrentState.tick();
    }
}
