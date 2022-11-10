using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    //cheats, add more if needed
    public static int startingMoney = 0;
    public static bool infiniteHealth = false;

    //notification interval time
    public static float notificationIntervalTime = 0f;

    [SerializeField] private GameObject ConfigMenu;

    #region singleton code
    //Miguel's really cool singleton code he made in 2020 and probably still works
    //put Singleton = this in Awake()
    private static Config _singleton;

    //getter and setter for singleton
    public static Config Singleton
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
                Debug.Log($"{nameof(Config)} instance already exists, destroying duplicate");
                Destroy(value);
            }
        }
    }
    #endregion

    private void Awake()
    {
        Singleton = this;

        DontDestroyOnLoad(this.gameObject);
    }
    
    //toggle options menu
    public void OnConfigTogglePressed()
    {
        ConfigMenu.SetActive(!ConfigMenu.activeInHierarchy);
    }

    //toggle value of infiniteHealth
    public void OnInfiniteHealthToggle()
    {
        infiniteHealth = !infiniteHealth;
    }

    public void OnGenerateNotificationPressed()
    {
        NotificationHandler.Singleton.SendSimpleNotification();
    }
}
