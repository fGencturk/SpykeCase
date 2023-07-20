using Common;
using Common.Model;
using Core.Data;
using UnityEngine;

namespace SlotMachine.Random.Milestone
{
    public class CombinationMilestoneProvider : ICombinationMilestoneProvider
    {
        private readonly SlotMachineConfig _slotMachineConfig;

        public CombinationMilestoneProvider(SlotMachineConfig slotMachineConfig)
        {
            _slotMachineConfig = slotMachineConfig;
        }
        
        public IntervalInt GetCombinationMilestoneOfRollIndex(int combinationIndex, int currentRollIndex)
        {
            var combinationConfig = _slotMachineConfig.SlotCombinationConfigs[combinationIndex];
            var currentIntervalIndex = Mathf.FloorToInt(currentRollIndex / (Constants.TotalPercentageWeight / (float)combinationConfig.Percentage));
            return GetCombinationMilestoneOfIntervalIndex(combinationIndex, currentIntervalIndex);
        }

        public IntervalInt GetCombinationMilestoneOfIntervalIndex(int combinationIndex, int intervalIndex)
        {
            var combinationConfig = _slotMachineConfig.SlotCombinationConfigs[combinationIndex];
            return Utilities.GetInterval(combinationConfig.Percentage, intervalIndex);
        }
    }
}