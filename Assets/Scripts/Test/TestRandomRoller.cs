using System;
using System.Collections.Generic;
using System.Text;
using Core.Data;
using Infrastructure.Save;
using Random;
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

        private void Awake()
        {
            PlayerPrefs.DeleteAll();
            var forceHaveCombinationProvider = new ForceHaveCombinationProvider(_SlotMachineConfig);
            var combinationMilestoneProvider = new CombinationMilestoneProvider(_SlotMachineConfig);
            var randomRollProvider = new RandomRollProvider(_SlotMachineConfig);
            _playerData = new SlotMachinePlayerData();
            _randomRollController = new RandomRollController(forceHaveCombinationProvider, combinationMilestoneProvider, randomRollProvider, _playerData);

            var combinationIdToSelectedRolls = new List<List<int>>();
            foreach (var slotCombinationConfig in _SlotMachineConfig.SlotCombinationConfigs)
            {
                combinationIdToSelectedRolls.Add(new List<int>());
            }

            for (int i = 0; i < 100; i++)
            {
                var combinationIndex = _randomRollController.GetNextCombinationIndex();
                combinationIdToSelectedRolls[combinationIndex].Add(i);
                Debug.LogWarning($"{combinationIndex} SELECTED for step {_playerData.CurrentRollIndex}");
            }

            for (var combinationIndex = 0; combinationIndex < combinationIdToSelectedRolls.Count; combinationIndex++)
            {
                var intervalString = new StringBuilder();
                var rolledIndexesString = new StringBuilder();
                
                var selectedRollIndexes = combinationIdToSelectedRolls[combinationIndex];
                foreach (var selectedRollIndex in selectedRollIndexes)
                {
                    var interval = combinationMilestoneProvider.GetCombinationMilestone(combinationIndex, selectedRollIndex);
                    intervalString.Append($"{interval.Min}-{interval.Max}\t\t");
                    rolledIndexesString.Append($"{selectedRollIndex}\t\t");
                }

                var slotCombinationConfig = _SlotMachineConfig.SlotCombinationConfigs[combinationIndex];
                Debug.LogWarning($"Percentage:{slotCombinationConfig.Percentage}, CombIndex:{combinationIndex}, Items:{string.Join(",", slotCombinationConfig.SlotItems)}\n{intervalString.ToString()}\n{rolledIndexesString.ToString()}");
            }
        }
    }
}