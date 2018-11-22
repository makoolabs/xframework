using UnityEngine;
using System.Collections;

#if UNITY_IOS
using System.Runtime.InteropServices;
using AOT;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EasyMobile.Internal.Gif.iOS
{
    internal static class iOSNativeGif
    {
        internal static event Action<int, float> GifExportProgress;
        internal static event Action<int, string> GifExportCompleted;

        private delegate void GifExportProgressDelegate(int taskId,float progress);

        private delegate void GifExportCompletedDelegate(int taskId,string filepath);

        [MonoPInvokeCallback(typeof(GifExportProgressDelegate))]
        private static void GifExportProgressCallback(int taskId, float progress)
        {
            if (GifExportProgress != null)
                GifExportProgress(taskId, progress);
        }

        [MonoPInvokeCallback(typeof(GifExportCompletedDelegate))]
        private static void GifExportCompletedCallback(int taskId, string filepath)
        {
            if (GifExportCompleted != null)
                GifExportCompleted(taskId, filepath);

            // Free the GC handles and IntPtr
            foreach (var gch in gcHandles[taskId])
            {
                gch.Free();
            }

            gcHandles.Remove(taskId);
        }

        // Store the array of GCHandles for each exporting task
        private static Dictionary<int, GCHandle[]> gcHandles;

        [DllImport("__Internal")]
        private static extern void _ExportGif(int taskId, 
                                              string filepath, 
                                              int width, 
                                              int height, 
                                              int loop, 
                                              int fps, 
                                              int sampleFac,  
                                              int frameCount, 
                                              IntPtr[] imageData, 
                                              int threadPriority, 
                                              GifExportProgressDelegate exportingCallback, 
                                              GifExportCompletedDelegate exportCompletedCallback);

        internal static void ExportGif(GifExportTask task)
        {
            var taskId = task.taskId;
            var filepath = task.filepath;
            var width = task.clip.Width;
            var height = task.clip.Height;
            var loop = task.loop;
            var fps = task.clip.FramePerSecond;
            var sampleFac = task.sampleFac;
            var frameCount = task.clip.Frames.Length;
            var imageData = task.imageData;
            var threadPriority = EncodeThreadPriority(task.workerPriority);

            var gcHandleArray = new GCHandle[imageData.Length];
            var ptrArray = new IntPtr[imageData.Length];

            for (int i = 0; i < imageData.Length; i++)
            {
                gcHandleArray[i] = GCHandle.Alloc(imageData[i], GCHandleType.Pinned);
                ptrArray[i] = gcHandleArray[i].AddrOfPinnedObject();
            }
                
            if (gcHandles == null)
                gcHandles = new Dictionary<int, GCHandle[]>();

            gcHandles.Add(taskId, gcHandleArray);

            _ExportGif(taskId, 
                filepath, 
                width, 
                height, 
                loop, 
                fps, 
                sampleFac,
                frameCount, 
                ptrArray, 
                threadPriority, 
                GifExportProgressCallback, 
                GifExportCompletedCallback);
        }

        private static int EncodeThreadPriority(System.Threading.ThreadPriority priority)
        {
            switch (priority)
            {
                case System.Threading.ThreadPriority.Lowest:
                    return -2;
                case System.Threading.ThreadPriority.BelowNormal:
                    return -1;
                case System.Threading.ThreadPriority.Normal:
                    return 0;
                case System.Threading.ThreadPriority.AboveNormal:
                    return 1;
                case System.Threading.ThreadPriority.Highest:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
#endif
