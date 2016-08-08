using Fesb.Hydrocontest.Domain.ValueObjects;

namespace Fesb.Hydrocontest.Domain.Entities
{
    public class Motor
    {
        public Temperature ControllerTemperature { get; }
        public Battery     Battery               { get; }
        public int         RotationsPerMinute    { get; }
        public Warnings    Warning               { get; }
        public Failures    Failure               { get; }

        public Motor
        (
            Temperature controllerTemperature,
            Battery     battery,
            int         rotationsPerMinute,
            Warnings    warning,
            Failures    failure
        )
        {
            ControllerTemperature = controllerTemperature;
            Battery               = battery;
            RotationsPerMinute    = rotationsPerMinute;
            Warning               = warning;
            Failure               = failure;
        }

        public Statuses GetStatus()
        {
            if (Failure != Failures.None)
                return Statuses.Failure;
            if (Warning != Warnings.None)
                return Statuses.Warning;
            return Statuses.Ok;
        }

        public static bool operator ==(Motor first, Motor second)
            => first.ControllerTemperature == second.ControllerTemperature
            && first.Battery               == second.Battery
            && first.RotationsPerMinute    == second.RotationsPerMinute
            && first.Warning               == second.Warning
            && first.Failure               == second.Failure;

        public static bool operator !=(Motor first, Motor second) => !(first == second);
    }
}
