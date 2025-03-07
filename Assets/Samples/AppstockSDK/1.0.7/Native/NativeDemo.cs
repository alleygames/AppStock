using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AppstockSDK.Api;
using AppstockSDK.Api.Native.Data.Response;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Assertions;

#nullable enable

namespace AppstockSDK.Demo.Native
{
    public class NativeDemo : MonoBehaviour
    {
        [SerializeField] private NativeConfigPreset? nativeConfig;
        [SerializeField] private TMP_Text? text;
        private INativeAdLoader? _nativeAdLoader;
        private INativeAd? _nativeAd;
        
        #region UnityMessages
        
        private IEnumerator Start()
        {
            Appstock.InitializeSdk("appstock");
            
            // If the user opts in to targeted advertising:
            MetaData gdprMetaData = new MetaData("gdrp");
            
            gdprMetaData.Set("consent", "true");
            Advertisement.SetMetaData(gdprMetaData);
//
// // If the user opts out of targeted advertising:
//             MetaData gdprMetaData = new MetaData("gdpr");
//             gdprMetaData.Set("consent", "false");
//             Advertisement.SetMetaData(gdprMetaData)

            // Wait for `SdkInitializer` to init SDK.
            yield return new WaitForSeconds(1);
            if (nativeConfig == null)
            {
                yield break;
            }
            _nativeAdLoader = nativeConfig.BuildAdLoader();
            _nativeAdLoader.LoadAd(OnAdLoadResult);
        }

        private void OnValidate() => Assert.IsNotNull(nativeConfig, $"{nameof(nativeConfig)} should not be null.");
        
        #endregion

        #region NativeAdProcessing

        private void OnAdLoadResult(INativeAd? nativeAd, AdError? status)
        {
            using (_nativeAdLoader)
            {
                _nativeAdLoader = null;
                if (nativeAd is null)
                {
                    print($"Failed to load ad : {status?.Message}");
                    LogToUI($"Failed to load ad: {status?.Message}", LogType.Error);
                    return;
                }
                if (status?.Message is { } message)
                {
                    LogToUI($"Ad loading status: {message}", LogType.Warning);
                }
                _nativeAd = nativeAd;
                SubscribeToEvents(nativeAd);
                DumpNativeAdContent(nativeAd);
                nativeAd.RegisterView(gameObject, null);
            }
        }

        private void DumpNativeAdContent(INativeAd nativeAd)
            => LogToUI("NativeAd Loaded\n" + string.Join('\n', 
                DumpNativeAdProperties(nativeAd).Select(x => $" - {x.Key}: {x.Value}")));

        private static IEnumerable<KeyValuePair<string, string?>> DumpNativeAdProperties(INativeAd nativeAd)
        {
            var i = 0;
            foreach (var nextTitle in nativeAd.Titles)
            {
                using (nextTitle)
                {
                    yield return new KeyValuePair<string, string?>(
                        $"{nameof(INativeAd.Titles)}[{i++}].{nameof(ITitleContent.Text)}",
                        nextTitle?.Text);
                }
            }
            i = 0;
            foreach (var nextImage in nativeAd.Images)
            {
                using (nextImage)
                {
                    yield return new KeyValuePair<string, string?>(
                        $"{nameof(INativeAd.Images)}[{i}].{nameof(IImageContent.ImageType)}",
                        nextImage?.ImageType is {} type ? $"{type} ({(int)type})" : null);
                    yield return new KeyValuePair<string, string?>(
                        $"{nameof(INativeAd.Images)}[{i}].{nameof(IImageContent.URL)}",
                        nextImage?.URL);
                    ++i;
                }
            }
            i = 0;
            foreach (var nextData in nativeAd.DataObjects)
            {
                using (nextData)
                {
                    yield return new KeyValuePair<string, string?>(
                        $"{nameof(INativeAd.DataObjects)}[{i}].{nameof(IDataContent.DataType)}",
                        nextData?.DataType is {} type ? $"{type} ({(int)type})" : null);
                    yield return new KeyValuePair<string, string?>(
                        $"{nameof(INativeAd.DataObjects)}[{i}].{nameof(IDataContent.Value)}",
                        nextData?.Value);
                    ++i;
                }
            }
            yield return new KeyValuePair<string, string?>(nameof(INativeAd.Title), nativeAd.Title);
            yield return new KeyValuePair<string, string?>(nameof(INativeAd.ImageUrl), nativeAd.ImageUrl);
            yield return new KeyValuePair<string, string?>(nameof(INativeAd.IconUrl), nativeAd.IconUrl);
            yield return new KeyValuePair<string, string?>(nameof(INativeAd.SponsoredBy), nativeAd.SponsoredBy);
            yield return new KeyValuePair<string, string?>(nameof(INativeAd.CallToAction), nativeAd.CallToAction);
        }

        #endregion

        #region AdCallbacks

        private void SubscribeToEvents(INativeAd nativeAd)
        {
            nativeAd.OnAdClicked += OnAdClicked;
            nativeAd.OnAdImpression += OnAdImpression;
            nativeAd.OnAdExpired += OnAdExpired;
        }

        private void UnsubscribeFromEvents(INativeAd nativeAd)
        {
            nativeAd.OnAdClicked -= OnAdClicked;
            nativeAd.OnAdImpression -= OnAdImpression;
            nativeAd.OnAdExpired -= OnAdExpired;
        }

        private void OnAdClicked() => LogToUI("Ad Clicked.");
        private void OnAdImpression() => LogToUI("Ad Impression.");

        private void OnAdExpired()
        {
            LogToUI("Ad Expired.");
            using (_nativeAd)
            {
                if (_nativeAd is not null)
                {
                    UnsubscribeFromEvents(_nativeAd);
                    _nativeAd.Dispose();
                    _nativeAd = null;
                }
            }
        }
        
        #endregion

        private void LogToUI(string message, LogType logType = LogType.Log)
        {
            if (text != null)
            {
                text.text += "\n" + message;
            }
            Debug.LogFormat(logType, LogOption.None, gameObject, "[{0:O}] {1}", DateTime.Now, message);
        }
    }
}
