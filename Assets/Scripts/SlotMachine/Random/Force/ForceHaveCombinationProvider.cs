using System.Collections.Generic;
using System.Linq;
using Common;
using Core.Data;

namespace Random.Force
{
    public class ForceHaveCombinationProvider : IForceHaveCombinationProvider
    {
        private List<List<int>> _rollIndexToForceHaveCombinations;

        public ForceHaveCombinationProvider(SlotMachineConfig slotMachineConfig)
        {
            _rollIndexToForceHaveCombinations = new List<List<int>>();
            _rollIndexToForceHaveCombinations.AddRange(Enumerable.Repeat<List<int>>(null, Constants.TotalPercentageWeight));

            for (var combinationIndex = 0; combinationIndex < slotMachineConfig.SlotCombinationConfigs.Count; combinationIndex++)
            {
                var slotCombinationConfig = slotMachineConfig.SlotCombinationConfigs[combinationIndex];
                for (var index = 0; index < slotCombinationConfig.Percentage; index++)
                {
                    var interval = Utilities.GetInterval(slotCombinationConfig.Percentage, index);
                    AddCombinationAsForceHave(combinationIndex, interval.Max);
                }
            }
        }

        private void AddCombinationAsForceHave(int combinationIndex, int rollIndex)
        {
            _rollIndexToForceHaveCombinations[rollIndex] ??= new List<int>();
            _rollIndexToForceHaveCombinations[rollIndex].Add(combinationIndex);
        }
        
        public bool TryGetForceHaveCombinationsList(int currentRollIndex, out IList<int> forceHaveCombinationList)
        {
            forceHaveCombinationList = _rollIndexToForceHaveCombinations[currentRollIndex];
            return forceHaveCombinationList != null;
        }
    }
}