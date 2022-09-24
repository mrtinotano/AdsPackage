using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Ads
{
    [CreateAssetMenu(fileName = "App Ads Settings", menuName = "MrTinoTano/Ads/Ads Settings")]
    public class AppAdsSettings : SingletonScriptableObject<AppAdsSettings>
    {
        public string AndroidAppID;
        public string iOSAppID;

        [Header("Banner Settings")]
        public bool UseBanner;
        [ConditionalHide("UseBanner")] public BannerSettings[] Banners;
    }
}
