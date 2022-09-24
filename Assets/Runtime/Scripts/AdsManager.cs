using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Ads
{
    public class AdsManager : Singleton<AdsManager>
    {
        protected override void Awake()
        {
            base.Awake();

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
                IronSource.Agent.loadBanner(AppAdsSettings.Instance.BannerSize, AppAdsSettings.Instance.BannerPosition);
        }

        private void OnBannerAdLoadedEvent()
        {
            Debug.Log("Banner Loaded");
        }
    }
}
