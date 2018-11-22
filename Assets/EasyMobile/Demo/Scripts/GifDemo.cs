using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyMobile;

namespace EasyMobile.Demo
{
    [AddComponentMenu("")]
    public class GifDemo : MonoBehaviour
    {
        [Header("GIF Settings")]
        public Recorder recorder;
        public string gifFilename = "easy_mobile_demo";
        public int loop = 0;
        [Range(1, 100)]
        public int quality = 90;
        public System.Threading.ThreadPriority exportThreadPriority;

        [Header("Giphy Upload Key")]
        public string giphyUsername;
        public string giphyApiKey;

        [Header("UI Stuff")]
        public GameObject recordingMark;
        public GameObject startRecordingButton;
        public GameObject stopRecordingButton;
        public GameObject playbackPanel;
        public ClipPlayerUI clipPlayer;
        public GameObject giphyLogo;
        public GameObject activityProgress;
        public Text activityText;
        public Text progressText;

        AnimatedClip recordedClip;
        bool isExportingGif;
        bool isUploadingGif;
        string exportedGifPath;
        string uploadedGifUrl;

        void OnDestroy()
        {
            // Dispose the used clip if needed
            if (recordedClip != null)
                recordedClip.Dispose(); 
        }

        void Awake()
        {
            // Init EM runtime if needed (useful in case only this scene is built).
            if (!RuntimeManager.IsInitialized())
                RuntimeManager.Init();
        }

        void Start()
        {
            startRecordingButton.SetActive(true);
            stopRecordingButton.SetActive(false);
            recordingMark.SetActive(false);
            playbackPanel.SetActive(false);
            activityProgress.SetActive(false);
        }

        void Update()
        { 
            giphyLogo.SetActive(Giphy.IsUsingAPI);
            activityProgress.SetActive(isExportingGif || isUploadingGif);
        }

        public void StartRecording()
        {
            // Dispose the old clip
            if (recordedClip != null)
                recordedClip.Dispose();
            
            Gif.StartRecording(recorder);
            startRecordingButton.SetActive(false);
            stopRecordingButton.SetActive(true);
            recordingMark.SetActive(true);
        }

        public void StopRecording()
        {
            recordedClip = Gif.StopRecording(recorder);
            startRecordingButton.SetActive(true);
            stopRecordingButton.SetActive(false);
            recordingMark.SetActive(false);
        }

        public void OpenPlaybackPanel()
        {
            if (recordedClip != null)
            {
                playbackPanel.SetActive(true);
                Gif.PlayClip(clipPlayer, recordedClip, 1f);
            }
            else
            {
                NativeUI.Alert("Nothing Recorded", "Please finish recording first.");
            }
        }

        public void ClosePlaybackPanel()
        {
            clipPlayer.Stop();
            playbackPanel.SetActive(false);
        }

        public void ExportGIF()
        {
            if (isExportingGif)
            {
                NativeUI.Alert("Exporting In Progress", "Please wait until the current GIF exporting is completed.");
                return;
            }
            else if (isUploadingGif)
            {
                NativeUI.Alert("Uploading In Progress", "Please wait until the GIF uploading is completed.");
                return;
            }

            isExportingGif = true;
            Gif.ExportGif(recordedClip, gifFilename, loop, quality, exportThreadPriority, OnGifExportProgress, OnGifExportCompleted);
        }

        public void UploadGIFToGiphy()
        {
            if (isExportingGif)
            {
                NativeUI.Alert("Exporting In Progress", "Please wait until the GIF exporting is completed.");
                return;
            }
            else if (string.IsNullOrEmpty(exportedGifPath))
            {
                NativeUI.Alert("No Exported GIF", "Please export a GIF file first.");
                return;
            }
                
            isUploadingGif = true;

            var content = new GiphyUploadParams();
            content.localImagePath = exportedGifPath;
            content.tags = "demo, easy mobile, sglib games, unity3d";

            if (!string.IsNullOrEmpty(giphyUsername) && !string.IsNullOrEmpty(giphyApiKey))
                Giphy.Upload(giphyUsername, giphyApiKey, content, OnGiphyUploadProgress, OnGiphyUploadCompleted, OnGiphyUploadFailed);
            else
                Giphy.Upload(content, OnGiphyUploadProgress, OnGiphyUploadCompleted, OnGiphyUploadFailed);
        }

        public void ShareGiphyURL()
        {
            if (string.IsNullOrEmpty(uploadedGifUrl))
            {
                NativeUI.Alert("Invalid URL", "No valid Giphy URL found. Did the upload succeed?");
                return;
            }

            Sharing.ShareURL(uploadedGifUrl);
        }

        // This callback is called repeatedly during the GIF exporting process.
        // It receives a progress value ranging from 0 to 1.
        void OnGifExportProgress(AnimatedClip clip, float progress)
        {
            activityText.text = "GENERATING GIF...";
            progressText.text = string.Format("{0:P0}", progress);
        }

        // This callback is called once the GIF exporting has completed.
        // It receives the filepath of the generated image.
        void OnGifExportCompleted(AnimatedClip clip, string path)
        {
            progressText.text = "DONE";

            isExportingGif = false;
            exportedGifPath = path;

            #if UNITY_EDITOR
            bool shouldUpload = UnityEditor.EditorUtility.DisplayDialog("Export Completed", "A GIF file has been created. Do you want to upload it to Giphy?", "Yes", "No");
            if (shouldUpload)
                UploadGIFToGiphy();
            #else
            NativeUI.AlertPopup popup = NativeUI.ShowTwoButtonAlert("Export Completed", "A GIF file has been created. Do you want to upload it to Giphy?", "Yes", "No");
            if (popup != null)
            {
                popup.OnComplete += (int buttonId) =>
                {
                    if (buttonId == 0)
                        UploadGIFToGiphy();
                };
            }
            #endif
        }

        // This callback is called repeatedly during the uploading process.
        // It receives a progress value ranging from 0 to 1.
        void OnGiphyUploadProgress(float progress)
        {
            activityText.text = "UPLOADING TO GIPHY...";
            progressText.text = string.Format("{0:P0}", progress);
        }

        // This callback is called once the uploading has completed.
        // It receives the URL of the uploaded image.
        void OnGiphyUploadCompleted(string url)
        {
            progressText.text = "DONE";

            isUploadingGif = false;
            uploadedGifUrl = url;
            #if UNITY_EDITOR
            bool shouldOpen = UnityEditor.EditorUtility.DisplayDialog("Upload Completed", "The GIF image has been uploaded to Giphy at " + url + ". Open it in the browser?", "Yes", "No");
            if (shouldOpen)
                Application.OpenURL(uploadedGifUrl);
            #else
            NativeUI.AlertPopup alert = NativeUI.ShowTwoButtonAlert("Upload Completed", "The GIF image has been uploaded to Giphy at " + url + ". Open it in the browser?", "Yes", "No");

            if (alert != null)
            {
                alert.OnComplete += (int buttonId) =>
                {
                    if (buttonId == 0)
                        Application.OpenURL(uploadedGifUrl);
                };
            }
            #endif
        }

        // This callback is called if the upload has failed.
        // It receives the error message.
        void OnGiphyUploadFailed(string error)
        {
            isUploadingGif = false;
            NativeUI.Alert("Upload Failed", "Uploading to Giphy has failed with error " + error);
        }
    }
}

    