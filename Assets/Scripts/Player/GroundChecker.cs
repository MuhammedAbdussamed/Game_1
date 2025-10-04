using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GroundChecker : MonoBehaviour
{
    // Layer Mask & Transform
    [SerializeField] private LayerMask ignoringLayers;
    [SerializeField] private Transform spine;
    [SerializeField] private float distance;
    
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
        ray = new Ray(transform.position, Vector3.down);

        ClampPosition();
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        if (Physics.Raycast(ray, out hit, distance, ~ignoringLayers))
        {
            player.onGround = true;
            Debug.DrawRay(transform.position, Vector3.down, Color.green, distance);
        }
        else
        {
            player.onGround = false;
            Debug.DrawRay(transform.position, Vector3.down, Color.red, distance);
        }
    }

    void ClampPosition()
    {
        transform.position = spine.position;
    }
    
}
