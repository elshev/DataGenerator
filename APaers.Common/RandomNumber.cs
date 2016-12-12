using System;
using System.Threading;

namespace APaers.Common
{
    public static class StaticRandom
    {
        private static int seed;
        private static readonly ThreadLocal<Random> threadLocal = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance => threadLocal.Value;

        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
                return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
