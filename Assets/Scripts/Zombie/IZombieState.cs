public interface IZombieState
{
    void Enter(ZombieController zombie);
    void Execute(ZombieController zombie);
    void Exit(ZombieController zombie);
}
