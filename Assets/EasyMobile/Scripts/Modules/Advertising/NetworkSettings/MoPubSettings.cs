using System;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile.Internal;

namespace EasyMobile
{
    [Serializable]
    public class MoPubSettings
    {
        /// <summary>
        /// Gets or sets the default banner identifier.
        /// </summary>
        public AdId DefaultBannerId
        {
            get { return mDefaultBannerId; }
            set { mDefaultBannerId = value; }
        }

        /// <summary>
        /// Gets or sets the default interstitial ad identifier.
        /// </summary>
        public AdId DefaultInterstitialAdId
        {
            get { return mDefaultInterstitialAdId; }
            set { mDefaultInterstitialAdId = value; }
        }

        /// <summary>
        /// Gets or sets the default rewarded ad identifier.
        /// </summary>
        public AdId DefaultRewardedAdId
        {
            get { return mDefaultRewardedAdId; }
            set { mDefaultRewardedAdId = value; }
        }

        /// <summary>
        /// Gets or sets the list of custom banner identifiers.
        /// Each identifier is associated with an ad placement.
        /// </summary>
        public Dictionary<AdPlacement, AdId> CustomBannerIds
        {
            get { return mCustomBannerIds; }
            set { mCustomBannerIds = value as Dictionary_AdPlacement_AdId; }
        }

        /// <summary>
        /// Gets or sets the list of custom interstitial ad identifiers.
        /// Each identifier is associated with an ad placement.
        /// </summary>
        public Dictionary<AdPlacement, AdId> CustomInterstitialAdIds
        {
            get { return mCustomInterstitialAdIds; }
            set { mCustomInterstitialAdIds = value as Dictionary_AdPlacement_AdId; }
        }

        /// <summary>
        /// Gets or sets the list of custom rewarded ad identifiers.
        /// Each identifier is associated with an ad placement.
        /// </summary>
        public Dictionary<AdPlacement, AdId> CustomRewardedAdIds
        {
            get { return mCustomRewardedAdIds; }
            set { mCustomRewardedAdIds = value as Dictionary_AdPlacement_AdId; }
        }

        /// <summary>
        /// Enables or disables application open report.
        /// </summary>
        public bool ReportAppOpen
        {
            get { return mReportAppOpen; }
            set { mReportAppOpen = value; }
        }

        /// <summary>
        /// Gets or sets ITune App Id.
        /// </summary>
        public string ITuneAppID
        {
            get { return mITuneAppID; }
            set { mITuneAppID = value; }
        }

        /// <summary>
        /// Enables or disables location passing.
        /// </summary>
        public bool EnableLocationPassing
        {
            get { return mEnableLocationPassing; }
            set { mEnableLocationPassing = value; }
        }

        /// <summary>
        /// Enables or disables advanced settings.
        /// </summary>
        public bool UseAdvancedSetting
        {
            get { return mUseAdvancedSetting; }
            set { mUseAdvancedSetting = value; }
        }

        /// <summary>
        /// Gets or sets the list of mediation settings.
        /// </summary>
        public MediationSetting[] MediationSettings
        {
            get { return mMediationSettings; }
            set { mMediationSettings = value; }
        }

        /// <summary>
        /// Gets or sets the list of initialize networks.
        /// </summary>
        public MoPubInitNetwork[] InitializeNetworks
        {
            get { return mInitNetworks; }
            set { mInitNetworks = value; }
        }

        /// <summary>
        /// Gets or sets the list of advanced bidders.
        /// </summary>
        public MoPubAdvancedBidder[] AdvancedBidders
        {
            get { return mAdvancedBidders; }
            set { mAdvancedBidders = value; }
        }

        /// <summary>
        /// Enables or disables auto request consent.
        /// </summary>
        public bool AutoRequestConsent
        {
            get { return mAutoRequestConsent; }
            set { mAutoRequestConsent = value; }
        }

        /// <summary>
        /// Force GDPR applicable or not ?
        /// </summary>
        public bool ForceGdprApplicable
        {
            get { return mForceGdprApplicable; }
            set { mForceGdprApplicable = value; }
        }

        [SerializeField]
        private bool mReportAppOpen;
        [SerializeField]
        private string mITuneAppID;
        [SerializeField]
        private bool mEnableLocationPassing;
        [SerializeField]
        private bool mUseAdvancedSetting;
        [SerializeField]
        private MediationSetting[] mMediationSettings;
        [SerializeField]
        private MoPubInitNetwork[] mInitNetworks;
        [SerializeField]
        private MoPubAdvancedBidder[] mAdvancedBidders;
        [SerializeField]
        private bool mAutoRequestConsent;
        [SerializeField]
        private bool mForceGdprApplicable;

        [SerializeField]
        private AdId mDefaultBannerId;
        [SerializeField]
        private AdId mDefaultInterstitialAdId;
        [SerializeField]
        private AdId mDefaultRewardedAdId;
        [SerializeField]
        private Dictionary_AdPlacement_AdId mCustomBannerIds;
        [SerializeField]
        private Dictionary_AdPlacement_AdId mCustomInterstitialAdIds;
        [SerializeField]
        private Dictionary_AdPlacement_AdId mCustomRewardedAdIds;

        public enum MoPubInitNetwork
        {
            AdColony,
            AdMob,
            Chartboost,
            Facebook,
            TapJoy,
            UnityAds,
            Vungle,
        }

        public enum MoPubAdvancedBidder
        {
            AdColony,
            Facebook,
        }

        [Serializable]
        public class MediationSetting
        {
            [SerializeField]
            private string mAdVendor = null;

            [SerializeField]
            private StringStringSerializableDictionary mExtraOptions = null;

            public string AdVendor { get { return mAdVendor; } }

            public Dictionary<string, string> ExtraOptions { get { return mExtraOptions; } }
        }
    }
}
