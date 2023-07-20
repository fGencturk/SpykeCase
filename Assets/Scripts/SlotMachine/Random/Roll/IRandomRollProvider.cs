using System.Collections.Generic;

namespace SlotMachine.Random.Roll
{
    public interface IRandomRollProvider
    {
        public int SelectRandomCombination(IEnumerator<int> availableCombinationIndexes);
    }
}