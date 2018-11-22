using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyMobile
{
    #if UNITY_ADS
    using UnityEngine.Advertisements;
    #endif

    public class UnityAdsClientImpl : AdClientImpl
    {
        private const string NO_SDK_MESSAGE = "SDK missing. Please enable UnityAds service.";
        private const string BANNER_UNSUPPORTED_MESSAGE = "UnityAds does not support banner ad format.";

        #if UNITY_ADS

        private UnityAdsSettings mAdSettings;

        #endif

        #region UnityAds Events

        #if UNITY_ADS

        public event Action<ShowResult, AdPlacement> InterstitialAdCallback;
        public event Action<ShowResult, AdPlacement> RewardedAdCallback;

        #endif

        #endregion

        #region Singleton

        private static UnityAdsClientImpl sInstance;

        private UnityAdsClientImpl()
        {
        }

        /// <summary>
        /// Returns the singleton client.
        /// </summary>
        /// <returns>The client.</returns>
        public static UnityAdsClientImpl CreateClient()
        {
            if (sInstance == null)
            {
                sInstance = new UnityAdsClientImpl();
            }
            return sInstance;
        }

        #endregion  // Object Creators

        #region AdClient Overrides

        public override AdNetwork Network { get { return AdNetwork.UnityAds; } }

        public override bool IsBannerAdSupported { get { return false; } }

        public override bool IsInterstitialAdSupported { get { return true; } }

        public override bool IsRewardedAdSupported { get { return true; } }

        public override bool IsInitialized
        {
            get
            {
                #if UNITY_ADS
                return mIsInitialized && Advertisement.isInitialized;
                #else
                return mIsInitialized;
                #endif
            }
        }

        protected override Dictionary<AdPlacement, AdId> CustomInterstitialAdsDict
        {
            get
            {
                #if UNITY_ADS
                return mAdSettings == null ? null : mAdSettings.CustomInterstitialAdIds;
                #else
                return null;
                #endif
            }
        }

        protected override Dictionary<AdPlacement, AdId> CustomRewardedAdsDict
        {
            get
            {
                #if UNITY_ADS
                return mAdSettings == null ? null : mAdSettings.CustomRewardedAdIds;
                #else
                return null;
                #endif
            }
        }

        protected override string NoSdkMessage { get { return NO_SDK_MESSAGE; } }

        public override bool IsSdkAvail
        {
            get
            {
                #if UNITY_ADS
                return true;
                #else
                return false;
                #endif
            }
        }

        protected override void InternalInit()
        {
            #if UNITY_ADS
            if (!mIsInitialized)
            {
                mIsInitialized = true;
                mAdSettings = EM_Settings.Advertising.UnityAds;

                // Set GDPR consent (if any).
                var consent = GetApplicableDataPrivacyConsent();
                ApplyDataPrivacyConsent(consent);

                Debug.Log("UnityAds client has been initialized.");
            }
            #endif

            // The rest of the initialization is done automatically by Unity.
        }

        //------------------------------------------------------------
        // Banner Ads.
        //------------------------------------------------------------

        protected override void InternalShowBannerAd(AdPlacement _, BannerAdPosition __, BannerAdSize ___)
        {
            Debug.LogWarning(BANNER_UNSUPPORTED_MESSAGE);
        }

        protected override void InternalHideBannerAd(AdPlacement _)
        {
            Debug.LogWarning(BANNER_UNSUPPORTED_MESSAGE);
        }

        protected override void InternalDestroyBannerAd(AdPlacement _)
        {
            Debug.LogWarning(BANNER_UNSUPPORTED_MESSAGE);
        }

        //------------------------------------------------------------
        // Interstitial Ads.
        //------------------------------------------------------------

        protected override void InternalLoadInterstitialAd(AdPlacement _)
        {
            // Unity Ads handles loading automatically.
        }

        protected override bool InternalIsInterstitialAdReady(AdPlacement placement)
        {
            #if UNITY_ADS
            string placementId = placement == AdPlacement.Default ? 
                mAdSettings.DefaultInterstitialAdId.Id : FindIdForPlacement(mAdSettings.CustomInterstitialAdIds, placement);

            if (placementId == string.Empty)
                return false;

            return Advertisement.IsReady(placementId);
            #else
            return false;
            #endif
        }

        protected override void InternalShowInterstitialAd(AdPlacement placement)
        {
            #if UNITY_ADS
            string id = placement == AdPlacement.Default ? 
                mAdSettings.DefaultInterstitialAdId.Id : FindIdForPlacement(mAdSettings.CustomInterstitialAdIds, placement);

            if (string.IsNullOrEmpty(id))
            {
                Debug.LogFormat("Attempting to show {0} interstitial ad with an undefined ID at placement {1}",
                    Network.ToString(),
                    AdPlacement.GetPrintableName(placement));
                return;
            }

            var showOptions = new ShowOptions
            { 
                resultCallback = (result) =>
                {
                    InternalInterstitialAdCallback(result, placement);
                }
            };
            Advertisement.Show(id, showOptions);
            #endif
        }

        //------------------------------------------------------------
        // Rewarded Ads.
        //------------------------------------------------------------

        protected override void InternalLoadRewardedAd(AdPlacement _)
        {
            // Unity Ads handles loading automatically.
        }

        protected override bool InternalIsRewardedAdReady(AdPlacement placement)
        {
            #if UNITY_ADS
            string placementId = placement == AdPlacement.Default ? 
                mAdSettings.DefaultRewardedAdId.Id : FindIdForPlacement(mAdSettings.CustomRewardedAdIds, placement);

            if (placementId == string.Empty)
                return false;

            return Advertisement.IsReady(placementId);
            #else
            return false;
            #endif
        }

        protected override void InternalShowRewardedAd(AdPlacement placement)
        {
            #if UNITY_ADS
            string id = placement == AdPlacement.Default ? 
                mAdSettings.DefaultRewardedAdId.Id : FindIdForPlacement(mAdSettings.CustomRewardedAdIds, placement);

            if (string.IsNullOrEmpty(id))
            {
                Debug.LogFormat("Attempting to show {0} rewarded ad with an undefined ID at placement {1}",
                    Network.ToString(),
                    AdPlacement.GetPrintableName(placement));
                return;
            }

            var showOptions = new ShowOptions
            { 
                resultCallback = (result) =>
                {
                    InternalRewardedAdCallback(result, placement);
                }
            };
            Advertisement.Show(id, showOptions);
            #endif
        }

        #endregion  // AdClient Overrides

        #region IConsentRequirable Overrides

        private const string DATA_PRIVACY_CONSENT_KEY = "EM_Ads_UnityAds_DataPrivacyConsent";

        protected override string DataPrivacyConsentSaveKey { get { return DATA_PRIVACY_CONSENT_KEY; } }

        protected override void ApplyDataPrivacyConsent(ConsentStatus consent)
        {
            #if UNITY_ADS
            // https://unityads.unity3d.com/help/legal/gdpr
            switch (consent)
            {
                case ConsentStatus.Granted:
                    SetGdprMetadata(true);
                    break;
                case ConsentStatus.Revoked:
                    SetGdprMetadata(false);
                    break;
                case ConsentStatus.Unknown:
                default:
                    break;
            }
            #endif
        }

        #endregion

        #region Private Stuff

        #if UNITY_ADS

        /// <summary>
        /// Set appropriate GDPR metadata. This can be done either before or after initialization.
        /// https://unityads.unity3d.com/help/legal/gdpr
        /// </summary>
        /// <param name="hasConsent">If set to <c>true</c> has consent.</param>
        private void SetGdprMetadata(bool hasConsent)
        {
            MetaData metaData = new MetaData("gdpr");
            metaData.Set("consent", hasConsent ? "true" : "false");
            Advertisement.SetMetaData(metaData);
        }

        #endif

        #endregion

        #region Ad Event Handlers

        #if UNITY_ADS
        void InternalInterstitialAdCallback(ShowResult result, AdPlacement placement)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    OnInterstitialAdCompleted(placement);
                    break;
                case ShowResult.Skipped:
                    OnInterstitialAdCompleted(placement);
                    break;
                case ShowResult.Failed:
                    break;
            }

            if (InterstitialAdCallback != null)
                InterstitialAdCallback(result, placement);
        }

        void InternalRewardedAdCallback(ShowResult result, AdPlacement placement)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    OnRewardedAdCompleted(placement);
                    break;
                case ShowResult.Skipped:
                    OnRewardedAdSkipped(placement);
                    break;
                case ShowResult.Failed:
                    break;
            }

            if (RewardedAdCallback != null)
                RewardedAdCallback(result, placement);
        }
        #endif

        #endregion  // Ad Event Handlers
    }
}