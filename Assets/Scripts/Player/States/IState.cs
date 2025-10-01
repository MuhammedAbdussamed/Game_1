using UnityEngine;

public interface IState
{
    void Enter(PlayerController controller);
    void Exit(PlayerController controller);
    void Update(PlayerController controller);
}
