namespace Fesb.Hydrocontest.Domain.ValueObjects
{
    public enum TemperatureUnits
    {
        Kelvin,
        Celsius,
        Fahrenheit
    }

    public class Temperature
    {
        public int              Value { get; }
        public TemperatureUnits Unit  { get; }

        public Temperature
        (
            int              value,
            TemperatureUnits unit
        )
        {
            Value = value;
            Unit  = unit;
        }

        public static bool operator ==(Temperature first, Temperature second)
            => first.Value == second.Value
            && first.Unit  == second.Unit;

        public static bool operator !=(Temperature first, Temperature second) => !(first == second);
    }
}
