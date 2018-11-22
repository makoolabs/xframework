using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
using System.Runtime.InteropServices;
using AOT;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EasyMobile.Internal.Gif.Android
{
    internal class AndroidNativeGif
    {
        private GifExportTask myExportTask;

        internal AndroidNativeGif(GifExportTask exportTask)
        {
            myExportTask = exportTask;
        }

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

        [DllImport("easymobile")]
        private static extern void _ExportGif(int taskId, 
                                              string filepath, 
                                              int width, 
                                              int height, 
                                              int loop, 
                                              int fps, 
                                              int sampleFac, 
                                              int frameCount, 
                                              IntPtr[] imageData, 
                                              GifExportProgressDelegate exportingCallback, 
                                              GifExportCompletedDelegate exportCompletedCallback);

        internal static void ExportGif(GifExportTask exportTask)
        {
            var instance = new AndroidNativeGif(exportTask);
            var worker = new Thread(instance.DoExportGif);
            worker.Priority = exportTask.workerPriority;
            worker.Start();
        }

        private void DoExportGif()
        {
            var taskId = myExportTask.taskId;
            var filepath = myExportTask.filepath;
            var width = myExportTask.clip.Width;
            var height = myExportTask.clip.Height;
            var loop = myExportTask.loop;
            var fps = myExportTask.clip.FramePerSecond;
            var sampleFac = myExportTask.sampleFac;
            var frameCount = myExportTask.clip.Frames.Length;
            var imageData = myExportTask.imageData;

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
                GifExportProgressCallback, 
                GifExportCompletedCallback);
        }
    }
}
#endif