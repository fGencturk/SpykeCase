#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using Common;
using Core.Data;
using Infrastructure.Save;
using Random.Force;
using Random.Milestone;
using Random.Roll;
using SlotMachine.Random;
using UnityEngine;

namespace Test
{
    public class TestRandomRoller : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private SlotMachineConfig _SlotMachineConfig;

        #endregion
        
        private RandomRollController _randomRollController;
        private SlotMachinePlayerData _playerData;

        public void RollUntil()
        {
            var forceHaveCombinationProvider = new ForceHaveCombinationProvider(_SlotMachineConfig);
            var combinationMilestoneProvider = new CombinationMilestoneProvider(_SlotMachineConfig);
            var randomRollProvider = new RandomRollProvider(_SlotMachineConfig);
            _playerData = new SlotMachinePlayerData();
            _randomRollController = new RandomRollController(forceHaveCombinationProvider, combinationMilestoneProvider, randomRollProvider, _playerData, _SlotMachineConfig.SlotCombinationConfigs.Count);

            var combinationIdToSelectedRolls = new List<List<int>>();
            foreach (var slotCombinationConfig in _SlotMachineConfig.SlotCombinationConfigs)
            {
                combinationIdToSelectedRolls.Add(new List<int>());
            }

            _playerData.Clear(_SlotMachineConfig.SlotCombinationConfigs.Count);
            for (int i = 0; i < Constants.TotalPercentageWeight; i++)
            {
                var combinationIndex = _randomRollController.GetNextCombinationIndex();
                combinationIdToSelectedRolls[combinationIndex].Add(i);
                Debug.Log($"Combination index {i}, last rolled in index: {combinationIndex}");
            }

            for (var combinationIndex = 0; combinationIndex < combinationIdToSelectedRolls.Count; combinationIndex++)
            {
                var intervalString = new StringBuilder();
                var rolledIndexesString = new StringBuilder();
                
                var selectedRollIndexes = combinationIdToSelectedRolls[combinationIndex];
                foreach (var selectedRollIndex in selectedRollIndexes)
                {
                    var interval = combinationMilestoneProvider.GetCombinationMilestoneOfRollIndex(combinationIndex, selectedRollIndex);
                    intervalString.Append($"{interval.Min}-{interval.Max}\t\t");
                    rolledIndexesString.Append($"{selectedRollIndex}\t\t");
                }

                var slotCombinationConfig = _SlotMachineConfig.SlotCombinationConfigs[combinationIndex];
                Debug.LogWarning($"Percentage:{slotCombinationConfig.Percentage}, CombIndex:{combinationIndex}, Items:{string.Join(",", slotCombinationConfig.SlotItems)}\n{intervalString.ToString()}\n{rolledIndexesString.ToString()}");
            }
        }
    }
}
#endif