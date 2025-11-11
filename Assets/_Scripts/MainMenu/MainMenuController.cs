using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas, optionsCanvas;
    [SerializeField] private Slider ambienceSlider, musicSlider;
    private void Start()
    {
        OnBackClicked();
        SoundManager.Instance.PlayMusic(AudioMusic.IntroMusic);
        OnAMusicVolumeChange();
        OnAmbienceVolumeChange();
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(AppScenes.LOADING_SCENE);
    }
    
    public void OnOptionsClicked() 
    {
        mainCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void OnExitClicked()
    {
        Debug.Log("Game was exited.");
        Application.Quit();
    }

    public void OnAmbienceVolumeChange()
    {
        SoundManager.Instance.SetAmbienceVolume(ambienceSlider.value);
    }

    public void OnAMusicVolumeChange()
    {
        SoundManager.Instance.SetMusicVolume(musicSlider.value);
    }
    public void OnBackClicked()
    {
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

}
