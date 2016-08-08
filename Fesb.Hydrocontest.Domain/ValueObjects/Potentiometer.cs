namespace Fesb.Hydrocontest.Domain.ValueObjects
{
    public class Potentiometer
    {
        public double Left  { get; }
        public double Right { get; }

        public Potentiometer
        (
            double left,
            double right
        )
        {
            Left  = left;
            Right = right;
        }

        public static bool operator ==(Potentiometer first, Potentiometer second)
            => first.Left  == second.Left
            && first.Right == second.Right;

        public static bool operator !=(Potentiometer first, Potentiometer second) => !(first == second);
    }
}
