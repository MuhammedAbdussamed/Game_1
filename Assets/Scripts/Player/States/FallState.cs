using UnityEngine;

public class FallState : IState
{
    public void Enter(PlayerController controller)
    {
        controller.player.animator.SetBool("Fall", true);
        controller.player.animator.SetBool("Jump", false);
    }

    public void Exit(PlayerController controller)
    {
        controller.player.animator.SetBool("Fall", false);
    }

    public void Update(PlayerController controller)
    {
        Fall(controller);

        if (controller.player.onGround && controller.isRunning)
        {
            controller.ChangeState(controller.player.walkState);
        }
        else if (controller.player.onGround && controller.isRunning)
        {
            controller.ChangeState(controller.player.idleState);
        }
    }

    void Fall(PlayerController controller)
    {
        // Vector3.up yönünde gravity uygula. Gravity negatif olduğu için sonuç negatif. Kuvveti yavaş yavaş uygula.
        controller.player.rb.AddForce(Vector3.up * Physics.gravity.y * (controller.player.FallSpeed - 1), ForceMode.Acceleration);
    }
    
}
