using System.Collections.Generic;
using Common;
using Infrastructure.Save;
using SlotMachine.Random.Force;
using SlotMachine.Random.Milestone;
using SlotMachine.Random.Roll;

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

                // Remove already rolled combinations
                for (var j = forceHaveCombinationIndexes.Count - 1; j >= 0; j--)
                {
                    var combinationIndex = forceHaveCombinationIndexes[j];
                    if (IsRolledInCurrentMilestone(combinationIndex, i))
                    {
                        forceHaveCombinationIndexes.RemoveAt(j);
                    }
                }

                cumulativeForceHaveCombinationIndexes.AddRange(forceHaveCombinationIndexes);

                var numberOfRolls = i - _playerData.CurrentRollIndex;
                if (numberOfRolls <= cumulativeForceHaveCombinationIndexes.Count)
                {
                    // We have force roll one of the items in cumulativeForceHaveCombinationIndexes so we obey the distribution rule
                    break;
                }
            }

            var distinctValues = new HashSet<int>();
            for (var i = 0; i < cumulativeForceHaveCombinationIndexes.Count; i++)
            {
                var combinationIndex = cumulativeForceHaveCombinationIndexes[i];
                if (!IsRolledInCurrentMilestone(combinationIndex, nextRollIndex))
                {
                    distinctValues.Add(combinationIndex);
                }
            }
            
            var selectedCombinationIndex = _randomRollProvider.SelectRandomCombination(distinctValues.GetEnumerator());
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