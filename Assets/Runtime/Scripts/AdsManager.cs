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

            IronSource.Agent.init(appID);

            if(AppAdsSettings.Instance.UseBanner)
                IronSource.Agent.loadBanner(AppAdsSettings.Instance.BannerSize, AppAdsSettings.Instance.BannerPosition);
        }

        private void OnEnable()
        {
            if (AppAdsSettings.Instance.UseBanner)
                IronSourceEvents.onBannerAdLoadedEvent += OnBannerAdLoadedEvent;
        }

        private void OnDisable()
        {
            if (AppAdsSettings.Instance.UseBanner)
                IronSourceEvents.onBannerAdLoadedEvent -= OnBannerAdLoadedEvent;
        }

        private void OnBannerAdLoadedEvent()
        {
            Debug.Log("Banner Loaded");
            IronSource.Agent.displayBanner();
        }
    }
}
