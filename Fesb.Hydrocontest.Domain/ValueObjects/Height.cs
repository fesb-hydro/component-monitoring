namespace Fesb.Hydrocontest.Domain.ValueObjects
{
    public class Height
    {
        public double LowerPoint  { get; }
        public double HigherPoint { get; }

        public Height
        (
            double lowerPoint,
            double higherPoint
        )
        {
            LowerPoint  = lowerPoint;
            HigherPoint = higherPoint;
        }

        public static bool operator ==(Height first, Height second)
            => first.LowerPoint  == second.LowerPoint
            && first.HigherPoint == second.HigherPoint;

        public static bool operator !=(Height first, Height second) => !(first == second);
    }
}
