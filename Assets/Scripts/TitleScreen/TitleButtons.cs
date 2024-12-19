using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        AudioSettings.instance.ToggleSettings();
    }
}
