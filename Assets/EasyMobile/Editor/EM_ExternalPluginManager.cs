using UnityEngine;
using UnityEditor;
using System.Collections;

namespace EasyMobile.Editor
{
    public static class EM_ExternalPluginManager
    {
        // AdColony
        public const string AdColonyNameSpace = "AdColony";

        // AdMob
        public const string GoogleMobileAdsNameSpace = "GoogleMobileAds";

        // Chartboost
        public const string ChartboostNameSpace = "ChartboostSDK";
        public const string ChartboostClassName = "Chartboost";

        // Facebook Audience Network.
        public const string FBAudienceNameSpace = "AudienceNetwork";

        // Heyzap
        public const string HeyzapNameSpace = "Heyzap";

        // IronSource
        public const string IronSourceClassname = "IronSource";

        // MoPub
        public const string MoPubClassName = "MoPub";

        // TapJoy
        public const string TapJoyNameSpace = "TapjoyUnity";

        // UnityIAP
        public const string UnityPurchasingAssemblyName = "UnityEngine.Purchasing";
        public const string UnityPurchasingNameSpace = "UnityEngine.Purchasing";
        public const string UnityPurchasingSecurityNameSpace = "UnityEngine.Purchasing.Security";
        public const string UnityPurchasingClassName = "UnityPurchasing";

        // Google Play Games
        public const string GPGSNameSpace = "GooglePlayGames";
        public const string GPGSClassName = "PlayGamesPlatform";

        // OneSignal
        public const string OneSignalClassName = "OneSignal";

        // Firebase Messaging
        public const string FirebaseMessagingNameSpace = "Firebase.Messaging";
        public const string FirebaseMessagingClassName = "FirebaseMessaging";

        // PlayMaker Unity UI add-on
        public const string PlayMakerUguiAddOnClass = "PlayMakerUGuiSceneProxy";

        // Advertising 3rd party plugins URLs
        public const string AdColonyDownloadURL = "https://github.com/AdColony/AdColony-Unity-SDK-3";
        public const string ChartboostDownloadURL = "https://answers.chartboost.com/en-us/articles/download";
        public const string FBAudienceDownloadURL = "https://developers.facebook.com/docs/audience-network/download#unity";
        public const string GoogleMobileAdsDownloadURL = "https://github.com/googleads/googleads-mobile-unity/releases";
        public const string HeyzapDownloadURL = "https://developers.heyzap.com/docs/unity_sdk_setup_and_requirements";
        public const string IronSourceDownloadURL = "https://developers.ironsrc.com/ironsource-mobile/unity/unity-plugin/#step-1";
        public const string MoPubDownloadURL = "https://github.com/mopub/mopub-unity-sdk/";
        public const string TapJoyDownloadURL = "https://ltv.tapjoy.com/d/sdks";

        // Game Services 3rd party plugins URLs
        public const string GooglePlayGamesDownloadURL = "https://github.com/playgameservices/play-games-plugin-for-unity";

        // Notifications 3rd party plugins URLs
        public const string OneSignalDownloadURL = "https://github.com/OneSignal/OneSignal-Unity-SDK";
        public const string FirebaseDownloadURL = "https://firebase.google.com/docs/unity/setup";

        /// <summary>
        /// Determines if AdColony plugin is available.
        /// </summary>
        /// <returns><c>true</c> if AdColony plugin available; otherwise, <c>false</c>.</returns>
        public static bool IsAdColonyAvail()
        {
            return EM_EditorUtil.NamespaceExists(AdColonyNameSpace);
        }

        /// <summary>
        /// Determines if AdMob plugin is available.
        /// </summary>
        /// <returns><c>true</c> if AdMob plugin available; otherwise, <c>false</c>.</returns>
        public static bool IsAdMobAvail()
        {
            return EM_EditorUtil.NamespaceExists(GoogleMobileAdsNameSpace);
        }

        /// <summary>
        /// Determines if Chartboost plugin is available.
        /// </summary>
        /// <returns><c>true</c> if Chartboost plugin available; otherwise, <c>false</c>.</returns>
        public static bool IsChartboostAvail()
        {
            System.Type chartboost = EM_EditorUtil.FindClass(ChartboostClassName, ChartboostNameSpace);
            return chartboost != null;
        }

        /// <summary>
        /// Determines if Facebook Audience Network plugin is available.
        /// </summary>
        /// <returns></returns>
        public static bool IsFBAudienceAvail()
        {
            return EM_EditorUtil.NamespaceExists(FBAudienceNameSpace);
        }

