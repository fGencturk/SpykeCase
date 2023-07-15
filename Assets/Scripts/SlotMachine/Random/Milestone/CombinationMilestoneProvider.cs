using Common;
using Common.Model;
using Core.Data;
using UnityEngine;

namespace Random.Milestone
{
    public class CombinationMilestoneProvider : ICombinationMilestoneProvider
    {
        private readonly SlotMachineConfig _slotMachineConfig;

        public CombinationMilestoneProvider(SlotMachineConfig slotMachineConfig)
        {
            _slotMachineConfig = slotMachineConfig;
        }
        
        public IntervalInt GetCombinationMilestone(int combinationIndex, int currentRollIndex)
        {
            var combinationConfig = _slotMachineConfig.SlotCombinationConfigs[combinationIndex];
            var currentIntervalIndex = Mathf.FloorToInt(currentRollIndex / (Constants.TotalPercentageWeight / (float)combinationConfig.Percentage));
            return Utilities.GetInterval(combinationConfig.Percentage, currentIntervalIndex);
        }
    }
}