using System;
using System.Collections.Generic;
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
        
        public int SelectRandomCombination(IEnumerator<int> availableCombinationIndexes)
        {
            var totalWeight = 0;
            while (availableCombinationIndexes.MoveNext())
            {
                var combinationIndex = availableCombinationIndexes.Current;
                var combinationConfig = _slotMachineConfig.SlotCombinationConfigs[combinationIndex];
                totalWeight += combinationConfig.Percentage;
            }
            availableCombinationIndexes.Reset();
            
            var randomWeight = UnityEngine.Random.Range(0, totalWeight);

            var totalPercentage = 0;
            while (availableCombinationIndexes.MoveNext())
            {
                var combinationIndex = availableCombinationIndexes.Current;
                
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