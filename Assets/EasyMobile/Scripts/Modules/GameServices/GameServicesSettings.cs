using UnityEngine;
using System.Collections;

namespace EasyMobile
{
    [System.Serializable]
    public partial class GameServicesSettings
    {
        public bool IsAutoInit { get { return mAutoInit; } }

        public float AutoInitDelay { get { return mAutoInitDelay; } }

        public int AndroidMaxLoginRequests { get { return mAndroidMaxLoginRequests; } }

        public bool GgpsDebugLogEnabled { get { return mGpgsDebugLogEnabled; } set { mGpgsDebugLogEnabled = value; } }

        public GpgsGravity GpgsPopupGravity { get { return mGpgsPopupGravity; } set { mGpgsPopupGravity = value; } }

        public Leaderboard[] Leaderboards { get { return mLeaderboards; } }

        public Achievement[] Achievements { get { return mAchievements; } }

        // Auto-init config
        [SerializeField]
        private bool mAutoInit = true;
        [SerializeField][Range(0, 60)]
        private float mAutoInitDelay = 0f;
        [SerializeField][Range(1, 30)]
        private int mAndroidMaxLoginRequests = 3;

        // GPGS setup.
        [SerializeField]
        private bool mGpgsDebugLogEnabled = false;
        [SerializeField]
        private GpgsGravity mGpgsPopupGravity = GpgsGravity.Top;

        // Leaderboards & Achievements
        [SerializeField]
        private Leaderboard[] mLeaderboards;
        [SerializeField]
        private Achievement[] mAchievements;

        // GPGS setup resources.
        // These fields are only used in our editor, hence the warning suppression.
        #pragma warning disable 0414
        [SerializeField]
        private string mAndroidXmlResources = string.Empty;
        #pragma warning restore 0414

        public enum GpgsGravity
        {
            Top,
            Bottom,
            Left,
            Right,
            CenterHorizontal
        }
    }
}

