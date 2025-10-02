using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GroundChecker : MonoBehaviour
{
    [Header("RaycastLayers")]
    [SerializeField] private LayerMask raycastLayers;
    [SerializeField] private Transform spine;
    

    // Script Reference
    private PlayerProperties player;

    // Raycast Variables
    private RaycastHit hit;
    private Ray ray;

    void Start()
    {
        player = PlayerProperties.Instance;
    }

    void Update()
    {
        Debug.Log(player.onGround);

        ray = new Ray(transform.position, Vector3.down);

        ClampPosition();
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        if (Physics.Raycast(ray, out hit, 1.1f, ~raycastLayers))
        {
            player.onGround = true;
            Debug.DrawRay(transform.position, Vector3.down, Color.green, 1.1f);
        }
        else
        {
            player.onGround = false;
            Debug.DrawRay(transform.position, Vector3.down, Color.red, 1.1f);
        }
    }

    void ClampPosition()
    {
        transform.position = spine.position;
    }
    
}
