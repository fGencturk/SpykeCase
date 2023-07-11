using System;
using System.Collections.Generic;
using Core.Data;
using Infrastructure.Save;
using Random;
using Random.Force;
using Random.Milestone;
using Random.Roll;
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

            _selecteds = new Dictionary<int, int>();
            for (int i = 0; i < 10; i++)
            {
                _selecteds[i] = 0;
            }

            for (int i = 0; i < 100; i++)
            {
                var combinationIndex = _randomRollController.GetNextCombinationIndex();
                _selecteds[combinationIndex]++;
                Debug.LogWarning($"{combinationIndex} SELECTED for step {_playerData.CurrentRollIndex}");
                Debug.LogWarning("------------------------");
            }
            
            Debug.LogWarning("FINISHED");
        }

        private Dictionary<int, int> _selecteds;

    }
}