        /// <summary>
        /// Determines if Heyzap plugin is available.
        /// </summary>
        /// <returns><c>true</c> if Heyzap plugin available; otherwise, <c>false</c>.</returns>
        public static bool IsHeyzapAvail()
        {
            return EM_EditorUtil.NamespaceExists(HeyzapNameSpace);
        }

        /// <summary>
        /// Determines if MoPub plugin is available.
        /// </summary>
        /// <returns><c>true</c> if MoPub plugin is available; otherwise, <c>false</c>.</returns>
        public static bool IsMoPubAvail()
        {
            return EM_EditorUtil.FindClass(MoPubClassName) != null;
        }

        /// Determines if IronSource plugin is available.
        /// </summary>
        public static bool IsIronSourceAvail()
        {
            return EM_EditorUtil.FindClass(IronSourceClassname) != null;
        }

        /// Determindes if TapJoy plugin is available.
        /// </summary>
        /// <returns><c>true</c> if TapJoy plugin is available, otherwise <c>false</c>.</returns>
        public static bool IsTapJoyAvail()
        {
            return EM_EditorUtil.NamespaceExists(TapJoyNameSpace);
        }

        /// <summary>
        /// Determines if UnityIAP is enabled.
        /// </summary>
        /// <returns><c>true</c> if enabled; otherwise, <c>false</c>.</returns>
        public static bool IsUnityIAPAvail()
        {
            // Here we check for the existence of the Security namespace instead of UnityPurchasing class in order to
            // make sure that the plugin is actually imported (rather than the service just being enabled).
            return EM_EditorUtil.NamespaceExists(UnityPurchasingSecurityNameSpace);
        }

        /// <summary>
        /// Determines if GooglePlayGames plugin is available.
        /// </summary>
        /// <returns><c>true</c> if is GPGS avail; otherwise, <c>false</c>.</returns>
        public static bool IsGPGSAvail()
        {
            System.Type gpgs = EM_EditorUtil.FindClass(GPGSClassName, GPGSNameSpace);
            return gpgs != null;
        }

        /// <summary>
        /// Determines if OneSignal plugin is available.
        /// </summary>
        /// <returns><c>true</c> if is one signal avail; otherwise, <c>false</c>.</returns>
        public static bool IsOneSignalAvail()
        {
            System.Type oneSignal = EM_EditorUtil.FindClass(OneSignalClassName);
            return oneSignal != null;
        }

        /// <summary>
        /// Determines if FirebaseMessaging plugin is available.
        /// </summary>
        public static bool IsFirebaseMessagingAvail()
        {
            System.Type firMsg = EM_EditorUtil.FindClass(FirebaseMessagingClassName, FirebaseMessagingNameSpace);
            return firMsg != null;
        }

        /// <summary>
        /// Determines if PlayMaker is installed.
        /// </summary>
        /// <returns><c>true</c> if is play maker avail; otherwise, <c>false</c>.</returns>
        public static bool IsPlayMakerAvail()
        {
            #if PLAYMAKER
            return true;
            #else
            return false;
            #endif
        }

        public static bool IsPlayMakerUguiAddOnAvail()
        {
            System.Type uGui = EM_EditorUtil.FindClass(PlayMakerUguiAddOnClass);

            return uGui != null;
        }

        public static void DownloadGoogleMobileAdsPlugin()
        {
            Application.OpenURL(GoogleMobileAdsDownloadURL);
        }

        public static void DownloadGooglePlayGamesPlugin()
        {
            Application.OpenURL(GooglePlayGamesDownloadURL);
        }

        public static void DownloadOneSignalPlugin()
        {
            Application.OpenURL(OneSignalDownloadURL);
        }

        public static void DownloadChartboostPlugin()
        {
            Application.OpenURL(ChartboostDownloadURL);
        }

        public static void DownloadHeyzapPlugin()
        {
            Application.OpenURL(HeyzapDownloadURL);
        }

        public static void DownloadAdColonyPlugin()
        {
            Application.OpenURL(AdColonyDownloadURL);
        }

        public static void DownloadFirebasePlugin()
        {
            Application.OpenURL(FirebaseDownloadURL);
        }

        public static void DownloadFacebookAudiencePlugin()
        {
            Application.OpenURL(FBAudienceDownloadURL);
        }

        public static void DownloadMoPubPlugin()
        {
            Application.OpenURL(MoPubDownloadURL);
        }

        public static void DownloadIronSourcePlugin()
        {
            Application.OpenURL(IronSourceDownloadURL);
        }

        public static void DownloadTapJoyPlugin()
        {
            Application.OpenURL(TapJoyDownloadURL);
        }
    }
}

