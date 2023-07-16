using Core.Data;
using Infrastructure.Event;
using Infrastructure.Save;
using Random.Force;
using Random.Milestone;
using Random.Roll;
using SlotMachine.Event;
using SlotMachine.Random;
using UI.Event;
using UnityEngine;

namespace SlotMachine
{
    public class SlotMachineController : MonoBehaviour
    {

        #region Inspector
        
        [SerializeField] private SlotMachineConfig _SlotMachineConfig;

        #endregion
        
        private SlotMachinePlayerData _playerData;
        private RandomRollController _randomRollController;

        #region Unity event functions

        private void Awake()
        {
            var forceHaveCombinationProvider = new ForceHaveCombinationProvider(_SlotMachineConfig);
            var combinationMilestoneProvider = new CombinationMilestoneProvider(_SlotMachineConfig);
            var randomRollProvider = new RandomRollProvider(_SlotMachineConfig);
            _playerData = new SlotMachinePlayerData();
            _randomRollController = new RandomRollController(forceHaveCombinationProvider, combinationMilestoneProvider, randomRollProvider, _playerData, _SlotMachineConfig.SlotCombinationConfigs.Count);
            
            Debug.Log($"Current roll index: {_playerData.CurrentRollIndex}");
            for (var i = 0; i < _SlotMachineConfig.SlotCombinationConfigs.Count; i++)
            {
                Debug.Log($"Combination index {i}, last rolled in index: {_playerData.GetLastHitIndex(i)}");
            }
            
        }

        private void OnEnable()
        {
            EventManager.Register<SlotMachineRollRequestedEvent>(OnRollRequested);
        }

        private void OnDisable()
        {
            EventManager.Unregister<SlotMachineRollRequestedEvent>(OnRollRequested);
        }

        #endregion

        #region Handlers
        
        private void OnRollRequested(SlotMachineRollRequestedEvent data)
        {
            var combinationIndex = _randomRollController.GetNextCombinationIndex();
            var slotCombinationConfig = _SlotMachineConfig.SlotCombinationConfigs[combinationIndex];
            Debug.Log($"RollIndex:{_playerData.CurrentRollIndex}, Selected CombinationIndex:{combinationIndex} [{string.Join(",", slotCombinationConfig.SlotItems)}]");
            EventManager.Send(SlotMachineRolledEvent.Create(slotCombinationConfig));
        }

        #endregion
        
        
        
    }
}