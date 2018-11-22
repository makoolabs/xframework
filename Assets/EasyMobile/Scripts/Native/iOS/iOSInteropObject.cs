#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace EasyMobile.iOS
{
    internal class iOSInteropObject : InteropObject
    {
        [DllImport("__Internal")]
        private static extern void _InteropObject_Ref(HandleRef self);

        [DllImport("__Internal")]
        private static extern void _InteropObject_Unref(HandleRef self);

        internal iOSInteropObject(IntPtr selfPointer)
            : base(selfPointer)
        {
        }

        protected override void AttachHandle(HandleRef selfPointer)
        {
            _InteropObject_Ref(selfPointer);
        }

        protected override void ReleaseHandle(HandleRef selfPointer)
        {
            _InteropObject_Unref(selfPointer);
        }
    }
}
#endif
