using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private uint counter = 1;

    #region singleton code
    //Miguel's really cool singleton code he made in 2020 and probably still works
    //put Singleton = this in Awake()
    private static LeaderboardManager _singleton;

    //getter and setter for singleton
    public static LeaderboardManager Singleton
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
                Debug.Log($"{nameof(LeaderboardManager)} instance already exists, destroying duplicate");
                Destroy(value);
            }
        }
    }
    #endregion

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        WebHandler.Singleton.GetPlayerScores();
    }

    public void AddToLeaderboard(string playerData)
    {
        GameObject listObject = Instantiate(prefab, this.gameObject.transform);

        listObject.GetComponent<TMP_Text>().text = counter.ToString() + playerData;

        counter++;
    }
}
