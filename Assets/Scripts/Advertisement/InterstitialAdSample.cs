using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdSample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnitID = "Interstitial_Android";
    [SerializeField] private string _iOSAdUnitID = "Interstitial_iOS";
    private string _adUnitID;

    private void Awake()
    {
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

    public void OnUnityAdsAdLoaded(string placementID)
    {
        Debug.Log($"{placementID} has been loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementID, UnityAdsLoadError error, string message)
    {
        Debug.Log($"{placementID} failed to load: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing ad unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        
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

        // load for next time ShowAd() is called
        LoadAd();
    }
}
