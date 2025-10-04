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
    private float energyResetTimer;

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
        EnergyReset();
        player.currentState.Update(this);

        // Transition
        SwitchRunState();

        Debug.Log(transform.position);
    }

    #region Assign

    void InputAssigns()
    {
        moveDirection = movementActionMap.FindAction("Move").ReadValue<Vector2>();
        isSpeedUp = movementActionMap.FindAction("SpeedUp").IsPressed();
        isJumpPressed = movementActionMap.FindAction("Jump").WasPressedThisFrame();
    }

    /*--------------------------------------------------------------------------*/

    void AnimatorAssigns()
    {
        player.animator.SetFloat("Speed", player.Speed);


        if (isJumpPressed && player.onGround)      // Karakter yerdeyse ve tuşa basildiysa
        {
            player.animator.SetBool("Jump", true);
        }

        else if (player.rb.linearVelocity.y < 0f && !player.onGround) // Karakter yerde değil ve düşüyorsa
        {
            player.animator.SetBool("Jump", false);
            player.animator.SetBool("Fall", true);
        }

        else if (player.onGround)                  // Karakter yerdeyse
        {
            isJumping = false;
            player.animator.SetBool("Fall", false);
            player.animator.SetBool("Jump", false);
        }
    }

    #endregion

    #region Functions

    void SpeedUp()
    {
        if (isSpeedUp && player.Energy > 5f)
        {
            player.Speed += 4.5f * Time.deltaTime;
        }
        else
        {
            player.Speed -= 9f * Time.deltaTime;
        }
    }

    /*----------------------------------------------*/


    void Jump()
    {
        if (player.onGround && isJumpPressed && !isJumping)
        {
            isJumping = true;

            Vector3 vel = player.rb.linearVelocity;

            // Mathf.Sqrt karekök almamizi sağlar.
            float jumpForce = Mathf.Sqrt(player.JumpHeight * -2f * Physics.gravity.y);   // v = √ 2.g.h --> Gereken hiz = yerçekimi x 2 x yükseklik. 

            vel.y = jumpForce;

            player.rb.linearVelocity = vel;
        }

        if (player.rb.linearVelocity.y < 0f)
        { 
            isFalling = true;
        }
        else if (player.onGround && isJumping)
        {
            isJumping = false;
        }
    }
    

    /*----------------------------------------------*/

    void EnergyReset()
    {
        if (player.Speed > 3.5f)
        {
            player.Energy -= 18f * Time.deltaTime;
            energyResetTimer = 0f;
        }
        else
        {
            energyResetTimer += Time.deltaTime;

            if (energyResetTimer >= 1.5f)
            {
                player.Energy += 12.5f * Time.deltaTime;
            }

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
