using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input References")]
    [SerializeField] private InputActionAsset playerActionMap;
    internal InputActionMap movementActionMap;
    internal InputActionMap CamActionMap;

    // Script reference
    internal PlayerProperties player;

    // Movement Variables        
    internal Vector2 moveDirection;

    // State bools
    internal bool isRunning;
    internal bool isFalling;

    // Movement bools
    internal bool isSpeedUp;
    internal bool isJumpPressed;
    internal bool isJumping;

    void Start()
    {
        player = PlayerProperties.Instance;
        playerActionMap.Enable();
        movementActionMap = playerActionMap.FindActionMap("Movement");
        CamActionMap = playerActionMap.FindActionMap("Camera");
    }

    void Update()
    {
        // Assign
        InputAssigns();
        AnimatorAssigns();

        // Functions
        SpeedUp();
        Jump();
        player.currentState.Update(this);

        // Transition
        SwitchRunState();
    }

    #region Assign

    void InputAssigns()
    {
        moveDirection = movementActionMap.FindAction("Move").ReadValue<Vector2>();
        isSpeedUp = movementActionMap.FindAction("SpeedUp").IsPressed();
        isJumpPressed = movementActionMap.FindAction("Jump").IsPressed();
    }

    void AnimatorAssigns()
    {
        player.animator.SetFloat("Speed", player.Speed);

        if (player.rb.linearVelocity.y > 0f)
        {
            player.animator.SetBool("Jump", true);
        }
        else
        {
            player.animator.SetBool("Jump", false);
        }
    }

    #endregion

    #region Functions

    void SpeedUp()
    {
        if (isSpeedUp)
        {
            player.Speed += 4.5f * Time.deltaTime;
        }
        else
        {
            player.Speed -= 9f * Time.deltaTime;
        }
    }

    void Jump()
    {
        if (player.onGround && isJumpPressed)
        {
            Vector3 v = player.rb.linearVelocity;
            v.y = 0f;
            player.rb.linearVelocity = v;

            player.rb.AddForce(Vector3.up * player.JumpPower);
        }

        if (player.rb.linearVelocity.y < 0f)
        {
            isFalling = true;
        }
    }

    #endregion

    #region StateTransition

    public void ChangeState(IState newState)
    {
        player.currentState.Exit(this);
        player.currentState = newState;
        player.currentState.Enter(this);
    }

    /*--------------------------------------*/

    void SwitchRunState()
    {
        if (moveDirection.x != 0f || moveDirection.y != 0f)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    #endregion

    
}
