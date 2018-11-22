using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using EM_GSM = EasyMobile.Editor.EM_GUIStyleManager;

namespace EasyMobile.Editor
{
    /// <summary>
    /// Partial class for module selection tab.
    /// </summary>
    internal partial class EM_SettingsEditor
    {
        private const int MinModuleBarHeight = 90;

        private GUIStyle ModuleTitleStyle { get { return EM_GSM.GetCustomStyle("Module Selection Title"); } }

        private GUIStyle ModuleDescriptionStyle { get { return EM_GSM.GetCustomStyle("Module Selection Description"); } }

        private GUIStyle ModuleIconStyle { get { return EM_GSM.GetCustomStyle("Module Selection Icon"); } }

        private GUIStyle ModuleHeaderIconStyle { get { return EM_GSM.GetCustomStyle("Module Header Icon"); } }

        private GUIStyle BackButtonStyle { get { return EM_GSM.GetCustomStyle("Module Back Button"); } }

        private GUIStyle ModulePanelStyle{ get { return EM_GSM.GetCustomStyle("Module Box"); } }

        public void DrawSelectModulePage()
        {
            DrawModuleSelectBar(Module.Advertising);
            DrawModuleSelectBar(Module.GameService);
            DrawModuleSelectBar(Module.InAppPurchase);
            DrawModuleSelectBar(Module.Notification);
            DrawModuleSelectBar(Module.Privacy);
            DrawModuleSelectBar(Module.Utility);
        }

        public void DrawModuleHeader()
        {
            DrawModuleSelectBar(activeModule, false, false);
        }

        /// <summary>
        /// Draw the top header of the active module when we open it.
        /// </summary>
        public void DrawModuleSelectBar(Module module, bool clickableBar = true, bool drawBackButton = false)
        {
            /// Get infos of the active module.
            string name = GetModuleName(module);
            string description = GetModuleDescription(module);
            Texture2D icon = GetModuleIcon(module);
            var triggerProperty = GetTriggerProperty(module);
            Action disableModule = GetDisableModuleAction(module);
            Action enableModule = GetEnableModuleAction(module);

            EditorGUILayout.BeginVertical(ModulePanelStyle, GUILayout.MinHeight(MinModuleBarHeight));
            EditorGUILayout.BeginHorizontal();

            /// Draw back button.
            if (drawBackButton)
            {
                if (GUILayout.Button("", BackButtonStyle))
                {
                    isSelectingModule.boolValue = true;
                }
            }     
            
            /// Draw icon.
            GUILayout.Label(icon, ModuleHeaderIconStyle);

            /// Draw name.
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(name, ModuleTitleStyle);
            EditorGUILayout.EndVertical();

            /// Draw toggle.
            Rect toggleRect = Rect.zero;
            if (triggerProperty != null)
            {
                EditorGUI.BeginChangeCheck();
                triggerProperty.boolValue = EM_EditorGUI.ModuleToggle(triggerProperty.boolValue, name);
                if (EditorGUI.EndChangeCheck())
                {
                    if (triggerProperty.boolValue)
                    {
                        // Enable the module and also select it.
                        // IMPORTANT: SelectModule() must be done first for the transition to be correct.
                        SelectModule(module);
                        enableModule();
                    }
                    else
                    {
                        // Disable the module and go back to home if is in module settings page.
                        disableModule();
                        isSelectingModule.boolValue = true;
                    }
                }

                toggleRect = GUILayoutUtility.GetLastRect();
            }

            EditorGUILayout.EndHorizontal();

            /// Draw description.
            EditorGUILayout.LabelField(description, ModuleDescriptionStyle);

            EditorGUILayout.EndVertical();

            if (clickableBar)
            {
                /// Draw an invisible button on top of all the elements above
                /// so we can click on it and open the module settings tab.
                /// If there's a toggle the button only extends to its left edge.
                var btnRect = GUILayoutUtility.GetLastRect();
                if (toggleRect != Rect.zero)
                    btnRect.width = toggleRect.x - btnRect.x;

                if (GUI.Button(btnRect, "", GUIStyle.none))
                {
                    if (IsModuleEnable(module))
                        SelectModule(module);
                }
            }
        }

        /// <summary>
        /// Check if a module is active or not.
        /// Return null if the module is not activable (<see cref="Module.Privacy"/>, <see cref="Module.Utility"/>).
        /// </summary>
        public bool? GetModuleActiveState(Module module)
        {
            switch (module)
            {
                case Module.Advertising:
                    return EM_Settings.IsAdModuleEnable;
                case Module.InAppPurchase:
                    return EM_Settings.IsIAPModuleEnable;
                case Module.GameService:
                    return EM_Settings.IsGameServicesModuleEnable;
                case Module.Notification:
                    return EM_Settings.IsNotificationsModuleEnable;
                case Module.Privacy:
                    return null;
                case Module.Utility:
                    return null;
                default:
                    return null;
            }
        }

