using UnityEngine;

public class IdleState : IState
{
    public void Enter(PlayerController controller)
    {

    }

    public void Exit(PlayerController controller)
    {

    }

    public void Update(PlayerController controller)
    {
        if (controller.isRunning)                                   // Koşuyor true ise
        {
            controller.ChangeState(controller.player.walkState);    // Walk State'e geç
        }
    }
}
