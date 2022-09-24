using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Ads
{
    [CreateAssetMenu(fileName = "Banner Ad Settings", menuName = "MrTinoTano/Ads/Banner Settings")]
    public class BannerSettings : ScriptableObject
    {
        public enum BannerSize
        {
            BANNER,
            LARGE,
            RECTANGLE,
            SMART,
            CUSTOM
        }

        public BannerSize Size;
        public IronSourceBannerPosition BannerPosition;
        public bool ShowImmediate;
        [EnumOptionShow("Size", "CUSTOM")] public Vector2 CustomSize;
    }
}
