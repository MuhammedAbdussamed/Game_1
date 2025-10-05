using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Script
    [SerializeField] private General_Script generalScript;
    [SerializeField] private PlayerController controller;

    // Movement Bools
    internal bool isCharacterIn;

    void Update()
    {
        Teleport();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isCharacterIn = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isCharacterIn = false;
        }
    }

    void Teleport()
    {
        if (isCharacterIn && generalScript.firstMeeting && controller.isInteractPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
