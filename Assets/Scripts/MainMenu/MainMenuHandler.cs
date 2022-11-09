using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{

    public void OnPlayPressed()
    {
        Debug.Log("Entering Game");
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitPressed()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
