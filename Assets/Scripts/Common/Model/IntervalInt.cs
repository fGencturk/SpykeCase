namespace Common.Model
{
    public struct IntervalInt
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