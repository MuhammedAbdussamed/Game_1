using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPS_Controller : MonoBehaviour
{
    [Header("Inputs References")]
    private InputActionMap CamActionMap;
    private InputAction aimAction;
    private InputAction lookAction;

    [Header("Cameras & Settings")]
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private Transform shoulder;
    [SerializeField] private float Sensitivity;

    // Script References
    [SerializeField] private PlayerController controller;

    // Movement Bools
    private bool isAiming;          // Karakterin aim alip almadiğini kontrol eden bool.
    private Vector2 lookDirection;  // Karakterin bakacaği yönü tutan değişken.

    private float yaw;              // Kameranin x ekseninde ki hareket değerini tutan değişken
    private float pitch;            // Kameranin y ekseninde ki hareket değerini tutan değişken

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;       // Mouse'u kilitler.

        CamActionMap = controller.CamActionMap;
        aimAction = CamActionMap.FindAction("Aim");
        lookAction = CamActionMap.FindAction("Look");
    }

    void Update()
    {
        // Assign
        InputAssign();

        // Functions
        ActiveAimCamera();
    }

    void LateUpdate()
    {
        Look();
    }

    void InputAssign()
    {
        isAiming = aimAction.IsPressed();
        lookDirection = lookAction.ReadValue<Vector2>();
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

    void Look()
    {
        yaw += lookDirection.x * Sensitivity * Time.deltaTime;
        pitch -= lookDirection.y * Sensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -40f, 90f);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        shoulder.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    
    }
}
