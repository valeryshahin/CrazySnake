using System;
using System.Threading;

namespace CrazySnake
{
    public static class RandomProvider
    {
        private static int seed = Environment.TickCount;

        private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref seed)));

        public static Direction RandomDirect()
        {
            return (Direction)randomWrapper.Value.Next(0, 3);
        }
    }
}
