using Fesb.Hydrocontest.Domain.Entities;
using Fesb.Hydrocontest.Domain.Mappers;
using Fesb.Hydrocontest.Domain.ValueObjects;

namespace Fesb.Hydrocontest.Domain.Commands
{
    public abstract class MotorCommand
    {
        public static Motor Execute(CreateDto rawMotorData)
        {
            return new Motor
            (
                controllerTemperature: new Temperature
                                       (
                                            value: rawMotorData.ControllerTemperature,
                                            unit:  TemperatureUnits.Celsius
                                        ),
                battery:                new Battery
                                        (
                                            voltage:     rawMotorData.BatteryVoltage / 57.45,
                                            capacity:    rawMotorData.BatteryCapacity,
                                            temperature: new Temperature
                                                         (
                                                             value: rawMotorData.BatteryTemperature,
                                                             unit:  TemperatureUnits.Celsius
                                                         )
                                        ),
                rotationsPerMinute:     rawMotorData.RotationsPerMinute * 10,
                warning:                Mapper.Warning.Map[rawMotorData.Warning],
                failure:                Mapper.Failure.Map[rawMotorData.Failure]
            );
        }

        public class CreateDto
        {
            public int ControllerTemperature { get; }
            public int BatteryVoltage        { get; }
            public int BatteryCapacity       { get; }
            public int BatteryTemperature    { get; }
            public int RotationsPerMinute    { get; }
            public int Warning               { get; }
            public int Failure               { get; }

            public CreateDto
            (
                int controllerTemperature,
                int batteryVoltage,
                int batteryCapacity,
                int batteryTemperature,
                int rotationsPerMinute,
                int warning,
                int failure
            )
            {
                ControllerTemperature = controllerTemperature;
                BatteryVoltage        = batteryVoltage;
                BatteryCapacity       = batteryCapacity;
                BatteryTemperature    = batteryTemperature;
                RotationsPerMinute    = rotationsPerMinute;
                Warning               = warning;
                Failure               = failure;
            }              
        }
    }
}
