using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPS_Controller : MonoBehaviour
{
    [Header("Inputs References")]
    [SerializeField] private InputActionAsset playerInputs;
    private InputActionMap cameraActionMap;
    private InputAction aimAction;

    [Header("Cameras")]
    [SerializeField] private CinemachineCamera aimCamera;

    // Movement Bools
    private bool isAiming;

    void Start()
    {
        cameraActionMap = playerInputs.FindActionMap("Camera");
        aimAction = cameraActionMap.FindAction("Aim");
    }

    void OnEnable()
    {
        playerInputs.Enable();
    }

    void Update()
    {
        // Assign
        InputAssign();

        // Functions
        ActiveAimCamera();
    }

    void InputAssign()
    {
        isAiming = aimAction.IsPressed();
    }

    void ActiveAimCamera()
    {
        if (isAiming)
        {
            aimCamera.gameObject.SetActive(true);
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
        }
    }
}
