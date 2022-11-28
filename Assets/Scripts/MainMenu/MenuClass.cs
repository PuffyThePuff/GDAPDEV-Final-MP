using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuClass : MonoBehaviour
{
    public virtual void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public virtual void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public virtual void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
