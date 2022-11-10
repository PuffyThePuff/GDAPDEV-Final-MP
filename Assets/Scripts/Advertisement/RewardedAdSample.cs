using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdSample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button _showAdButton;
    [SerializeField] private Button _hideAdButton;

    [SerializeField] private string _androidAdUnitID = "Interstitial_Android";
    [SerializeField] private string _iOSAdUnitID = "Interstitial_iOS";
    private string _adUnitID;

    private void Awake()
    {
        _adUnitID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSAdUnitID : _androidAdUnitID;

        _showAdButton.interactable = false;
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
        _showAdButton.interactable = false;
        Advertisement.Show(_adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"{placementId} has been loaded");
        _showAdButton.interactable = true;
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

        if (placementId.Equals(_adUnitID) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            // ADD REWARD HERE
            Debug.Log("Ad watch completed");
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
        throw new System.NotImplementedException();
    }
}
