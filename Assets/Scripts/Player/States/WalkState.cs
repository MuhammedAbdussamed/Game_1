using System.Collections;
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
        Vector3 move = new Vector3(controller.moveDirection.x, 0f, controller.moveDirection.y);

        move = controller.transform.TransformDirection(move);                           // Karakter etrafinda dönse bile ön yüzü hep Z değeri kalacak.

        Vector3 velocity = controller.player.rb.linearVelocity;
        velocity.x = move.x;
        velocity.z = move.z;

        controller.player.rb.linearVelocity = velocity;
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
        /*----------------------------------------*/
        else if (controller.moveDirection.y > 0f && controller.moveDirection.x > 0f)
        {
            movementState = MovementState.Forward_Right;
        }
        else if (controller.moveDirection.y > 0f && controller.moveDirection.x < 0f)
        {
            movementState = MovementState.Forward_Left;
        }
        else if (controller.moveDirection.y < 0f && controller.moveDirection.x > 0f)
        {
            movementState = MovementState.Backward_Right;
        }
        else if (controller.moveDirection.y < 0f && controller.moveDirection.x < 0f)
        {
            movementState = MovementState.Backward_Left;
        }
    }

    #endregion

    #region Animations Functions 

    void ResetAnimations(PlayerController controller)
    {
        controller.player.animator.SetBool("Forward", false);
        controller.player.animator.SetBool("Backward", false);
        controller.player.animator.SetBool("Right", false);
        controller.player.animator.SetBool("Left", false);
        controller.player.animator.SetBool("Forward_Right", false);
        controller.player.animator.SetBool("Forward_Left", false);
        controller.player.animator.SetBool("Backward_Right", false);
        controller.player.animator.SetBool("Backward_Left", false);
    }
    
    void WalkAnimations(PlayerController controller)
    {
        ResetAnimations(controller);

        switch (movementState)
        {
            case MovementState.Forward:
                controller.player.animator.SetBool("Forward", true);
                break;

            case MovementState.Backward:
                controller.player.animator.SetBool("Backward", true);
                break;

            case MovementState.Right:
                controller.player.animator.SetBool("Right", true);
                break;

            case MovementState.Left:
                controller.player.animator.SetBool("Left", true);
                break;

            case MovementState.Forward_Right:
                controller.player.animator.SetBool("Forward_Right", true);
                break;

            case MovementState.Forward_Left:
                controller.player.animator.SetBool("Forward_Left", true);
                break;

            case MovementState.Backward_Right:
                controller.player.animator.SetBool("Backward_Right", true);
                break;

            case MovementState.Backward_Left:
                controller.player.animator.SetBool("Backward_Left", true);
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
