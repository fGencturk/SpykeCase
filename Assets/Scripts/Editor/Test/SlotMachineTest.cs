using System.Collections.Generic;
using System.Text;
using Common;
using Common.Model;
using Core.Data;
using Infrastructure.Save;
using SlotMachine.Random;
using SlotMachine.Random.Force;
using SlotMachine.Random.Milestone;
using SlotMachine.Random.Roll;
using UnityEngine;

namespace Editor.Test
{
    public class SlotMachineTest
    {
        private readonly SlotMachineConfig _slotMachineConfig;

        public SlotMachineTest(SlotMachineConfig slotMachineConfig)
        {
            _slotMachineConfig = slotMachineConfig;
        }

        public void Execute()
        {
            PlayerPrefs.DeleteAll();
            
            var forceHaveCombinationProvider = new ForceHaveCombinationProvider(_slotMachineConfig);
            var combinationMilestoneProvider = new CombinationMilestoneProvider(_slotMachineConfig);
            var randomRollProvider = new RandomRollProvider(_slotMachineConfig);
            var playerData = new SlotMachinePlayerData();
            var randomRollController = new RandomRollController(forceHaveCombinationProvider, combinationMilestoneProvider, randomRollProvider, playerData, _slotMachineConfig.SlotCombinationConfigs.Count);

            var combinationIdToSelectedRolls = new List<List<int>>();
            foreach (var slotCombinationConfig in _slotMachineConfig.SlotCombinationConfigs)
            {
                combinationIdToSelectedRolls.Add(new List<int>());
            }

            for (int i = 0; i < Constants.TotalPercentageWeight; i++)
            {
                var combinationIndex = randomRollController.GetNextCombinationIndex();
                combinationIdToSelectedRolls[combinationIndex].Add(i);
            }

            for (var combinationIndex = 0; combinationIndex < combinationIdToSelectedRolls.Count; combinationIndex++)
            {
                var intervalString = new StringBuilder();
                var rolledIndexesString = new StringBuilder();
                
                var intervalRanges = new List<IntervalInt>();
                var selectedRollIndexes = combinationIdToSelectedRolls[combinationIndex];
                foreach (var selectedRollIndex in selectedRollIndexes)
                {
                    var interval = combinationMilestoneProvider.GetCombinationMilestoneOfRollIndex(combinationIndex, selectedRollIndex);
                    System.Diagnostics.Debug.Assert(!intervalRanges.Contains(interval));
                    intervalRanges.Add(interval);
                    
                    intervalString.Append($"{interval.Min}-{interval.Max}\t\t");
                    rolledIndexesString.Append($"{selectedRollIndex}\t\t");
                }

                var slotCombinationConfig = _slotMachineConfig.SlotCombinationConfigs[combinationIndex];
                System.Diagnostics.Debug.Assert(intervalRanges.Count == slotCombinationConfig.Percentage);
                Debug.LogWarning($"Percentage:{slotCombinationConfig.Percentage}, CombIndex:{combinationIndex}, Items:{string.Join(",", slotCombinationConfig.SlotItems)}\n{intervalString.ToString()}\n{rolledIndexesString.ToString()}");
            }
        }
    }
}