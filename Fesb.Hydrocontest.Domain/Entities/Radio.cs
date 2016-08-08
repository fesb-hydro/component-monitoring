using Fesb.Hydrocontest.Domain.ValueObjects;

namespace Fesb.Hydrocontest.Domain.Entities
{
    public class Radio
    {
        public int           Mode          { get; }
        public double        Throttle      { get; }
        public int           Steer         { get; }
        public Potentiometer Potentiometer { get; }

        public Radio
        (
            int           mode,
            double        throttle,
            int           steer,
            Potentiometer potentiometer
        )
        { 
            Mode          = mode;
            Throttle      = throttle;
            Steer         = steer;
            Potentiometer = potentiometer;
        }

        public Statuses GetStatus() => Statuses.Ok;

        public static bool operator ==(Radio first, Radio second)
            => first.Mode          == second.Mode
            && first.Throttle      == second.Throttle
            && first.Steer         == second.Steer
            && first.Potentiometer == second.Potentiometer;

        public static bool operator !=(Radio first, Radio second) => !(first == second);
    }
}
