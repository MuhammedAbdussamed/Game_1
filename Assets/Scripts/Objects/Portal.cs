using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Script
    private GameManager gameManager;

    // Movement Bools
    internal bool isCharacterIn;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        Teleport();
    }

    void OnTriggerEnter(Collider col)
    {
        DetectPlayer(col, true);
        MissionCompleted();
    }

    void OnTriggerExit(Collider col)
    {
        DetectPlayer(col, false);
    }

    void MissionCompleted()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (gameManager.taskScript.currentTaskIndex == 0 && gameManager.taskScript.duringMission)
            {
                gameManager.taskScript.isPortalFound = true;
            }
        }
    }

    void Teleport()
    {
        if (isCharacterIn && !gameManager.generalScript.firstMeeting && gameManager.controller.isInteractPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void DetectPlayer(Collider col, bool isIn)
    {
        if (col.CompareTag("Player"))
        {
            isCharacterIn = isIn;
        }
    }
}
