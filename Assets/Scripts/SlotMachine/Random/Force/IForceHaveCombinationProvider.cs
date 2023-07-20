using System.Collections.Generic;

namespace SlotMachine.Random.Force
{
    public interface IForceHaveCombinationProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentRollIndex"></param>
        /// <param name="forceHaveCombinationList"></param>
        /// <returns>Combination indexes to be had already rolled after their checkpoint till currentRollIndex</returns>
        public bool TryGetForceHaveCombinationsList(int currentRollIndex, out IList<int> forceHaveCombinationList);
    }
}