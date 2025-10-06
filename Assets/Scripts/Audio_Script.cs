using UnityEngine;

public class Audio_Script : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioSource walkEffect;

    [Header("Sound_Effects")]
    [SerializeField] private AudioClip popUp;
    [SerializeField] private AudioClip missionCompleted;
    [SerializeField] private AudioClip metalWalkEffect;
    [SerializeField] private AudioClip grassWalkEffect;
    [SerializeField] private AudioClip gravelWalkEffect;
    private AudioClip currentWalkEffect;

    // Flags
    private bool missionSoundPlayed;
    private bool isPlaying;

    // Scripts
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        MissionPopup();
        PlayWalkEffect();
    }

    #region Functions

    void MissionPopup()
    {
        if (gameManager.taskScript.duringMission && !missionSoundPlayed)
        {
            soundEffect.PlayOneShot(popUp);
            missionSoundPlayed = true;
        }

        else if (gameManager.taskScript.isPortalFound && missionSoundPlayed)
        {
            soundEffect.PlayOneShot(missionCompleted);
        }

        else if (!gameManager.taskScript.duringMission)
        {
            missionSoundPlayed = false;
        }
    }

    void PlayWalkEffect()
    {
        SetWalkEffect();

        if (gameManager.controller.isRunning && !isPlaying)
        {
            isPlaying = true;
            walkEffect.Play();
        }
        else if (!gameManager.controller.isRunning)
        {
            walkEffect.Stop();
            isPlaying = false;
        }
    }

    void SetWalkEffect()
    {
        switch (gameManager.groundChecker.groundMaterial)
        {
            case GroundChecker.GroundMaterial.Metal:
                currentWalkEffect = metalWalkEffect;
                break;

            case GroundChecker.GroundMaterial.Grass:
                currentWalkEffect = grassWalkEffect;
                break;

            case GroundChecker.GroundMaterial.Gravel:
                currentWalkEffect = gravelWalkEffect;
                break;
        }

        walkEffect.clip = currentWalkEffect;
    }

    #endregion
}
