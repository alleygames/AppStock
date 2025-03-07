using System;
using AppstockSDK.Api;
using UnityEngine;

#nullable enable

namespace AppstockSDK.Demo.Banner
{
    public class BannerDemo : MonoBehaviour
    {
        [SerializeField] private AnchoredAdPosition bannerPosition;

        private IBannerAd? _adUnit;

        #region UnityEvents

        private void Start()
        {
            Invoke(nameof(LoadBannerAd),1f);
        }

        private void LoadBannerAd()
        {
            Debug.Log($"[{DateTime.Now:O}] Attempting to Load Ad...");
            _adUnit = new BannerAd(new(320, 250))
            {
                PlacementID = "4",
                AdUnitFormat = AdFormat.Banner,
                AnchoredPosition = bannerPosition,
                AdPosition = bannerPosition.ToAdPosition(),
            };
            
            SubscribeToEvents(_adUnit);
            _adUnit.LoadAd();
            SetHidden(true);
        }

        #endregion

        #region PublicAPI

        public void SetHidden(bool hidden)
        {
            if (_adUnit is null)
            {
                return;
            }
            if (hidden)
            {
                _adUnit.Hide();
            }
            else
            {
                _adUnit.Show();
            }
        }

        public void DropAdUnit()
        {
            if (_adUnit is null)
            {
                return;
            }
            UnsubscribeFromEvents(_adUnit);
            _adUnit.Dispose();
            _adUnit = null;
        }

        #endregion

        #region Callbacks

        private void SubscribeToEvents(IBannerAd adUnit)
        {
            adUnit.OnAdLoaded += OnAdUnitLoaded;
            adUnit.OnAdFailed += OnAdUnitFailed;
            adUnit.OnAdClicked += OnAdUnitClicked;
            adUnit.OnAdClosed += OnAdUnitClosed;
        }

        private void UnsubscribeFromEvents(IBannerAd adUnit)
        {
            adUnit.OnAdLoaded -= OnAdUnitLoaded;
            adUnit.OnAdFailed -= OnAdUnitFailed;
            adUnit.OnAdClicked -= OnAdUnitClicked;
            adUnit.OnAdClosed -= OnAdUnitClosed;
        }

        private void OnAdUnitLoaded()
        {
            Debug.Log($"[{DateTime.Now:O}] (video: ) Ad Loaded.");
            ToastMessage.instance.ShowMessage("Banner Ad Loaded.");
        }

        private void OnAdUnitFailed(AdError? adError)
        {
            Debug.LogError($"[{DateTime.Now:O}] (video: ) Ad Failed: {adError?.Message}.");
        }

        private void OnAdUnitClicked() => Debug.Log($"[{DateTime.Now:O}] (video: ) Ad Clicked.");

        private void OnAdUnitClosed()
        {
            Debug.Log($"[{DateTime.Now:O}] (video: ) Ad Closed.");
        }

        #endregion
    }
}
