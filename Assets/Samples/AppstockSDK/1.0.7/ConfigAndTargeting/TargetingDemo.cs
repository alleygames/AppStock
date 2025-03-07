using System;
using System.Collections;
using System.Linq;
using AppstockSDK.Api;
using UnityEngine;

#nullable enable

namespace AppstockSDK.DevApp
{
    public class TargetingDemo : MonoBehaviour
    {
        public SdkConfig sdkConfig = new();
        public TargetingData targetingData = new();
        
        // Start is called before the first frame update
        private IEnumerator Start()
        {
            Debug.Log($"[{DateTime.Now:O}] Applying config...");
            Appstock.Sdk.Apply(sdkConfig);
            
            Debug.Log($"[{DateTime.Now:O}] Attempting to init SDK...");
            Appstock.InitializeSdk("appstock-demo");

            Debug.Log($"[{DateTime.Now:O}] Letting SDK init to finish...");
            yield return new WaitForSeconds(1);
            
            Debug.Log($"[{DateTime.Now:O}] Applying targeting data...");
            Appstock.Targeting.Apply(targetingData);
            
            Debug.Log($"[{DateTime.Now:O}] Attempting to load Ad...");
            var ad = new InterstitialAd
            {
                PlacementID = string.IsNullOrWhiteSpace(sdkConfig.endpointID) ? "5" : null,
            };
            ad.OnAdLoaded += () =>
            {
                Debug.Log($"[{DateTime.Now:O}] Ad Loaded.");
                ad.Show();
            };
            ad.OnAdFailed += error => Debug.Log($"[{DateTime.Now:O}] Ad Failed -- {error?.Message}");
            ad.LoadAd();
            
            ConfirmConfigsApplied();
        }

        private void ConfirmConfigsApplied()
        {
            {
                var sdkConfigSnapshot = new SdkConfigSnapshot(Appstock.Sdk);
                var snapshotJson = JsonUtility.ToJson(sdkConfigSnapshot, prettyPrint: true);
                Debug.Log($"(snapshot) {snapshotJson}");
                
                var unequalFields = sdkConfigSnapshot.UnequalFields(sdkConfig).ToList(); 
                if (unequalFields.Any())
                {
                    var configJson = JsonUtility.ToJson(sdkConfig, prettyPrint: true);
                    Debug.Log($"(config) {configJson}");
                    Debug.LogWarning($"[DIFF-FIELDS] ({unequalFields.Count}): {string.Join(", ", unequalFields)}.");
                }
                else
                {
                    Debug.Log("SDK config applied successfully.");
                }
            }
            {
                var targetingSnapshot = Appstock.Targeting.TakeSnapshot();
                var snapshotJson = JsonUtility.ToJson(targetingSnapshot, prettyPrint: true);
                Debug.Log($"(snapshot) {snapshotJson}");
                
                var unequalFields = targetingData.UnequalFields(targetingSnapshot, fieldsToIgnore: new[]
                {
                    nameof(targetingData.userExtJson) // the ordering is not guaranteed upon retrieval 
                }).ToList();
                if (unequalFields.Any())
                {
                    var configJson = JsonUtility.ToJson(targetingData, prettyPrint: true);
                    Debug.Log($"(config) {configJson}");
                    Debug.LogWarning($"[DIFF-FIELDS] ({unequalFields.Count}): {string.Join(", ", unequalFields)}.");
                }
                else
                {
                    Debug.Log("Targeting data applied successfully.");
                }
            }
        }
    }
}
