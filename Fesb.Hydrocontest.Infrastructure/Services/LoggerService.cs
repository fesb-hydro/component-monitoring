using System;
using System.IO;
using System.Reflection;
using System.Text;
using Fesb.Hydrocontest.Domain;
using Fesb.Hydrocontest.Domain.Entities;
using Fesb.Hydrocontest.Infrastructure.Extensions;

namespace Fesb.Hydrocontest.Infrastructure.Services
{
    public interface ILoggerService
    {
        void LogToCsvFile(Radio radio, Motor motor, Boat boat);
    }

    public class LoggerService : ILoggerService
    {
        private readonly IMementoService _mementoService;

        public LoggerService()
        {
            _mementoService = new MementoService();
            if (!File.Exists(GetFilePath())) LogHeaderToCsvFile();
        }

        public void LogToCsvFile(Radio radio, Motor motor, Boat boat)
        {
            if (radio.IsNull() || motor.IsNull() || boat.IsNull()) return;
            if (_mementoService.AreAllStatesUnchanged(radio, motor, boat)) return;

            using (var streamWriter = File.AppendText(GetFilePath()))
            {
                var logBuilder = new StringBuilder();
                var timestamp = DateTime.Now;

                logBuilder
                    .Append($"{timestamp.ToString("yyyy-MM-dd HH:mm:ss.fffffff")};")
                    .Append($"{SerializeRadio(radio)};")
                    .Append($"{SerializeMotor(motor)};")
                    .Append($"{SerializeBoat(boat)};");

                streamWriter.WriteLine(logBuilder.ToString());
            }
        }

        private string SerializeRadio(Radio radio)
        {
            return string.Join(";", 
                Enum.GetName(typeof(Statuses), radio.GetStatus()),
                radio.Mode,
                radio.Throttle,
                radio.Steer,
                radio.Potentiometer.Left,
                radio.Potentiometer.Right
            );
        }

        private string SerializeMotor(Motor motor)
        {
            return string.Join(";", 
                Enum.GetName(typeof(Statuses), motor.GetStatus()), 
                motor.ControllerTemperature.Value, 
                motor.Battery.Temperature.Value, 
                motor.Battery.Capacity, 
                motor.Battery.Voltage, 
                motor.RotationsPerMinute, 
                Enum.GetName(typeof(Warnings), motor.Warning), 
                Enum.GetName(typeof(Failures), motor.Failure)
            );
        }

        private string SerializeBoat(Boat boat)
        {
            return string.Join(";",
                Enum.GetName(typeof(Statuses), boat.GetStatus()),
                boat.Height.LowerPoint,
                boat.Height.HigherPoint,
                boat.Roll,
                boat.Speed,
                boat.FoilAngle
            );
        }

        private void LogHeaderToCsvFile()
        {
            using (var streamWriter = File.AppendText(GetFilePath()))
            {
                var logBuilder = new StringBuilder();

                logBuilder
                    .AppendLine(";Radio;;;;;;Motor;;;;;;Boat;;;;;;")
                    .Append("Timestamp;Status;Mode;Throttle;Steer;Potentiometer left;Potentiometer right;")
                    .Append("Status;Controller temperature;Battery temperature;Battery capacity;Battery voltage;RPM;Warning;Failure;")
                    .Append("Status;Height lower point;Height higher point;Roll;Speed;Foil angle;");

                streamWriter.WriteLine(logBuilder.ToString());
            }
        }

        private string GetFilePath()
        {
            var logFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) 
                                ?? @"C:\";
            return Path.Combine(logFolderPath, "Log.csv");
        }
    }
}
