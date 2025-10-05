using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class General_Script : MonoBehaviour
{
    // Animation Assign
    [SerializeField] private Animator animator;
    private bool interact;

    // UI Elements
    [SerializeField] private Canvas talkCanvas;
    [SerializeField] private TextMeshProUGUI dialogue;

    // Movement Bools
    private bool isNearPlayer;

    // Scripts
    [SerializeField] private PlayerController controller;

    // Dialogue bools
    [SerializeField] private List<String> dialogues = new List<String>();
    internal bool firstMeeting;

    void Update()
    {
        InteractPlayer();
        AnimatorAssign();
    }

    void OnTriggerEnter(Collider col)
    {
        DetectPlayer(col);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isNearPlayer = false;
            interact = false;
        }
    }

    #region Functions

    void DetectPlayer(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    /*------------------------------*/

    void InteractPlayer()
    {
        if (interact && controller.isInteractPressed)
        {
            talkCanvas.gameObject.SetActive(false);
            controller.movementActionMap.Enable();
        }

        if (isNearPlayer && controller.isInteractPressed)
        {
            controller.player.isInteracting = true;
            interact = true;

            SetInteract();
        }
    }

    /*-------------------------------*/

    void SetInteract()
    {
        if (!firstMeeting)
        {
            FirstMeeting();
        }
        
        else if (firstMeeting)
        {
            Debug.Log("Zaten konuştun");
        }
        
    }

    /*-------------------------------*/

    void FirstMeeting()
    {
        controller.movementActionMap.Disable();

        talkCanvas.gameObject.SetActive(true);
        dialogue.text = dialogues[0];
        firstMeeting = true;
    }

    /*-------------------------------*/


    void AnimatorAssign()
    {
        animator.SetBool("Talking", interact);
    }

    #endregion
}
