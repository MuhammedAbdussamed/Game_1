using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class Task_Script : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private General_Script generalScript;
    [SerializeField] private Portal portalScript;

    [Header("UI_Objects")]
    [SerializeField] private Canvas taskCanvas;
    [SerializeField] private Toggle taskToggle;
    [SerializeField] private TextMeshProUGUI taskText;
    
    // Task List
    [SerializeField] private List<String> tasks = new List<String>();

    void Update()
    {
        SetTask();
    }

    async void SetTask()
    {
        if (generalScript.firstMeeting)
        {
            taskCanvas.gameObject.SetActive(true);
            taskText.text = tasks[0];
        }

        if (generalScript.firstMeeting && portalScript.isCharacterIn)
        {
            taskToggle.isOn = true;

            ColorBlock cb = taskToggle.colors;
            cb.normalColor = Color.green;
            taskToggle.colors = cb;

            await Task.Delay(2000);

            taskCanvas.gameObject.SetActive(false);
        }
    }
}
