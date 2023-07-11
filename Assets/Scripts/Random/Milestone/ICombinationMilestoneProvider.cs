using Common.Model;

namespace Random.Milestone
{
    public interface ICombinationMilestoneProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="combinationIndex"></param>
        /// <param name="currentRollIndex"></param>
        /// <returns>Range in which combination index must be rolled once, inclusive.</returns>
        public IntervalInt GetCombinationMilestone(int combinationIndex, int currentRollIndex);
    }
}