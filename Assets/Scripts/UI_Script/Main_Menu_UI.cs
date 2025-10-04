using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Main_Menu_UI : MonoBehaviour
{
    [SerializeField] private Canvas optionsMenu;

    [SerializeField] private AudioMixer audioMixer;

    // Sliders
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider brightnessSlider;

    // Volumes
    [SerializeField] private Volume volume;
    private ColorAdjustments colorAdjustments;

    // Images
    [SerializeField] private Image darkScreen;

    // Values
    private float soundVolume;
    private float brightnessVolume;

    void Start()
    {
        volume.profile.TryGet(out ColorAdjustments ca);
        colorAdjustments = ca;

        SetBrightness();
        SetSoundValue();
    }

    public void SetBrightness()
    {
        brightnessVolume = brightnessSlider.value;
        colorAdjustments.postExposure.value = Mathf.Lerp(-100f, 100f, brightnessVolume);
    }

    public void SetSoundValue()
    {
        soundVolume = soundSlider.value;
        audioMixer.SetFloat("MasterVolume", soundVolume);
    }

    public void OpenOptions()
    {
        optionsMenu.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        darkScreen.GetComponent<Animator>().SetBool("isPressed", true);
        yield return new WaitForSeconds(2.9f);
        SceneManager.LoadScene("Level_01");
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Oyundan çikiliyor");
        Application.Quit();
    }
}
