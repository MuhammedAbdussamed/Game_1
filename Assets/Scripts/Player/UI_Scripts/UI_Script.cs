using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    // Canvases
    [SerializeField] private Canvas TalkCanvas;
    [SerializeField] private Canvas TaskCanvas;
    private Transform DynamicCanvas;

    // Elements
    private Slider HealthBar;
    private Slider EnergyBar;

    private TextMeshProUGUI dialogue;

    private TextMeshProUGUI taskText;
    private Toggle taskToggle; 

    // Scripts
    private GameManager gameManager;

    void Start()
    {
        // Scripts
        gameManager = GameManager.Instance;

        // Canvases
        DynamicCanvas = transform.Find("DynamicCanvas");

        // Elements
        HealthBar = DynamicCanvas.transform.Find("Health_Bar").GetComponent<Slider>();
        EnergyBar = DynamicCanvas.transform.Find("Energy_Bar").GetComponent<Slider>();

        dialogue = TalkCanvas.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();

        taskText = TaskCanvas.transform.Find("Task_Text").GetComponent<TextMeshProUGUI>();
        taskToggle = TaskCanvas.transform.Find("Task_Toggle").GetComponent<Toggle>();

        // Scripts
        gameManager.player = PlayerProperties.Instance;
    }

    void Update()
    {
        AssignValue();
        OpenDialogue();
        SetTask();
    }

    void AssignValue()
    {
        HealthBar.value = gameManager.player.Health;
        EnergyBar.value = gameManager.player.Energy;
    }

    void OpenDialogue()
    {
        if (gameManager.player.isInteracting)
        {
            TalkCanvas.gameObject.SetActive(true);
            PrintDialogue();
        }
        else
        {
            TalkCanvas.gameObject.SetActive(false);
        }
    }

    void PrintDialogue()
    {
        if (gameManager.generalScript.firstMeeting)
        {
            dialogue.text = gameManager.generalScript.dialogues[0];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1 && !gameManager.generalScript.firstMeeting && gameManager.generalScript.isNearPlayer)
        {
            dialogue.text = "İş başına asker!";
        }
    }

    void SetTask()
    {
        if (gameManager.taskScript.duringMission)           // Eğer görev devam ediyorsa...
        {
            if (!gameManager.taskScript.isPortalFound)              // Portal bulunamadiysa devam et...
            {
                AddTask();
            }
            else if (gameManager.taskScript.isPortalFound)          // Portal bulunduysa devam et...
            {
                RemoveTask();
            }
        }
    }

    void AddTask()
    {
        TaskCanvas.gameObject.SetActive(true);

        taskText.text = gameManager.taskScript.tasks[0];
    }

    async void RemoveTask()
    {
        taskToggle.isOn = true;                     // Görev butonunu tikle ve
                                                    
        await Task.Delay(2000);                     // 2  saniye  bekle

        TaskCanvas.gameObject.SetActive(false);     // Task bölümünü kapat
    }
}
