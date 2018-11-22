using UnityEngine;
using System;

namespace EasyMobile
{
    public partial class GameServicesSettings
    {
        public bool IsSavedGamesEnabled { get { return mEnableSavedGames; } }

        public SavedGameConflictResolutionStrategy AutoConflictResolutionStrategy { get { return mAutoConflictResolutionStrategy; } set { mAutoConflictResolutionStrategy = value; } }

        public GPGSSavedGameDataSource GPGSDataSource { get { return mGpgsDataSource; } set { mGpgsDataSource = value; } }

        [SerializeField]
        private bool mEnableSavedGames = false;
        [SerializeField]
        private SavedGameConflictResolutionStrategy mAutoConflictResolutionStrategy = SavedGameConflictResolutionStrategy.UseBase;
        [SerializeField]
        private GPGSSavedGameDataSource mGpgsDataSource = GPGSSavedGameDataSource.ReadCacheOrNetwork;
    }
}

