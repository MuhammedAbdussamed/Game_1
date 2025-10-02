using UnityEngine;

public class JumpState : IState
{
    public void Enter(PlayerController controller)
    {

    }

    public void Exit(PlayerController controller)
    {

    }

    public void Update(PlayerController controller)
    {
        Jump(controller);

        if (controller.player.onGround)
        {
            controller.ChangeState(controller.player.idleState);
        }
    }

    void Jump(PlayerController controller)
    {
        controller.player.rb.AddForce(Vector3.up * controller.player.JumpPower);
    }
}
