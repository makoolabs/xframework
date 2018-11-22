#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using EasyMobile.Internal;

namespace EasyMobile.iOS.Foundation
{
    internal class NSLocale
    {
        #region Native Methods

        [DllImport("__Internal")]
        private static extern int NSLocale_CurrentLocaleIdentifier([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int NSLocale_CurrentCountryCode([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int NSLocale_CurrentLanguageCode([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int NSLocale_CurrentScriptCode([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int NSLocale_CurrentVariantCode([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int NSLocale_CurrentCurrencyCode([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int NSLocale_CurrentCurrencySymbol([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        #endregion // Native Methods

        /// <summary>
        /// The identifier for the current locale.
        /// </summary>
        /// <returns>The current locale identifier.</returns>
        public static string GetCurrentLocaleIdentifier()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                NSLocale_CurrentLocaleIdentifier(strBuffer, strLen));
        }

        /// <summary>
        /// The country code for the current locale.
        /// </summary>
        /// <returns>The current country code.</returns>
        public static string GetCurrentCountryCode()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                NSLocale_CurrentCountryCode(strBuffer, strLen));
        }

        /// <summary>
        /// The language code for the current locale.
        /// </summary>
        /// <returns>The current language code.</returns>
        public static string GetCurrentLanguageCode()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                NSLocale_CurrentLanguageCode(strBuffer, strLen));
        }

        /// <summary>
        /// The script code for the current locale.
        /// </summary>
        /// <returns>The current script code.</returns>
        public static string GetCurrentScriptCode()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                NSLocale_CurrentScriptCode(strBuffer, strLen));
        }

        /// <summary>
        /// The variant code for the current locale.
        /// </summary>
        /// <returns>The current variant code.</returns>
        public static string GetCurrentVariantCode()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                NSLocale_CurrentVariantCode(strBuffer, strLen));
        }

        /// <summary>
        /// The currency code for the current locale.
        /// </summary>
        /// <returns>The current currency code.</returns>
        public static string GetCurrentCurrencyCode()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                NSLocale_CurrentCurrencyCode(strBuffer, strLen));
        }

        /// <summary>
        /// The currency symbol for the current locale.
        /// </summary>
        /// <returns>The current currency symbol.</returns>
        public static string GetCurrentCurrencySymbol()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                NSLocale_CurrentCurrencySymbol(strBuffer, strLen));
        }
    }
}
#endif