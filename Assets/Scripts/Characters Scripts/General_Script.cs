using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class General_Script : MonoBehaviour
{
    // Animation Assign
    private Animator animator;
    
    // UI Elements
    [SerializeField] private Canvas talkCanvas;
    [SerializeField] private TextMeshProUGUI dialogue;

    // Movement Bools
    internal bool isNearPlayer;
    internal bool firstMeeting = true;

    // States
    internal bool interacting;

    // Scripts
    private GameManager gameManager;

    // Dialogue bools
    [SerializeField] internal List<String> dialogues = new List<String>();
    

    void Start()
    {
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        InteractPlayer();
        AnimatorAssign();
    }

    void OnTriggerEnter(Collider col){ DetectPlayer(col, true); }   // Karakterin collidera girmesi

    void OnTriggerExit(Collider col){ DetectPlayer(col, false); }   // Karakterin colliderdan çikmasi

    #region Functions

    public void DetectPlayer(Collider col, bool enterExit)
    {
        if (col.CompareTag("Player"))
        {
            isNearPlayer = enterExit;
        }
    }

    /*------------------------------*/

    void InteractPlayer()
    {
        if (isNearPlayer && gameManager.controller.isInteractPressed && !interacting)
        {
            interacting = true;
        }
        else if (interacting && gameManager.controller.isInteractPressed)
        {
            interacting = false;
            firstMeeting = false;
        }
    }

    /*-------------------------------*/


    void AnimatorAssign()
    {
        animator.SetBool("Talking", interacting);
    }

    #endregion
}
