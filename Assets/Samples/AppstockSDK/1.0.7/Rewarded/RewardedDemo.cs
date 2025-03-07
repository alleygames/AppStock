using System;
using System.Collections;
using AppstockSDK.Api;
using UnityEngine;

#nullable enable

namespace AppstockSDK.Demo.Rewarded
{
    public class RewardedDemo : MonoBehaviour
    {
        [SerializeField] private bool showVideoAd;
        private IRewardedAd? _adUnit;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2);
            LoadRewardedAd();
        }

        private void LoadRewardedAd()
        {
            Debug.Log($"[{DateTime.Now:O}] Attempting to Load Ad...");
            _adUnit = new RewardedAd()
            {
                PlacementID = showVideoAd ? "16" : "12",
            };
            SubscribeToEvents(_adUnit);
            _adUnit.LoadAd();
        }

        private void SubscribeToEvents(IRewardedAd adUnit)
        {
            adUnit.OnAdLoaded += OnAdUnitLoaded;
            adUnit.OnAdDisplayed += OnAdUnitDisplayed;
            adUnit.OnAdFailed += OnAdUnitFailed;
            adUnit.OnAdClicked += OnAdUnitClicked;
            adUnit.OnAdClosed += OnAdUnitClosed;
            adUnit.OnReward += OnAdUnitRewarded;
        }

        private void UnsubscribeFromEvents(IRewardedAd adUnit)
        {
            adUnit.OnAdLoaded -= OnAdUnitLoaded;
            adUnit.OnAdDisplayed -= OnAdUnitDisplayed;
            adUnit.OnAdFailed -= OnAdUnitFailed;
            adUnit.OnAdClicked -= OnAdUnitClicked;
            adUnit.OnAdClosed -= OnAdUnitClosed;
            adUnit.OnReward -= OnAdUnitRewarded;
        }

        private void OnAdUnitLoaded()
        {
            Debug.Log($"[{DateTime.Now:O}] Ad Loaded.");
            ToastMessage.instance.ShowMessage("Rewarded Ad Loaded.");
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
            LoadRewardedAd();
        }
        private void OnAdUnitRewarded(AdReward? adReward) => Debug.Log($"[{DateTime.Now:O}] Ad Rewarded: {adReward}.");

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

        public void ShowRewardedAd()
        {
            if (_adUnit.Loaded)
            {
                _adUnit.Show();
            }
            else
            {
                ToastMessage.instance.ShowMessage("Rewarded Ad is not loaded.");
            }
        }
    }
}
