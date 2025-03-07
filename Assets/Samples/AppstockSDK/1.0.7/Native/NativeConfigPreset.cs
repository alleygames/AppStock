using System;
using AppstockSDK.Api;
using UnityEngine;
using AppstockSDK.Api.Native.Data.Request;

#if UNITY_EDITOR
using UnityEditor;
#endif

#nullable enable

namespace AppstockSDK.Demo.Native
{
    [Serializable]
    [CreateAssetMenu(fileName = "NativeConfigPreset", menuName = "AppstockSDK/Native Config Preset")]
    public class NativeConfigPreset : ScriptableObject
    {
        public ConfigWarnings warnings;
        public AdUnitData adUnitData;
        
        public INativeAdLoader BuildAdLoader()
            => adUnitData.BuildAdLoader();

        #if UNITY_EDITOR
        public void OnValidate()
        {
            warnings.warnings?.Clear();
            var path = AssetDatabase.GetAssetPath(this);
            foreach (var warning in adUnitData.Warnings)
            {
                warnings.warnings?.Add(warning);
            }
            var warningsCount = warnings.warnings?.Count ?? 0;
            if (warningsCount == 0) {
                return;
            }
            Debug.LogWarning($"Found {warningsCount} warning(s) in: {path}", context: this);
        }
        #endif
    }
}
