using System.Collections.Generic;

namespace Random.Roll
{
    public interface IRandomRollProvider
    {
        public int SelectRandomCombination(IList<int> availableCombinationIndexes);
    }
}