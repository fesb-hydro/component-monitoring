using Fesb.Hydrocontest.Domain.ValueObjects;

namespace Fesb.Hydrocontest.Domain.Entities
{
    public class Boat
    {
        public Height Height    { get; }
        public double Pitch     { get; }
        public int    Roll      { get; }
        public double Speed     { get; }
        public int    FoilAngle { get; }

        public Boat
        (
            Height height,
            double pitch,
            int    roll,
            double speed,
            int    foilAngle
        )
        {
            Height    = height;
            Pitch     = pitch;
            Roll      = roll;
            Speed     = speed;
            FoilAngle = foilAngle;
        }

        public Statuses GetStatus() => Statuses.Ok;

        public static bool operator ==(Boat first, Boat second)
            => first.Height    == second.Height
            && first.Pitch     == second.Pitch
            && first.Roll      == second.Roll
            && first.Speed     == second.Speed
            && first.FoilAngle == second.FoilAngle;

        public static bool operator !=(Boat first, Boat second) => !(first == second);
    }
}
