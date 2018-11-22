#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using EasyMobile.Internal;

namespace EasyMobile.iOS.Telephony
{
    internal class CTCarrier
    {
        #region Native Methods

        [DllImport("__Internal")]
        private static extern bool CTCarrier_AllowsVOIP();

        [DllImport("__Internal")]
        private static extern int CTCarrier_CarrierName([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int CTCarrier_IsoCountryCode([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int CTCarrier_MobileCountryCode([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int CTCarrier_MobileNetworkCode([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        #endregion // Native Methods

        /// <summary>
        /// Indicates if the carrier allows VoIP calls to be made on its network.
        /// </summary>
        /// <returns><c>true</c>, if allows VoIP, <c>false</c> otherwise.</returns>
        public static bool GetAllowsVOIP()
        {
            return CTCarrier_AllowsVOIP();
        }

        /// <summary>
        /// The name of the user’s home cellular service provider.
        /// </summary>
        /// <returns>The carrier name.</returns>
        public static string GetCarrierName()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                CTCarrier_CarrierName(strBuffer, strLen));
        }

        /// <summary>
        /// The ISO country code for the user’s cellular service provider.
        /// </summary>
        /// <returns>The iso country code.</returns>
        public static string GetIsoCountryCode()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                CTCarrier_IsoCountryCode(strBuffer, strLen));
        }

        /// <summary>
        /// The mobile country code (MCC) for the user’s cellular service provider.
        /// </summary>
        /// <returns>The mobile country code.</returns>
        public static string GetMobileCountryCode()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                CTCarrier_MobileCountryCode(strBuffer, strLen));
        }

        /// <summary>
        /// The mobile network code (MNC) for the user’s cellular service provider.
        /// </summary>
        /// <returns>The mobile network code.</returns>
        public static string GetMobileNetworkCode()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                CTCarrier_MobileNetworkCode(strBuffer, strLen));
        }
    }
}
#endif