namespace Common.Model
{
    public class IntervalInt
    {
        public int Min { get; }
        public int Max { get; }

        public IntervalInt(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}