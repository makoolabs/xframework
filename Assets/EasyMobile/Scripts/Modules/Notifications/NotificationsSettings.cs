using UnityEngine;
using System;
using System.Collections;
using EasyMobile.Internal;

namespace EasyMobile
{
    [System.Serializable]
    public class NotificationsSettings
    {
        public const string DEFAULT_CATEGORY_ID = "notification.category.default";
        public const string DEFAULT_CATEGORY_NAME = "Default";

        public bool IsAutoInit { get { return mAutoInit; } }

        public float AutoInitDelay { get { return mAutoInitDelay; } }

        public NotificationAuthOptions iOSAuthOptions { get { return mIosAuthOptions; } }

        public PushNotificationProvider PushNotificationService { get { return mPushNotificationService; } }

        public string OneSignalAppId { get { return mOneSignalAppId; } }

        public string[] FirebaseTopics { get { return mFirebaseTopics; } }

        public NotificationCategoryGroup[] CategoryGroups { get { return mCategoryGroups; } }

        public NotificationCategory DefaultCategory { get { return mDefaultCategory; } }

        public NotificationCategory[] UserCategories { get { return mUserCategories; } }

        // Initialization config
        [SerializeField]
        private bool mAutoInit = true;
        [SerializeField][Range(0, 60)]
        private float mAutoInitDelay = 0f;

        // iOS authorization options
        [SerializeField][EnumFlags]
        private NotificationAuthOptions mIosAuthOptions = NotificationAuthOptions.Alert | NotificationAuthOptions.Badge | NotificationAuthOptions.Sound;

        // Remote notification settings
        [SerializeField]
        private PushNotificationProvider mPushNotificationService = PushNotificationProvider.None;

        [SerializeField]
        private string mOneSignalAppId;

        [SerializeField]
        private string[] mFirebaseTopics;

        // Category groups
        [SerializeField]
        private NotificationCategoryGroup[] mCategoryGroups;

        // Default notification category
        [SerializeField]
        private NotificationCategory mDefaultCategory = new NotificationCategory()
        {
            id = DEFAULT_CATEGORY_ID,
            name = DEFAULT_CATEGORY_NAME
        };

        // User categories
        [SerializeField]
        private NotificationCategory[] mUserCategories;

        public NotificationCategory GetCategoryWithId(string categoryId)
        {
            if (!string.IsNullOrEmpty(categoryId))
            {
                if (categoryId.Equals(DefaultCategory.id))
                {
                    return DefaultCategory;
                }
                else if (UserCategories != null)
                {
                    foreach (var c in UserCategories)
                    {
                        if (categoryId.Equals(c.id))
                            return c;
                    }
                }
            }

            return null;
        }
    }

    public enum PushNotificationProvider
    {
        None,
        OneSignal,
        Firebase,
    }

    [Flags]
    public enum NotificationAuthOptions
    {
        Alert = 1 << 0,
        Badge = 1 << 1,
        Sound = 1 << 2,
    }
}

