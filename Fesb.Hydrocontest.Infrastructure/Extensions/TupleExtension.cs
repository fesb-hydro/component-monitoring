using System;

namespace Fesb.Hydrocontest.Infrastructure.Extensions
{
    public static class TupleExtension
    {
        public static int Minimum(this Tuple<int, int> tuple) => tuple.Item1;
        public static int Maximum(this Tuple<int, int> tuple) => tuple.Item2;
    }
}
