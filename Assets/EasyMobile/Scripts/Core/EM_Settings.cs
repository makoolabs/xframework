using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace EasyMobile
{
    public class EM_Settings : ScriptableObject
    {
        public static EM_Settings Instance
        {
            get
            {
                if (sInstance == null)
                {
                    sInstance = LoadSettingsAsset();

                    if (sInstance == null)
                    {
                        #if !UNITY_EDITOR
                        Debug.LogError("Easy Mobile settings not found! " +
                            "Please go to menu Windows > Easy Mobile > Settings to setup the plugin.");
                        #endif
                        sInstance = CreateInstance<EM_Settings>();   // Create a dummy scriptable object for temporary use.
                    }
                }

                return sInstance;
            }
        }

        public static EM_Settings LoadSettingsAsset()
        {
            return Resources.Load("EM_Settings") as EM_Settings;
        }

        #region Module Settings


        public static AdSettings Advertising { get { return Instance.mAdvertisingSettings; } }

        public static GameServicesSettings GameServices { get { return Instance.mGameServiceSettings; } }

        public static IAPSettings InAppPurchasing { get { return Instance.mInAppPurchaseSettings; } }

        public static PrivacySettings Privacy { get { return Instance.mPrivacySettings; } }

        public static NotificationsSettings Notifications { get { return Instance.mNotificationSettings; } }

        // Rating Request (Store Review) belongs to Utilities module.
        public static RatingRequestSettings RatingRequest { get { return Instance.mRatingRequestSettings; } }

        public static bool IsAdModuleEnable { get { return Instance.mIsAdModuleEnable; } }

        public static bool IsIAPModuleEnable{ get { return Instance.mIsIAPModuleEnable; } }

        public static bool IsGameServicesModuleEnable{ get { return Instance.mIsGameServiceModuleEnable; } }

        public static bool IsNotificationsModuleEnable { get { return Instance.mIsNotificationModuleEnable; } }

        #endregion

        #region Private members

        private static EM_Settings sInstance;

        [SerializeField]
        private AdSettings mAdvertisingSettings;
        [SerializeField]
        private GameServicesSettings mGameServiceSettings;
        [SerializeField]
        private IAPSettings mInAppPurchaseSettings;
        [SerializeField]
        private NotificationsSettings mNotificationSettings;
        [SerializeField]
        private PrivacySettings mPrivacySettings;
        [SerializeField]
        private RatingRequestSettings mRatingRequestSettings;

        [SerializeField]
        private bool mIsAdModuleEnable = false;
        [SerializeField]
        private bool mIsIAPModuleEnable = false;
        [SerializeField]
        private bool mIsGameServiceModuleEnable = false;
        [SerializeField]
        private bool mIsNotificationModuleEnable = false;

        #if UNITY_EDITOR
        // These fields are only used as a SerializedProperty in the editor scripts, hence the warning suppression.
        #pragma warning disable 0414
        [SerializeField]
        private int mActiveModuleIndex = 0; // Index of the active module on the toolbar.
        [SerializeField]
        private bool mIsSelectingModule = true;
        #pragma warning restore 0414

        #endif

        #endregion
    }
}

