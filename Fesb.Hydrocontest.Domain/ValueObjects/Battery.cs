namespace Fesb.Hydrocontest.Domain.ValueObjects
{
    public class Battery
    {
        public double      Voltage     { get; }
        public int         Capacity    { get; }
        public Temperature Temperature { get; }

        public Battery
        (
            double      voltage,
            int         capacity,
            Temperature temperature
        )
        {
            Voltage     = voltage;
            Capacity    = capacity;
            Temperature = temperature;
        }

        public static bool operator ==(Battery first, Battery second)
            => first.Voltage     == second.Voltage
            && first.Capacity    == second.Capacity
            && first.Temperature == second.Temperature;

        public static bool operator !=(Battery first, Battery second) => !(first == second);
    }
}
