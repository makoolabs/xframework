#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using EasyMobile.Internal;

namespace EasyMobile.iOS.Foundation
{
    internal class NSTimeZone
    {
        #region Native Methods

        [DllImport("__Internal")]
        private static extern int NSTimeZone_LocalTimeZone_Name([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int NSTimeZone_LocalTimeZone_Abbreviation([In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int NSTimeZone_LocalTimeZone_SecondsFromGMT();

        #endregion // Native Methods

        /// <summary>
        /// The geopolitical region ID that identifies the local timezone.
        /// </summary>
        /// <returns>The local time zone name.</returns>
        public static string GetLocalTimeZoneName()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                NSTimeZone_LocalTimeZone_Name(strBuffer, strLen));
        }

        /// <summary>
        /// The abbreviation for the local timezone, such as “EDT” (Eastern Daylight Time).
        /// </summary>
        /// <returns>The local time zone abbreviation.</returns>
        public static string GetLocalTimeZoneAbbreviation()
        {
            return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                NSTimeZone_LocalTimeZone_Abbreviation(strBuffer, strLen));
        }

        /// <summary>
        /// The current difference in seconds between the local timezone and Greenwich Mean Time.
        /// </summary>
        /// <returns>The local time zone seconds from GM.</returns>
        public static int GetLocalTimeZoneSecondsFromGMT()
        {
            return NSTimeZone_LocalTimeZone_SecondsFromGMT();
        }
    }
}
#endif