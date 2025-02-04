using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onStartPress()
    {
        SceneManager.LoadScene("Main");
    }

    public void onExitPress()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
}
