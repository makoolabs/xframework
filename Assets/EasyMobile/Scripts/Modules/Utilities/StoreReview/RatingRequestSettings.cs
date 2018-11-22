using UnityEngine;
using System.Collections;

namespace EasyMobile
{
    [System.Serializable]
    public class RatingRequestSettings
    {
        public RatingDialogContent DefaultRatingDialogContent
        { 
            get { return mDefaultRatingDialogContent; } 
        }

        public uint MinimumAcceptedStars
        { 
            get { return mMinimumAcceptedStars; } 
        }

        public string SupportEmail
        { 
            get { return mSupportEmail; }
        }

        public string IosAppId
        { 
            get { return mIosAppId; }
        }

        public uint AnnualCap
        { 
            get { return mAnnualCap; }
        }

        public uint DelayAfterInstallation
        { 
            get { return mDelayAfterInstallation; }
        }

        public uint CoolingOffPeriod
        { 
            get { return mCoolingOffPeriod; }
        }

        public bool IgnoreConstraintsInDevelopment
        { 
            get { return mIgnoreContraintsInDevelopment; }
        }

        // Appearance
        [SerializeField]
        private RatingDialogContent mDefaultRatingDialogContent = RatingDialogContent.Default;

        // Behaviour
        [SerializeField][Range(0, 5)]
        private uint mMinimumAcceptedStars = 4;
        [SerializeField]
        private string mSupportEmail;
        [SerializeField]
        private string mIosAppId;

        // Display constraints
        [SerializeField][Range(3, 100)]
        private uint mAnnualCap = 12;
        [SerializeField][Range(0, 365)]
        private uint mDelayAfterInstallation = 10;
        [SerializeField][Range(0, 365)]
        private uint mCoolingOffPeriod = 10;
        [SerializeField]
        private bool mIgnoreContraintsInDevelopment = false;
    }
}

