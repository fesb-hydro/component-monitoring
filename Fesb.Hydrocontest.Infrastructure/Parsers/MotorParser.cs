using System.Collections.Generic;
using System.Linq;
using Fesb.Hydrocontest.Domain.Commands;
using Fesb.Hydrocontest.Infrastructure.Services;

namespace Fesb.Hydrocontest.Infrastructure.Parsers
{
    public enum MotorIds
    {
        ControllerTemperature = 6,
        BatteryVoltage        = 7,
        BatteryCapacity       = 8,
        BatteryTemperature    = 9,
        RotationsPerMinute    = 10,
        Warnings              = 11,
        Failures              = 12
    }

    public abstract class MotorParser
    {
        public static MotorCommand.CreateDto Parse(IList<InputDto> motorData)
        {
            return new MotorCommand.CreateDto
            (
                controllerTemperature: motorData.Single(x => x.Id == (int)MotorIds.ControllerTemperature).Value,
                batteryVoltage:        motorData.Single(x => x.Id == (int)MotorIds.BatteryVoltage       ).Value,
                batteryCapacity:       motorData.Single(x => x.Id == (int)MotorIds.BatteryCapacity      ).Value,
                batteryTemperature:    motorData.Single(x => x.Id == (int)MotorIds.BatteryTemperature   ).Value,
                rotationsPerMinute:    motorData.Single(x => x.Id == (int)MotorIds.RotationsPerMinute   ).Value,
                warning:               motorData.Single(x => x.Id == (int)MotorIds.Warnings             ).Value,
                failure:               motorData.Single(x => x.Id == (int)MotorIds.Failures             ).Value
            );
        }
    }
}
