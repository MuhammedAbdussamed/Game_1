using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Task_Script : MonoBehaviour
{
    // Scripts
    private GameManager gameManager;

    // Task List
    [SerializeField] internal List<String> tasks = new List<String>();

    // Variables
    internal String currentTask;
    internal float currentTaskIndex;

    // Mission Flag
    internal bool isPortalFound;
    internal bool duringMission;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        SetTask();
        FinishTask();
    }

    async void SetTask()
    {
        if (!gameManager.generalScript.firstMeeting)
        {
            currentTask = tasks[0];
            currentTaskIndex = 0;
            duringMission = true;
        }
    }

    void FinishTask()
    {
        if (currentTaskIndex == 0 && isPortalFound)
        {
            duringMission = false;
        }
    }


}
