using System;
using System.Collections;
using AppstockSDK.Api;
using UnityEngine;

#nullable enable

namespace AppstockSDK.Demo.Interstitial
{
    public class InterstitialDemo : MonoBehaviour
    {
        [SerializeField] private bool showVideoAd;
        private IInterstitialAd? _adUnit;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2);
            LoadInterstitialAd();
        }

        private void LoadInterstitialAd()
        {
            Debug.Log($"[{DateTime.Now:O}] Attempting to Load Ad...");
            _adUnit = showVideoAd
                ? new InterstitialAd()
                {
                    PlacementID = "7",
                    AdUnitFormats = new []{ AdFormat.Video },
                }
                : new InterstitialAd()
                {
                    PlacementID = "5",
                };
            SubscribeToEvents(_adUnit);
            _adUnit.LoadAd();
        }

        private void SubscribeToEvents(IInterstitialAd adUnit)
        {
            adUnit.OnAdLoaded += OnAdUnitLoaded;
            adUnit.OnAdDisplayed += OnAdUnitDisplayed;
            adUnit.OnAdFailed += OnAdUnitFailed;
            adUnit.OnAdClicked += OnAdUnitClicked;
            adUnit.OnAdClosed += OnAdUnitClosed;
        }

        private void UnsubscribeFromEvents(IInterstitialAd adUnit)
        {
            adUnit.OnAdLoaded -= OnAdUnitLoaded;
            adUnit.OnAdDisplayed -= OnAdUnitDisplayed;
            adUnit.OnAdFailed -= OnAdUnitFailed;
            adUnit.OnAdClicked -= OnAdUnitClicked;
            adUnit.OnAdClosed -= OnAdUnitClosed;
        }

        private void OnAdUnitLoaded()
        {
            Debug.Log($"[{DateTime.Now:O}] Ad Loaded.");
            ToastMessage.instance.ShowMessage("Interstitial Ad Loaded.");
            // _adUnit?.Show();
        }

        private void OnAdUnitFailed(AdError? adError)
        {
            Debug.LogError($"[{DateTime.Now:O}] Ad Failed: {adError?.Message}.");
            DropAdUnit();
        }

        private void OnAdUnitDisplayed() => Debug.Log($"[{DateTime.Now:O}] Ad Displayed.");
        private void OnAdUnitClicked() => Debug.Log($"[{DateTime.Now:O}] Ad Clicked.");

        private void OnAdUnitClosed()
        {
            Debug.Log($"[{DateTime.Now:O}] Ad Closed.");
            DropAdUnit();
            
            LoadInterstitialAd();
        }

        private void DropAdUnit()
        {
            if (_adUnit is null)
            {
                return;
            }
            UnsubscribeFromEvents(_adUnit);
            _adUnit.Dispose();
            _adUnit = null;
        }

        public void ShowInterstitialAd()
        {
            if (_adUnit.Loaded)
            {
                _adUnit.Show();
            }
            else
            {
                ToastMessage.instance.ShowMessage("Interstitial Ad is not loaded.");
            }
        }

    }
}
