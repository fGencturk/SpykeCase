using UnityEngine;

namespace Infrastructure.Save
{
    public class SlotMachinePlayerData : IPlayerData
    {
        private const string SlotMachinePrefPrefix = "Core.Save.SlotMachinePlayerData.SlotMachinePrefPrefix";
        private const string CurrentRollIndexPrefKey = "Core.Save.SlotMachinePlayerData.CurrentRollIndex";

        #region Properties

        public int CurrentRollIndex
        {
            get => PlayerPrefs.GetInt(CurrentRollIndexPrefKey, -1);
            set => PlayerPrefs.SetInt(CurrentRollIndexPrefKey, value);
        }

        #endregion

        public int GetLastHitIndex(int slotIndex)
        {
            return PlayerPrefs.GetInt($"{SlotMachinePrefPrefix}{slotIndex}", -1);
        }

        public void SetLastHitIndex(int slotIndex, int lastHitIndex)
        {
            PlayerPrefs.SetInt($"{SlotMachinePrefPrefix}{slotIndex}", lastHitIndex);
        }
        
        
    }
}