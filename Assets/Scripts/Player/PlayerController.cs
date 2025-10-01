using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input References")]
    [SerializeField] private InputActionAsset playerActionMap;
    private InputActionMap movementActionMap;

    // Script reference
    internal PlayerProperties player;

    // Movement Variables        
    internal Vector2 moveDirection;

    // Movement bools
    internal bool isRunning;
    internal bool isSpeedUp;

    void Start()
    {
        player = PlayerProperties.Instance;
        playerActionMap.Enable();
        movementActionMap = playerActionMap.FindActionMap("Movement");
    }

    void Update()
    {
        // Assign
        InputAssigns();
        AnimatorAssigns();

        // Functions
        SpeedUp();
        player.currentState.Update(this);

        // Transition
        SwitchRunState();

        Debug.Log(moveDirection);
    }

    #region Assign

    void InputAssigns()
    {
        moveDirection = movementActionMap.FindAction("Move").ReadValue<Vector2>();
        isSpeedUp = movementActionMap.FindAction("SpeedUp").IsPressed();
    }

    void AnimatorAssigns()
    {
        player.animator.SetFloat("Speed", player.Speed);
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
