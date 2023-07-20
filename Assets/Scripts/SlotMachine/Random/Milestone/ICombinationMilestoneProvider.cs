using Common.Model;

namespace SlotMachine.Random.Milestone
{
    public interface ICombinationMilestoneProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="combinationIndex"></param>
        /// <param name="currentRollIndex"></param>
        /// <returns>Range in which combination index must be rolled once, inclusive.</returns>
        IntervalInt GetCombinationMilestoneOfRollIndex(int combinationIndex, int currentRollIndex);

        IntervalInt GetCombinationMilestoneOfIntervalIndex(int combinationIndex, int intervalIndex);
    }
}