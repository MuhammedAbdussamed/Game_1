using System;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public static PlayerProperties Instance { get; private set; }   // Bu scripti heryerden erişilir yap.

    [Header("Movement Properties")]
    public float Speed;
    public float JumpHeight;
    internal float minSpeed;
    internal float maxSpeed;

    [Header("Properties")]
    public float Health;
    public float Energy;
    public float FallSpeed;

    // Movement Bools
    internal bool onGround;
    internal bool isInteracting;

    // Components
    internal Rigidbody rb;
    internal Animator animator;

    // States
    internal IState idleState;
    internal IState walkState;
    internal IState currentState;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        idleState = new IdleState();
        walkState = new WalkState();

        currentState = idleState;

        // Components Assign
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Values Assign
        minSpeed = 3f;
        maxSpeed = 10f;
    }

    void Update()
    {
        Clamps();
    }

    public void Clamps()
    {
        Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed);
        Energy = Mathf.Clamp(Energy, 0f, 100f);
    }

}
