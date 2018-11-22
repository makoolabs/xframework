using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyMobile
{
    [Serializable]
    public class PrivacySettings
    {
        public ConsentDialog DefaultConsentDialog { get { return mDefaultConsentDialog; } }

        [SerializeField]
        private ConsentDialog mDefaultConsentDialog;

        [SerializeField]
        private ConsentDialogComposerSettings mConsentDialogComposerSettings;
    }

    [Serializable]
    public class ConsentDialogComposerSettings
    {
        [SerializeField]
        private int mToggleSelectedIndex;
        [SerializeField]
        private int mButtonSelectedIndex;
        [SerializeField]
        private bool mEnableCopyPasteMode;
    }
}
