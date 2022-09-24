using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Ads
{
    public class AdsManager : Singleton<AdsManager>
    {
        private void Start()
        {
            string appID = string.Empty;

#if UNITY_ANDROID
            appID = AppAdsSettings.Instance.AndroidAppID;
#elif UNITY_IOS
            appID = AppAdsSettings.Instance.iOSAppID;
#endif

            IronSource.Agent.validateIntegration();

            IronSource.Agent.init(appID);
        }

        private void OnEnable()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent += OnSdkInitializationCompletedEvent;

            if (AppAdsSettings.Instance.UseBanner)
                IronSourceEvents.onBannerAdLoadedEvent += OnBannerAdLoadedEvent;
        }

        private void OnDisable()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent -= OnSdkInitializationCompletedEvent;

            if (AppAdsSettings.Instance.UseBanner)
                IronSourceEvents.onBannerAdLoadedEvent -= OnBannerAdLoadedEvent;
        }

        private void OnSdkInitializationCompletedEvent()
        {
            Debug.Log("SDK Init");
            if (AppAdsSettings.Instance.UseBanner)
                LoadBanner();
        }

        private void LoadBanner()
        {
            BannerSettings banner = AppAdsSettings.Instance.Banner;

            IronSourceBannerSize size = null;

            switch (banner.Size)
            {
                case BannerSettings.BannerSize.BANNER:
                    size = IronSourceBannerSize.BANNER;
                    break;
                case BannerSettings.BannerSize.LARGE:
                    size = IronSourceBannerSize.LARGE;
                    break;
                case BannerSettings.BannerSize.RECTANGLE:
                    size = IronSourceBannerSize.RECTANGLE;
                    break;
                case BannerSettings.BannerSize.SMART:
                    size = IronSourceBannerSize.SMART;
                    break;
                case BannerSettings.BannerSize.CUSTOM:
                    size = new IronSourceBannerSize((int)banner.CustomSize.x, (int)banner.CustomSize.y);
                    break;
            }

            IronSource.Agent.loadBanner(size, banner.BannerPosition);

            if (!banner.ShowImmediate)
                IronSource.Agent.hideBanner();
        }

        private void OnBannerAdLoadedEvent()
        {
            Debug.Log("Banner Loaded");
        }

    }
}
