namespace Fesb.Hydrocontest.Infrastructure.Extensions
{
    public static class IntExtension
    {
        public static bool IsWithinRange(this int value, int minimum, int maximum)
            => value >= minimum && value <= maximum;
    }
}
