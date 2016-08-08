using System;

namespace Fesb.Hydrocontest.Infrastructure.Helpers
{
    public class Result<TS, TO>
        where TO : class
        where TS : struct, IComparable, IConvertible, IFormattable
    {
        public TO Output { get; }
        public TS Status { get; }

        public Result
        (
            TS status,
            TO output = null
        )
        {
            Status = status;
            Output = output;
        }
    }
}
