using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }       // Heryerden erişilebilir olmasini sağla

    [Header("Script References")]
    [SerializeField] internal PlayerController controller;           //
    [SerializeField] internal General_Script generalScript;          //
    [SerializeField] internal Task_Script taskScript;                //  
    [SerializeField] internal UI_Script uiScript;                    // Diğer önemli scriptlerin referanslari.
    [SerializeField] internal Portal portalScript;                   // 
    [SerializeField] internal GroundChecker groundChecker;           //
    internal PlayerProperties player;                                //

    void Awake()
    {
        if (Instance == null) { Instance = this; }                   //
                                                                     // Klasik kalip. Instance null'sa bu scripte eşitle. Değilse bu scripti yok et.
        else { Destroy(gameObject); }                                //
    }

    void Start()
    {
        player = PlayerProperties.Instance;
    }

    void Update()
    {
        SetInteract();
    }

    void SetInteract()
    {
        player.isInteracting = generalScript.interacting;
    }

    
}
