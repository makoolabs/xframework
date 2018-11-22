using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace EasyMobile.Demo
{
    public class DemoUtils : MonoBehaviour
    {
        public Sprite checkedSprite;
        public Sprite uncheckedSprite;

        public void GoHome()
        {
            SceneManager.LoadScene("DemoHome");
        }

        public void AdvertisingDemo()
        {
            SceneManager.LoadScene("AdvertisingDemo");
        }

        public void GameServiceDemo()
        {
            SceneManager.LoadScene("GameServicesDemo");
        }

        public void GameServiceDemo_SavedGames()
        {
            #if EASY_MOBILE_PRO
            SceneManager.LoadScene("GameServicesDemo_SavedGames");
            #else
            NativeUI.Alert("Feature Unavailable", "Saved Games feature is only available on Easy Mobile Pro.");
            #endif
        }

        public void GifDemo()
        {
            SceneManager.LoadScene("GifDemo");
        }

        public void InAppPurchaseDemo()
        {
            SceneManager.LoadScene("InAppPurchasingDemo");
        }

        public void SharingDemo()
        {
            SceneManager.LoadScene("SharingDemo");
        }

        public void NativeUIDemo()
        {
            SceneManager.LoadScene("NativeUIDemo");
        }

        public void UtilitiesDemo()
        {
            SceneManager.LoadScene("UtilitiesDemo");
        }

        public void NotificationsDemo()
        {
            SceneManager.LoadScene("NotificationsDemo");
        }

        public void PrivacyDemo()
        {
            SceneManager.LoadScene("PrivacyDemo");
        }

        public void DisplayBool(GameObject infoObj, bool state, string msg)
        {
            Image img = infoObj.GetComponentInChildren<Image>();
            Text txt = infoObj.GetComponentInChildren<Text>();

            if (img == null || txt == null)
            {
                Debug.LogError("Could not found Image or Text component beneath object: " + infoObj.name);
            }

            if (state)
            {
                img.sprite = checkedSprite;
                img.color = Color.green;
            }
            else
            {
                img.sprite = uncheckedSprite;
                img.color = Color.red;
            }

            txt.text = msg;
        }

        public void PlayButtonSound()
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        }
    }
}
