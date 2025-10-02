using System;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public static PlayerProperties Instance { get; private set; }   // Bu scripti heryerden erişilir yap.

    [Header("Movement Properties")]
    public float Speed;
    public float JumpPower;

    [Header("Properties")]
    public float Health;
    public float Energy;
    public float FallSpeed;

    // Movement Bools
    internal bool onGround;

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
    }

    void Update()
    {
        Speed = Mathf.Clamp(Speed, 3f, 8f);
    }

}
