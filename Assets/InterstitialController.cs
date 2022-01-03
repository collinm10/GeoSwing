using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class InterstitialController : MonoBehaviour
{
    private InterstitialAd interstitial; 

    void Start()
    {
        CreateAndLoadAd();
    }

    public void CreateAndLoadAd()
    {
        interstitial = new InterstitialAd("ca-app-pub-3940256099942544/4411468910");

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleInterstitialAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleInterstitialAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleInterstitialAdOpening;
        // Called when an ad request failed to show.
        this.interstitial.OnAdFailedToShow += HandleInterstitialAdFailedToShow;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleInterstitialAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the Interstitial ad with the request.
        this.interstitial.LoadAd(request);
    }

    // Load content to the Ad Unit:
    public void ShowAd()
    {
        this.interstitial.Show();
    }

    public void HandleInterstitialAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialAdLoaded event received");
    }

    public void HandleInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print("HandleInterstitialAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialAdOpening event received");
    }

    public void HandleInterstitialAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleInterstitialAdClosed(object sender, EventArgs args)
    {
        CreateAndLoadAd();
        MonoBehaviour.print("HandleInterstitialAdClosed event received");
    }

}