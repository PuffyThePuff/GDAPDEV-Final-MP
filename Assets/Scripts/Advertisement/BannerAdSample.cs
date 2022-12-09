using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAdSample : MonoBehaviour
{
    [SerializeField] private string _androidAdUnitID = "Banner_Android";
    [SerializeField] private string _iOSAdUnitID = "Banner_iOS";
    private string _adUnitID;

    #region singleton code
    //Miguel's really cool singleton code he made in 2020 and probably still works
    //put Singleton = this in Awake()
    private static BannerAdSample _singleton;

    //getter and setter for singleton
    public static BannerAdSample Singleton
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
                Debug.Log($"{nameof(BannerAdSample)} instance already exists, destroying duplicate");
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
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner load error: {message}");
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions{clickCallback = OnBannerClicked, showCallback = OnBannerShow, hideCallback = OnBannerHidden};

        Advertisement.Banner.Show(_adUnitID, options);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    private void OnBannerClicked()
    {

    }
    
    private void OnBannerShow()
    {

    }

    private void OnBannerHidden()
    {

    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}
