using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace EasyMobile.Internal
{
    internal static class PInvokeCallbackUtil
    {
        private static readonly bool VERBOSE_DEBUG = false;

        internal static IntPtr ToIntPtr<T>(Action<T> callback, Func<IntPtr, T> conversionFunction)
        where T : InteropObject
        {
            Action<IntPtr> pointerReceiver = result =>
            {
                using (T converted = conversionFunction(result))
                {
                    if (callback != null)
                    {
                        callback(converted);
                    }
                }
            };

            return ToIntPtr(pointerReceiver);
        }

        internal static IntPtr ToIntPtr<T, P>(Action<T, P> callback, Func<IntPtr, T> conversionFunction)
        where T : InteropObject
        {
            Action<IntPtr, P> pointerReceiver = (param1, param2) =>
            {
                using (T converted = conversionFunction(param1))
                {
                    if (callback != null)
                    {
                        callback(converted, param2);
                    }
                }
            };

            return ToIntPtr(pointerReceiver);
        }

        internal static IntPtr ToIntPtr(Delegate callback)
        {
            if (callback == null)
            {
                return IntPtr.Zero;
            }

            // Use a GCHandle to retain the callback, it will be freed when the callback returns the and
            // handle is converted back to callback via IntPtrToCallback.
            var handle = GCHandle.Alloc(callback);
            return GCHandle.ToIntPtr(handle);
        }

        internal static T IntPtrToTempCallback<T>(IntPtr handle) where T : class
        {
            return IntPtrToCallback<T>(handle, true);
        }

        private static T IntPtrToCallback<T>(IntPtr handle, bool unpinHandle) where T : class
        {
            if (PInvokeUtil.IsNull(handle))
            {
                return null;
            }

            var gcHandle = GCHandle.FromIntPtr(handle);
            try
            {
                return (T)gcHandle.Target;
            }
            catch (System.InvalidCastException e)
            {
                Debug.LogError("GC Handle pointed to unexpected type: " + gcHandle.Target.ToString() +
                    ". Expected " + typeof(T));
                throw e;
            }
            finally
            {
                if (unpinHandle)
                {
                    gcHandle.Free();
                }
            }
        }

        internal static T IntPtrToPermanentCallback<T>(IntPtr handle) where T : class
        {
            return IntPtrToCallback<T>(handle, false);
        }

        internal enum Type
        {
            Permanent,
            Temporary}

        ;

        internal static void PerformInternalCallback(string callbackName, Type callbackType,
                                                     IntPtr response, IntPtr callbackPtr)
        {
            if (VERBOSE_DEBUG)
                Debug.Log("Entering internal callback for " + callbackName);

            Action<IntPtr> callback = null;
            try
            {
                callback = callbackType == Type.Permanent
                ? IntPtrToPermanentCallback<Action<IntPtr>>(callbackPtr)
                    : IntPtrToTempCallback<Action<IntPtr>>(callbackPtr);
            }
            catch (Exception e)
            {
                Debug.LogError("Error encountered converting " + callbackName + ". " +
                    "Smothering to avoid passing exception into Native: " + e);
                return;
            }

            if (VERBOSE_DEBUG)
                Debug.Log("Internal Callback converted to action");

            if (callback == null)
            {
                return;
            }

            try
            {
                callback(response);
            }
            catch (Exception e)
            {
                Debug.LogError("Error encountered executing " + callbackName + ". " +
                    "Smothering to avoid passing exception into Native: " + e);
            }
        }

        internal static void PerformInternalCallback<T>(string callbackName, Type callbackType,
                                                        IntPtr param1, T param2, IntPtr callbackPtr)
        {
            if (VERBOSE_DEBUG)
                Debug.Log("Entering internal callback for " + callbackName);
            
            Action<IntPtr, T> callback = null;
            try
            {
                callback = callbackType == Type.Permanent
                ? IntPtrToPermanentCallback<Action<IntPtr, T>>(callbackPtr)
                    : IntPtrToTempCallback<Action<IntPtr, T>>(callbackPtr);
            }
            catch (Exception e)
            {
                Debug.LogError("Error encountered converting " + callbackName + ". " +
                    "Smothering to avoid passing exception into Native: " + e);
                return;
            }

            if (VERBOSE_DEBUG)
                Debug.Log("Internal Callback converted to action");
            
            if (callback == null)
            {
                return;
            }

            try
            {
                callback(param1, param2);
            }
            catch (Exception e)
            {
                Debug.LogError("Error encountered executing " + callbackName + ". " +
                    "Smothering to avoid passing exception into Native: " + e);
            }
        }

        internal static void PerformInternalCallback<T, P>(string callbackName, Type callbackType,
                                                           T[] param1, P param2, IntPtr callbackPtr)
        {
            if (VERBOSE_DEBUG)
                Debug.Log("Entering internal callback for " + callbackName);
            
            Action<T[], P> callback = null;
            try
            {
                callback = callbackType == Type.Permanent
                    ? IntPtrToPermanentCallback<Action<T[], P>>(callbackPtr)
                    : IntPtrToTempCallback<Action<T[], P>>(callbackPtr);
            }
            catch (Exception e)
            {
                Debug.LogError("Error encountered converting " + callbackName + ". " +
                    "Smothering to avoid passing exception into Native: " + e);
                return;
            }

            if (VERBOSE_DEBUG)
                Debug.Log("Internal Callback converted to action");
            
            if (callback == null)
            {
                return;
            }

            try
            {
                callback(param1, param2);
            }
            catch (Exception e)
            {
                Debug.LogError("Error encountered executing " + callbackName + ". " +
                    "Smothering to avoid passing exception into Native: " + e);
            }
        }

        internal static void PerformInternalCallback<T>(string callbackName, Type callbackType,
                                                        T param, IntPtr callbackPtr)
        {
            if (VERBOSE_DEBUG)
                Debug.Log("Entering internal callback for " + callbackName);
            
            Action<T> callback = null;
            try
            {
                callback = callbackType == Type.Permanent
                    ? IntPtrToPermanentCallback<Action<T>>(callbackPtr)
                    : IntPtrToTempCallback<Action<T>>(callbackPtr);
            }
            catch (Exception e)
            {
                Debug.LogError("Error encountered converting " + callbackName + ". " +
                    "Smothering to avoid passing exception into Native: " + e);
                return;
            }

            if (VERBOSE_DEBUG)
                Debug.Log("Internal Callback converted to action");
            
            if (callback == null)
            {
                return;
            }

            try
            {
                callback(param);
            }
            catch (Exception e)
            {
                Debug.LogError("Error encountered executing " + callbackName + ". " +
                    "Smothering to avoid passing exception into Native: " + e);
            }
        }
    }
}

