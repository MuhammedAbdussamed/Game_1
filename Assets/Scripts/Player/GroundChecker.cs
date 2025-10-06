using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GroundChecker : MonoBehaviour
{
    // Layer Mask & Transform
    [SerializeField] private LayerMask ignoringLayers;
    [SerializeField] private Transform spine;

    // Script Reference
    private GameManager gameManager;

    // Ground Material
    internal GroundMaterial groundMaterial;

    // Raycast Variables
    private RaycastHit hit;
    private Ray ray;

    void Start()
    {
        gameManager = GameManager.Instance;
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
        if (Physics.Raycast(ray, out hit, 1.8f, ~ignoringLayers))
        {
            gameManager.player.onGround = true;
            Debug.DrawRay(transform.position, Vector3.down, Color.green, 1.8f);
            CheckGroundMaterial(hit);
        }
        else
        {
            gameManager.player.onGround = false;
            Debug.DrawRay(transform.position, Vector3.down, Color.red, 1.8f);
        }
    }

    void ClampPosition()
    {
        transform.position = spine.position;
    }

    void CheckGroundMaterial(RaycastHit hit)
    {
        if (hit.collider.CompareTag("MetalGround"))
        {
            groundMaterial = GroundMaterial.Metal;
        }
        else if (hit.collider.CompareTag("Grass"))
        {
            groundMaterial = GroundMaterial.Grass;
        }
        else if (hit.collider.CompareTag("Gravel"))
        {
            groundMaterial = GroundMaterial.Gravel;
        }

    }

    public enum GroundMaterial
    {
        Grass,
        Metal,
        Gravel
    }
    
}
