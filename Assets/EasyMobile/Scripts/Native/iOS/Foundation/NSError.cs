#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using EasyMobile.Internal;

namespace EasyMobile.iOS.Foundation
{
    internal class NSError : iOSInteropObject
    {
        [DllImport("__Internal")]
        private static extern int _InteropNSError_Code(HandleRef self);

        [DllImport("__Internal")]
        private static extern int _InteropNSError_Domain(
            HandleRef self, [In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int _InteropNSError_LocalizedDescription(
            HandleRef self, [In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int _InteropNSError_LocalizedRecoverySuggestion(
            HandleRef self, [In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        [DllImport("__Internal")]
        private static extern int _InteropNSError_LocalizedFailureReason(
            HandleRef self, [In,Out] /* from(char *) */ byte[] strBuffer, int strLen);

        public NSError(IntPtr selfPointer)
            : base(selfPointer)
        {
        }

        public static NSError FromPointer(IntPtr pointer)
        {
            if (pointer.Equals(IntPtr.Zero))
            {
                return null;
            }
            return new NSError(pointer);
        }

        /// <summary>
        /// The error code.
        /// </summary>
        /// <value>The code.</value>
        public int Code
        {
            get { return _InteropNSError_Code(SelfPtr()); }
        }

        /// <summary>
        /// A string containing the error domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain
        {
            get
            {
                return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                    _InteropNSError_Domain(SelfPtr(), strBuffer, strLen));
            }
        }

        /// <summary>
        /// A string containing the localized description of the error.
        /// </summary>
        /// <value>The localized description.</value>
        public string LocalizedDescription
        {
            get
            {
                return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                    _InteropNSError_LocalizedDescription(SelfPtr(), strBuffer, strLen));
            }
        }

        /// <summary>
        /// A string containing the localized recovery suggestion for the error.
        /// </summary>
        /// <value>The localized recovery suggestion.</value>
        public string LocalizedRecoverySuggestion
        {
            get
            {
                return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                    _InteropNSError_LocalizedRecoverySuggestion(SelfPtr(), strBuffer, strLen));
            }
        }

        /// <summary>
        /// A string containing the localized explanation of the reason for the error.
        /// </summary>
        /// <value>The localized failure reason.</value>
        public string LocalizedFailureReason
        {
            get
            {
                return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                    _InteropNSError_LocalizedFailureReason(SelfPtr(), strBuffer, strLen));
            }
        }
    }
}
#endif