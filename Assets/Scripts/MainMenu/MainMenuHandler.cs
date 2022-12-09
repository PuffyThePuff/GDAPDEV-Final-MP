using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{

    public void OnPlayPressed()
    {
        Debug.Log("Entering Game");
		PlayerDataManager.instance.newGame();
        SceneManager.LoadScene(1);
    }

    public void OnQuitPressed()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
