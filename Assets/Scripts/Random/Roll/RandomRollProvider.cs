using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;

namespace Random.Roll
{
    public class RandomRollProvider : IRandomRollProvider
    {
        private readonly SlotMachineConfig _slotMachineConfig;

        public RandomRollProvider(SlotMachineConfig slotMachineConfig)
        {
            _slotMachineConfig = slotMachineConfig;
        }
        
        public int SelectRandomCombination(IList<int> availableCombinationIndexes)
        {
            var totalWeight = availableCombinationIndexes.Sum(combinationIndex => _slotMachineConfig.SlotCombinationConfigs[combinationIndex].Percentage);
            var randomWeight = UnityEngine.Random.Range(0, totalWeight);

            var totalPercentage = 0;
            foreach (var combinationIndex in availableCombinationIndexes)
            {
                var combinationConfig = _slotMachineConfig.SlotCombinationConfigs[combinationIndex];
                totalPercentage += combinationConfig.Percentage;
                if (randomWeight < totalPercentage)
                {
                    return combinationIndex;
                }
            }

            throw new Exception($"Random index could not determined correctly, available combination indexes: {string.Join(",", availableCombinationIndexes)}");
        }
    }
}