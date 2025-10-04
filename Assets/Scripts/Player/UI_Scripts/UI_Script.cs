using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    // Canvases
    private Transform DynamicCanvas;

    // Elements
    private Slider HealthBar;
    private Slider EnergyBar;

    // Scripts
    private PlayerProperties player;

    void Start()
    {
        // Canvases
        DynamicCanvas = transform.Find("DynamicCanvas");

        // Elements
        HealthBar = DynamicCanvas.transform.Find("Health_Bar").GetComponent<Slider>();
        EnergyBar = DynamicCanvas.transform.Find("Energy_Bar").GetComponent<Slider>();

        // Scripts
        player = PlayerProperties.Instance;
    }

    void Update()
    {
        AssignValue();
    }

    void AssignValue()
    {
        HealthBar.value = player.Health;
        EnergyBar.value = player.Energy;
    }
}
