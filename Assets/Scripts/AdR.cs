using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public static class AdR
{
    private static AdRequest adRequest;

    private static InterstitialAd interstitialAdAfterMMLose;
    private static BannerView banner;
    public static RewardBasedVideoAd fortuneVideoAd;

    private const string MM_AFTER_LOSE = "ca-app-pub-6132055699566134/7556123215";
    private const string BANNER = "ca-app-pub-6132055699566134/2011304069";
    private const string FORTUNE_VIDEO = "ca-app-pub-6132055699566134/4217968847";

    public static void Initialization(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(InitializationIE());
    }

    private static void Init()
    {
        if (GameController.settings.NoAds != Boolean.False)
            return;
        adRequest = new AdRequest.Builder().Build();

        banner = new BannerView(BANNER, AdSize.Banner, AdPosition.Bottom);
        banner.LoadAd(adRequest);

        interstitialAdAfterMMLose = new InterstitialAd(MM_AFTER_LOSE);
        interstitialAdAfterMMLose.LoadAd(adRequest);

        interstitialAdAfterMMLose.OnAdClosed += OnAdClosed_interstitialAdAfterMMLose;
        interstitialAdAfterMMLose.OnAdFailedToLoad += OnAdFailed_interstitialAdAfterMMLose;

        fortuneVideoAd = RewardBasedVideoAd.Instance;
        fortuneVideoAd.LoadAd(adRequest, FORTUNE_VIDEO);

        fortuneVideoAd.OnAdClosed += OnAdClosed_fortuneVideoAd;
        fortuneVideoAd.OnAdFailedToLoad += OnAdFailed_fortuneVideoAd;
        fortuneVideoAd.OnAdLoaded += OnAdLoaded_fortuneVideoAd;
        fortuneVideoAd.OnAdRewarded += GameController.mainMenu.OnClickRotateButtonInRotate;

        GameController.mainMenuCause += AfterMMLose;
        GameController.mainMenuCause += MainM;

        GameController.playingCause += Play;
    }

    private static IEnumerator InitializationIE()
    {
        while (GameController.settings.NoAds == Boolean.Other)
            yield return 0;
        Init();
    }

    private static void Play()
    {
        banner.Hide();
    }

    private static void MainM()
    {
        if (GameController.settings.NoAds != Boolean.False)
            return;
        banner.Show();
    }
    
    public static void AfterMMLose()
    {
        if (GameController.settings.NoAds != Boolean.False)
            return;
        if (Random.Range(0,2) == 0)
        interstitialAdAfterMMLose.Show();
    }

    private static void OnAdClosed_fortuneVideoAd(object sender, System.EventArgs args)
    {
        fortuneVideoAd.LoadAd(adRequest, FORTUNE_VIDEO);
        GameController.mainMenu.fortune.rotateButton.interactable = false;
    }

    private static void OnAdLoaded_fortuneVideoAd(object sender, System.EventArgs args)
    {
        GameController.mainMenu.fortune.rotateButton.interactable = true;
    }

    private static void OnAdFailed_fortuneVideoAd(object sender, System.EventArgs args)
    {
        fortuneVideoAd.LoadAd(adRequest, FORTUNE_VIDEO);
    }

    private static void OnAdClosed_interstitialAdAfterMMLose(object sender, System.EventArgs args)
    {
        interstitialAdAfterMMLose.LoadAd(adRequest);
    }

    private static void OnAdFailed_interstitialAdAfterMMLose(object sender, System.EventArgs args)
    {
        interstitialAdAfterMMLose.LoadAd(adRequest);
    }
}
