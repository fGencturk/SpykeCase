using Common.Model;
using UnityEngine;

namespace Common
{
    public static class Utilities
    {
        public static IntervalInt GetInterval(int percentage, int intervalIndex)
        {
            var stepSize = Constants.TotalPercentageWeight / (float)percentage;
            return new IntervalInt((int)(intervalIndex * stepSize),
                Mathf.Min(Constants.TotalPercentageWeight, (int)(stepSize * (intervalIndex + 1))) - 1);
        }
    }
}