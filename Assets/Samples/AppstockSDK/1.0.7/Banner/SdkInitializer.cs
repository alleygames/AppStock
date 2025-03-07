using System;
using AppstockSDK.Api;
using UnityEngine;

namespace AppstockSDK.Demo.Banner
{
    public class SdkInitializer : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            Debug.Log($"[{DateTime.Now:O}] Attempting to init SDK...");
            Appstock.InitializeSdk("appstock-demo");
        }
    }
}
