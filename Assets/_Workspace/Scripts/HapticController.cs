using System;
using Lofelt.NiceVibrations;
using UnityEngine;

namespace _Workspace.Scripts
{
    public class HapticController : MonoBehaviour
    {
        #region Unity Funcs

        private void OnEnable()
        {
            TileHolder.OnTilesPopped += TileHolderOnOnTilesPopped;
            TileHolder.OnTilePlaced += TileHolderOnOnTilePlaced;
            TileHolder.OnGameCompleted += TileHolderOnOnGameCompleted;
            TileHolder.OnGameFailed += TileHolderOnOnGameFailed;
        }

        private void OnDisable()
        {
            TileHolder.OnTilesPopped -= TileHolderOnOnTilesPopped;
            TileHolder.OnTilePlaced -= TileHolderOnOnTilePlaced;
            TileHolder.OnGameCompleted -= TileHolderOnOnGameCompleted;
            TileHolder.OnGameFailed -= TileHolderOnOnGameFailed;
        }

        #endregion

        #region Unity Event Funcs

        private void TileHolderOnOnGameFailed()
        {
            PlayHaptic(HapticPatterns.PresetType.Failure);
        }

        private void TileHolderOnOnGameCompleted()
        {
            PlayHaptic(HapticPatterns.PresetType.Success);
        }

        
        private void TileHolderOnOnTilesPopped(TileHolder.OnTilesPoppedActionClass arg0)
        {
            PlayHaptic(HapticPatterns.PresetType.HeavyImpact);
        }

        private void TileHolderOnOnTilePlaced()
        {
            PlayHaptic(HapticPatterns.PresetType.MediumImpact);
        }

        #endregion

        #region Haptic Funcs

        private void PlayHaptic(HapticPatterns.PresetType hapticPreset)
        {
                HapticPatterns.PlayPreset(hapticPreset);
        }
        #endregion

    }
}