        public string GetModuleName(Module module)
        {
            switch (module)
            {
                case Module.Advertising:
                    return AdModuleLabel;
                case Module.InAppPurchase:
                    return IAPModuleLabel;
                case Module.GameService:
                    return GameServiceModuleLabel;
                case Module.Notification:
                    return NotificationModuleLabel;
                case Module.Privacy:
                    return PrivacyModuleLabel;
                case Module.Utility:
                    return UtilityModuleLabel;
                default:
                    return null;
            }
        }

        public string GetModuleDescription(Module module)
        {
            switch (module)
            {
                case Module.Advertising:
                    return AdModuleIntro;
                case Module.InAppPurchase:
                    return IAPModuleIntro;
                case Module.GameService:
                    return GameServiceModuleIntro;
                case Module.Notification:
                    return NotificationModuleIntro;
                case Module.Privacy:
                    return PrivacyModuleIntro;
                case Module.Utility:
                    return UtilityModuleIntro;
                default:
                    return "";
            }
        }

        public Texture2D GetModuleIcon(Module module)
        {
            switch (module)
            {
                case Module.Advertising:
                    return EM_GSM.AdIcon;
                case Module.InAppPurchase:
                    return EM_GSM.IAPIcon;
                case Module.GameService:
                    return EM_GSM.GameServiceIcon;
                case Module.Notification:
                    return EM_GSM.NotificationIcon;
                case Module.Privacy:
                    return EM_GSM.PrivacyIcon;
                case Module.Utility:
                    return EM_GSM.UtilityIcon;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Return the method used to draw all the GUI of a module.
        /// </summary>
        public Action GetModuleDrawAction(Module module)
        {
            switch (module)
            {
                case Module.Advertising:
                    return AdModuleGUI;
                case Module.InAppPurchase:
                    return IAPModuleGUI;
                case Module.GameService:
                    return GameServiceModuleGUI;
                case Module.Notification:
                    return NotificationModuleGUI;
                case Module.Privacy:
                    return PrivacyModuleGUI;
                case Module.Utility:
                    return UtilityModuleGUI;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Return the method to enable a module in <see cref="EM_PluginManager"/>.
        /// Return an empty method if the target module doesn't need to be enabled (<see cref="Module.Privacy"/>, <see cref="Module.Utility"/>).
        /// </summary>
        public Action GetEnableModuleAction(Module module)
        {
            switch (module)
            {
                case Module.Advertising:
                    return EM_PluginManager.EnableAdModule;
                case Module.InAppPurchase:
                    return EM_PluginManager.EnableIAPModule;
                case Module.GameService:
                    return EM_PluginManager.EnableGameServiceModule;
                case Module.Notification:
                    return EM_PluginManager.EnableNotificationModule;
                case Module.Privacy:
                    return () =>
                    {
                    };
                case Module.Utility:
                    return () =>
                    {
                    };
                default:
                    return () =>
                    {
                    };
            }
        }

        /// <summary>
        /// Return the method to disable a module in <see cref="EM_PluginManager"/>.
        /// Return an empty method if the target module can't be disabled (<see cref="Module.Privacy"/>, <see cref="Module.Utility"/>).
        /// </summary>
        public Action GetDisableModuleAction(Module module)
        {
            switch (module)
            {
                case Module.Advertising:
                    return EM_PluginManager.DisableAdModule;
                case Module.InAppPurchase:
                    return EM_PluginManager.DisableIAPModule;
                case Module.GameService:
                    return EM_PluginManager.DisableGameServiceModule;
                case Module.Notification:
                    return EM_PluginManager.DisableNotificationModule;
                case Module.Privacy:
                    return () =>
                    {
                    };
                case Module.Utility:
                    return () =>
                    {
                    };
                default:
                    return () =>
                    {
                    };
            }
        }

        /// <summary>
        /// Return the serliazed property used to check if a module is active or not.
        /// Reutrn null if the target module is not activable (<see cref="Module.Privacy"/>, <see cref="Module.Utility"/>).
        /// </summary>
        public SerializedProperty GetTriggerProperty(Module module)
        {
            switch (module)
            {
                case Module.Advertising:
                    return isAdModuleEnable;
                case Module.InAppPurchase:
                    return isIAPModuleEnable;
                case Module.GameService:
                    return isGameServiceModuleEnable;
                case Module.Notification:
                    return isNotificationModuleEnable;
                case Module.Privacy:
                    return null;
                case Module.Utility:
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Determines whether the module is enable. Modules that don't have
        /// a toggle are enabled by default.
        /// </summary>
        /// <returns><c>true</c> if this instance is module enable the specified module; otherwise, <c>false</c>.</returns>
        /// <param name="module">Module.</param>
        public bool IsModuleEnable(Module module)
        {
            var trigger = GetTriggerProperty(module);
            return trigger == null || trigger.boolValue;
        }

        /// <summary>
        /// Makes a module active so its settings UI is opened.
        /// </summary>
        /// <param name="module">Module.</param>
        private void SelectModule(Module module)
        {
            isSelectingModule.boolValue = false;
            activeModule = module;
            // Store the toolbar index value to the serialized settings file.
            activeModuleIndex.intValue = (int)activeModule;
        }
    }
}
