using UnityEngine;
using UnityEngine.EventSystems;

public class WalkState : IState
{
    MovementState movementState;                // Karakterin hangi yöne gideceğini belirten enum değişkeni.

    public void Enter(PlayerController controller)
    {

    }

    public void Exit(PlayerController controller)
    {
        ResetAnimations(controller);
    }

    public void Update(PlayerController controller)
    {
        // Functions
        ChangeMovementState(controller);
        WalkAnimations(controller);
        Walk(controller);
        
        if (!controller.isRunning)                                  // Koşuyor false ise
        {
            controller.ChangeState(controller.player.idleState);    // Idle State'e geç
        }
    }

    #region Movement Functions

    void Walk(PlayerController controller)
    {
        Vector3 move = new Vector3(controller.moveDirection.x, controller.player.rb.linearVelocity.y, controller.moveDirection.y);

        move = controller.transform.TransformDirection(move);

        controller.player.rb.linearVelocity = move * controller.player.Speed;
    }

    /*-----------------------------------------------------------------------------------*/

    void ChangeMovementState(PlayerController controller)
    {
        if (controller.moveDirection.y > 0f && controller.moveDirection.x == 0f)         //
        {                                                                                // Öne git
            movementState = MovementState.Forward;                                       //
        }
        else if (controller.moveDirection.y < 0f && controller.moveDirection.x == 0f)    //
        {                                                                                // Arkaya git
            movementState = MovementState.Backward;                                      //
        }
        else if (controller.moveDirection.y == 0f && controller.moveDirection.x > 0f)    // 
        {                                                                                // Sağa git
            movementState = MovementState.Right;                                         //
        }
        else if (controller.moveDirection.y == 0f && controller.moveDirection.x < 0f)    //
        {                                                                                // Sola git
            movementState = MovementState.Left;                                          //
        }
    }

    #endregion

    #region Animations Functions 

    void ResetAnimations(PlayerController controller)
    {
        controller.player.animator.SetBool("WalkForward", false);
        controller.player.animator.SetBool("WalkBackward", false);
        controller.player.animator.SetBool("WalkRight", false);
        controller.player.animator.SetBool("WalkLeft", false);
    }
    
    void WalkAnimations(PlayerController controller)
    {
        ResetAnimations(controller);

        switch (movementState)
        {
            case MovementState.Forward:
                controller.player.animator.SetBool("WalkForward", true);
                break;

            case MovementState.Backward:
                controller.player.animator.SetBool("WalkBackward", true);
                break;

            case MovementState.Right:
                controller.player.animator.SetBool("WalkRight", true);
                break;

            case MovementState.Left:
                controller.player.animator.SetBool("WalkLeft", true);
                break;
        }
    }

    #endregion

    #region Enums

    enum MovementState
    {
        Idle,
        Forward,
        Backward,
        Right,
        Left,
        Forward_Right,
        Forward_Left,
        Backward_Right,
        Backward_Left
    }
    
    #endregion
    
}
