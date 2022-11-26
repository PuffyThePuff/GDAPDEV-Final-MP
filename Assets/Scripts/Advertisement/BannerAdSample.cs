using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAdSample : MonoBehaviour
{
    [SerializeField] private Button _showBannerButton;
    [SerializeField] private Button _hideBannerButton;
    
    [SerializeField] private string _androidAdUnitID = "Banner_Android";
    [SerializeField] private string _iOSAdUnitID = "Banner_iOS";
    private string _adUnitID;

    private void Awake()
    {
        _adUnitID = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSAdUnitID : _androidAdUnitID;
    }

    IEnumerator WaitForAdsManagerInitialized()
    {
        yield return new WaitUntil(() => Advertisement.isInitialized);
        LoadBanner();
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions{loadCallback = OnBannerLoaded, errorCallback = OnBannerError};

        Advertisement.Banner.Load(_adUnitID, options);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner Loaded");
        _showBannerButton.interactable = true;
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner load error: {message}");
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions{clickCallback = OnBannerClicked, showCallback = OnBannerShow, hideCallback = OnBannerHidden};

        Advertisement.Banner.Show(_adUnitID, options);
    }

    private void OnBannerClicked()
    {

    }
    
    private void OnBannerShow()
    {
        _showBannerButton.interactable = false;
        _hideBannerButton.interactable = true;
    }

    private void OnBannerHidden()
    {
        _showBannerButton.interactable = true;
        _hideBannerButton.interactable = false;
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}
