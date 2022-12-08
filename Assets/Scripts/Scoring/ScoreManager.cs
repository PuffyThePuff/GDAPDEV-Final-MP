using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public string userName = "";

    #region singleton code
    //Miguel's really cool singleton code he made in 2020 and probably still works
    //put Singleton = this in Awake()
    private static ScoreManager _singleton;

    //getter and setter for singleton
    public static ScoreManager Singleton
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
                Debug.Log($"{nameof(ScoreManager)} instance already exists, destroying duplicate");
                Destroy(value);
            }
        }
    }
    #endregion

    private void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(this);
    }

    public void addScore(int valueToAdd)
    {
        score += valueToAdd;
    }

    public void resetData()
    {
        userName = "";
        score = 0;
    }
}
