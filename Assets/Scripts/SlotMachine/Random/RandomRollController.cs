using System.Collections.Generic;
using System.Linq;
using Common;
using Infrastructure.Save;
using Random.Force;
using Random.Milestone;
using Random.Roll;
using UnityEngine;

namespace SlotMachine.Random
{
    public class RandomRollController
    {
        private readonly IForceHaveCombinationProvider _forceHaveCombinationProvider;
        private readonly ICombinationMilestoneProvider _combinationMilestoneProvider;
        private readonly IRandomRollProvider _randomRollProvider;
        private readonly IPlayerData _playerData;
        private readonly int _combinationCount;

        public RandomRollController(IForceHaveCombinationProvider forceHaveCombinationProvider, ICombinationMilestoneProvider combinationMilestoneProvider, IRandomRollProvider randomRollProvider, IPlayerData playerData, int combinationCount)
        {
            _forceHaveCombinationProvider = forceHaveCombinationProvider;
            _combinationMilestoneProvider = combinationMilestoneProvider;
            _randomRollProvider = randomRollProvider;
            _playerData = playerData;
            _combinationCount = combinationCount;
        }

        public int GetNextCombinationIndex()
        {
            var nextRollIndex = _playerData.CurrentRollIndex + 1;

            if (nextRollIndex == Constants.TotalPercentageWeight)
            {
                _playerData.Clear(_combinationCount);
                nextRollIndex = _playerData.CurrentRollIndex + 1;
            }
            
            var cumulativeForceHaveCombinationIndexes = new List<int>();
            for (var i = nextRollIndex; i < Constants.TotalPercentageWeight; i++)
            {
                if (!_forceHaveCombinationProvider.TryGetForceHaveCombinationsList(i, out var forceHaveCombinationIndexes))
                {
                    continue;
                }

                var unRolledForceHaveCombinationIndexes = forceHaveCombinationIndexes.Where(combinationIndex => !IsRolledInCurrentMilestone(combinationIndex, i));
                cumulativeForceHaveCombinationIndexes.AddRange(unRolledForceHaveCombinationIndexes);

                var numberOfRolls = i - _playerData.CurrentRollIndex;
                if (numberOfRolls <= cumulativeForceHaveCombinationIndexes.Count)
                {
                    // We have force roll one of the items in cumulativeForceHaveCombinationIndexes so we obey the distribution rule
                    break;
                }
            }

            var distinctValues = cumulativeForceHaveCombinationIndexes.Distinct().Where(combinationIndex =>
                !IsRolledInCurrentMilestone(combinationIndex, nextRollIndex)).ToList();
            
            Debug.Log($"[{string.Join(",", distinctValues.OrderBy(index => index))}] - Random item is selection limited by these combination indexes");
            
            var selectedCombinationIndex = _randomRollProvider.SelectRandomCombination(distinctValues);
            _playerData.CurrentRollIndex = nextRollIndex;
            _playerData.SetLastHitIndex(selectedCombinationIndex, nextRollIndex);
            return selectedCombinationIndex;
        }

        private bool IsRolledInCurrentMilestone(int combinationIndex, int currentRollIndex)
        {
            return _playerData.GetLastHitIndex(combinationIndex) >= _combinationMilestoneProvider
                .GetCombinationMilestoneOfRollIndex(combinationIndex, currentRollIndex).Min;
        }
        
        
    }
}