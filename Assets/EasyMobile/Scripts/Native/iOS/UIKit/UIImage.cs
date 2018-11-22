#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using EasyMobile.Internal;

namespace EasyMobile.iOS.UIKit
{
    internal class UIImage : iOSInteropObject
    {
        [DllImport("__Internal")]
        private static extern float _InteropUIImage_Scale(HandleRef self);

        [DllImport("__Internal")]
        private static extern float _InteropUIImage_Width(HandleRef self);

        [DllImport("__Internal")]
        private static extern float _InteropUIImage_Height(HandleRef self);

        [DllImport("__Internal")]
        private static extern int _InteropUIImage_GetPNGData(
            HandleRef self, [In, Out] /* from(unsigned char *) */ byte[] buffer, int byteCount);

        [DllImport("__Internal")]
        private static extern int _InteropUIImage_GetJPEGData(
            HandleRef self, float compressionQuality, [In, Out] /* from(unsigned char *) */ byte[] buffer, int byteCount);

        private float? mScale = null;
        private float? mWidth = null;
        private float? mHeight = null;

        public UIImage(IntPtr selfPointer)
            : base(selfPointer)
        {
        }

        public static UIImage FromPointer(IntPtr pointer)
        {
            if (pointer.Equals(IntPtr.Zero))
            {
                return null;
            }
            return new UIImage(pointer);
        }

        public float Scale
        {
            get
            {
                if (mScale == null)
                    mScale = _InteropUIImage_Scale(SelfPtr());
                return mScale.Value;
            }
        }

        public float Width
        {
            get
            {
                if (mWidth == null)
                    mWidth = _InteropUIImage_Width(SelfPtr());
                return mWidth.Value;
            }
        }

        public float Height
        {
            get
            {
                if (mHeight == null)
                    mHeight = _InteropUIImage_Height(SelfPtr());
                return mHeight.Value;
            }
        }

        public byte[] GetPNGData()
        {
            return PInvokeUtil.GetNativeArray<byte>((buffer, length) =>
                _InteropUIImage_GetPNGData(SelfPtr(), buffer, length));
        }

        public byte[] GetJPEGData(float compressionQuality)
        {
            compressionQuality = UnityEngine.Mathf.Clamp(compressionQuality, 0, 1);
            return PInvokeUtil.GetNativeArray<byte>((buffer, length) =>
                _InteropUIImage_GetJPEGData(SelfPtr(), compressionQuality, buffer, length));
        }
    }
}
#endif
