using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationHandler : MonoBehaviour
{
    #region singleton code
    //Miguel's really cool singleton code he made in 2020 and probably still works
    //put Singleton = this in Awake()
    private static NotificationHandler _singleton;

    //getter and setter for singleton
    public static NotificationHandler Singleton
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
                Debug.Log($"{nameof(NotificationHandler)} instance already exists, destroying duplicate");
                Destroy(value);
            }
        }
    }
    #endregion

    private void BuildDefaultNotificationChannel()
    {
        // unique channel ID
        // key used when sending a notif in game
        string channel_id = "default";

        // how it'll appear in the settings
        string title = "Default Channel";

        // importance
        Importance importance = Importance.Default;

        // description of channel
        string description = "Default channel for this game";

        AndroidNotificationChannel defaultChannel = new AndroidNotificationChannel(channel_id, title, description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);
    }

    private void BuildRepeatNotificationChannel()
    {
        string channel_id = "repeat";
        string title = "Repeat Channel";
        Importance importance = Importance.Default;
        string description = "Channel for repeating notifications";

        AndroidNotificationChannel repeatChannel = new AndroidNotificationChannel(channel_id, title, description, importance);

        AndroidNotificationCenter.RegisterNotificationChannel(repeatChannel);
    }

    private void Awake()
    {
        Singleton = this;
        BuildDefaultNotificationChannel();
        DontDestroyOnLoad(this.gameObject);
    }

    public void SendSimpleNotification()
    {
        string notif_title = "Simple Notif";
        string notif_message = "Notification Get!";
        System.DateTime fireTime = System.DateTime.Now.AddSeconds(10);

        AndroidNotification notif = new AndroidNotification(notif_title, notif_message, fireTime);

        AndroidNotificationCenter.SendNotification(notif, default);
    }

    public void SendRepeatNotification()
    {
        string notif_title = "Repeat Notif";
        string notif_message = "Repeat Notification Get!";
        System.DateTime fireTime = System.DateTime.Now.AddSeconds(10);
        System.TimeSpan timeSpan = new System.TimeSpan(0, 0, 30);   //hours, minutes, seconds

        AndroidNotification notif = new AndroidNotification(notif_title, notif_message, fireTime, timeSpan);

        AndroidNotificationCenter.SendNotification(notif, default);
    }
}
