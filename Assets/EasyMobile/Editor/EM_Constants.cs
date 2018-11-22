using UnityEngine;
using System.Collections;

namespace EasyMobile.Editor
{
    public static class EM_Constants
    {
        // Product name
        public const string ProductName = "Easy Mobile";
        public const string Copyright = "© 2017-2018 SgLib Games LLC. All Rights Reserved.";

        // Current version
        #if !EASY_MOBILE_PRO
        // EM Basic.
        public const string versionString = "2.2.0";
        public const int versionInt = 0x020200;
        
#else
        // EM Pro.
        public const string versionString = "2.1.1";
        public const int versionInt = 0x020101;
        #endif

        // Folder
        public const string RootPath = "Assets/EasyMobile";
        public const string EditorFolder = RootPath + "/Editor";
        public const string TemplateFolder = EditorFolder + "/Templates";
        public const string GeneratedFolder = RootPath + "/Generated";
        public const string MainPrefabFolder = RootPath;
        public const string MaterialsFolder = RootPath + "/Materials";
        public const string PackagesFolder = RootPath + "/Packages";
        public const string SkinFolder = RootPath + "/GUISkins";
        public const string SkinTextureFolder = SkinFolder + "/Textures";
        public const string ResourcesFolder = RootPath + "/Resources";
        public const string ScriptsFolder = RootPath + "/Scripts";
        public const string ReceiptValidationFolder = "Assets/Plugins/UnityPurchasing/generated";
        public const string AssetsPluginsAndroidFolder = "Assets/Plugins/Android";
        public const string AssetsPluginsIOSFolder = "Assets/Plugins/iOS";


        // Assets and stuff
        public const string SettingsAssetName = "EM_Settings";
        public const string SettingsAssetExtension = ".asset";
        public const string SettingsAssetPath = ResourcesFolder + "/EM_Settings.asset";
        public const string MainPrefabName = "EasyMobile";
        public const string PrefabExtension = ".prefab";
        public const string MainPrefabPath = MainPrefabFolder + "/EasyMobile.prefab";
        public const string PluginSettingsFilePath = EditorFolder + "/EasyMobileSettings.txt";
        public const string ClipPlayerMaterialPath = MaterialsFolder + "/ClipPlayerMat.mat";

        // Demo app.
        public const string DemoFolder = RootPath + "/Demo";
        public const string DemoScenesFolder = DemoFolder + "/Scenes";
        public const string DemoHomeScenePath = DemoScenesFolder + "/DemoHome.unity";
        public const string DemoAdvertisingScenePath = DemoScenesFolder + "/Modules/AvertisingDemo.unity";
        public const string DemoGameServicesScenePath = DemoScenesFolder + "/Modules/GameServicesDemo.unity";
        public const string DemoInAppPurchasingScenePath = DemoScenesFolder + "/Modules/InAppPurchasingDemo.unity";
        public const string DemoNativeUIScenePath = DemoScenesFolder + "/Modules/NativeUIDemo.unity";
        public const string DemoNotificationsScenePath = DemoScenesFolder + "/Modules/NotificationsDemo.unity";
        public const string DemoPrivacyScenePath = DemoScenesFolder + "/Modules/PrivacyDemo.unity";
        public const string DemoSharingScenePath = DemoScenesFolder + "/Modules/SharingDemo.unity";
        public const string DemoUtilitiesScenePath = DemoScenesFolder + "/Modules/UtilitiesDemo.unity";

        // UnityPackages
        public const string PlayServicersResolverPackagePath = PackagesFolder + "/play-services-resolver.unitypackage";
        public const string PlayMakerActionsPackagePath = PackagesFolder + "/PlayMakerActions.unitypackage";

        // Android native package names.
        public const string AndroidNativePackageName = "com.sglib.easymobile.androidnative";
        public const string AndroidNativeNotificationPackageName = "com.sglib.easymobile.androidnative.notification";

        // Generated class names
        public const string RootNameSpace = "EasyMobile";
        public const string AndroidGPGSConstantClassName = "EM_GPGSIds";
        public const string GameServicesConstantsClassName = "EM_GameServicesConstants";
        public const string IAPConstantsClassName = "EM_IAPConstants";
        public const string NotificationsConstantsClassName = "EM_NotificationsConstants";
        public const string NotificationAndroidResFolderName = "EMNotificationResources";
        public const string AdvertisingConstantsClassName = "EM_AdvertisingConstants";

        // URLs
        public const string EasyMobileWebsiteURL = "https://easymobile.sglibgames.com/";
        #if !EASY_MOBILE_PRO
        public const string DocumentationURL = EasyMobileWebsiteURL + "/docs/basic";
        public const string ScriptingRefURL = EasyMobileWebsiteURL + "/api/basic";
        public const string AssetStoreURL = "http://u3d.as/18pa";
        

#else
        public const string DocumentationURL = EasyMobileWebsiteURL + "/docs/pro";
        public const string ScriptingRefURL = EasyMobileWebsiteURL + "/api/pro";
        public const string AssetStoreURL = "http://u3d.as/Dd2";
        #endif

        public const string SupportEmail = "support@sglibgames.com";
        public const string SupportEmailSubject = "[EM Basic][YOUR_INVOICE_NUMBER]";

        // Common symbols
        public const string NoneSymbol = "[None]";
        public const string DeleteSymbol = "-";
        public const string UpSymbol = "↑";
        public const string DownSymbol = "↓";

        // ProjectSettings keys
        public const string PSK_EMVersionString = "VERSION";
        public const string PSK_EMVersionInt = "VERSION_INT";
        public const string PSK_ImportedPlayServicesResolver = "IMPORTED_PLAY_SERVICES_RESOLVER";
    }
}

