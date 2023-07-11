using System;
using Common;
using Common.Model;
using Core.Data;

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
            // TODO create lookup & optimize here
            var combinationConfig = _slotMachineConfig.SlotCombinationConfigs[combinationIndex];
            for (var i = 0; i < combinationConfig.Percentage; i++)
            {
                var interval = Utilities.GetInterval(combinationConfig.Percentage, i);
                if (interval.Min <= currentRollIndex && currentRollIndex <= interval.Max)
                {
                    return interval;
                }
            }

            throw new Exception("Not found");
        }
    }
}