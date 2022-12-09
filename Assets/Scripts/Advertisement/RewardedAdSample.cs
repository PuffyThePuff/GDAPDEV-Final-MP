using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdSample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnitID = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitID = "Rewarded_iOS";
    private string _adUnitID;

    #region singleton code
    //Miguel's really cool singleton code he made in 2020 and probably still works
    //put Singleton = this in Awake()
    private static RewardedAdSample _singleton;

    //getter and setter for singleton
    public static RewardedAdSample Singleton
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
                Debug.Log($"{nameof(RewardedAdSample)} instance already exists, destroying duplicate");
                Destroy(value);
            }
        }
    }
    #endregion

    private void Awake()
    {
        Singleton = this;

        _adUnitID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSAdUnitID : _androidAdUnitID;
    }

    private void Start()
    {
        StartCoroutine(WaitForAdsManagerInitialized());
    }

    IEnumerator WaitForAdsManagerInitialized()
    {
        yield return new WaitUntil(() => Advertisement.isInitialized);
        LoadAd();
    }

    // NOTE: only load when Advertisments class has been intialized, otherwise it will result in an error
    public void LoadAd()
    {
        Debug.Log("Loading ad: " + _adUnitID);
        Advertisement.Load(_adUnitID, this);
    }

    public void ShowAd()
    {
        Debug.Log("Showing ad: " + _adUnitID);
        Advertisement.Show(_adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"{placementId} has been loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"{placementId} failed to load: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Unity ads show complete");

        if (placementId.Equals(_adUnitID))
        {
            // ADD REWARD HERE
            Debug.Log("Ad watch completed");

            GameOverManager.Instance.RevivePlayer();
        }

        // load another for next time
        Advertisement.Load(_adUnitID, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing ad unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }
}
