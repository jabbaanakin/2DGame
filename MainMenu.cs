using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsCanvas;
    private bool _isPushed = true;
    public void StartHandler()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingHandler()
    {
        settingsCanvas.SetActive(true);
    }
    public void BackToMenuHandler()
    {
        settingsCanvas.SetActive(false);
    }
    public void ExitHandler()
    {
        Application.Quit();
    }
    public void SoundsHandler()
    {
        if (_isPushed)
        {
            AudioListener.volume = 0.0f;
            _isPushed = false;
        }
        else
        {
            AudioListener.volume = 1.0f;
            _isPushed = true;
        }
    }
}
