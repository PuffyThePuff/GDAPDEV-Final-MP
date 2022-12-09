using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReLevelManager : MonoBehaviour
{
    #region singleton code
    //Miguel's really cool singleton code he made in 2020 and probably still works
    //put Singleton = this in Awake()
    private static ReLevelManager _singleton;

    //getter and setter for singleton
    public static ReLevelManager Singleton
    {
        get => _singleton;

        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
            }
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(ReLevelManager)} instance already exists, destroying duplicate");
                Destroy(value);
            }
        }
    }
    #endregion

    private void Awake()
    {
        Singleton = this;
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
