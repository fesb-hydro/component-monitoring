using System;
using Fesb.Hydrocontest.Domain;
using Fesb.Hydrocontest.Domain.Commands;
using Fesb.Hydrocontest.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fesb.Hydrocontest.Tests.Rules
{
    [TestClass]
    public class ConversionTest
    {
        private readonly Seeds _seeds;

        public ConversionTest()
        {
            _seeds = new Seeds();
        }

        [TestMethod]
        public void IsRadioConverting()
        {
            var radioDto = _seeds.GenerateRandomRadioDto();
            var radio = RadioCommand.Execute(radioDto);

            Assert.AreEqual(radio.Throttle, radioDto.Throttle / 2.54);
            Assert.AreEqual(radio.Steer, (radioDto.Steer - 127) * 30);
            Assert.AreEqual(radio.Potentiometer.Left, radioDto.PotentiometerLeft / 2.54);
            Assert.AreEqual(radio.Potentiometer.Right, radioDto.PotentiometerRight / 2.54);
        }

        [TestMethod]
        public void IsMotorConverting()
        {
            var motorDto = _seeds.GenerateRandomMotorDtoWithSpecifiedWarningAndFailure(0, 0);
            var motor = MotorCommand.Execute(motorDto);

            Assert.AreEqual(motor.ControllerTemperature.Value, motorDto.ControllerTemperature);
            Assert.AreEqual(motor.ControllerTemperature.Unit, TemperatureUnits.Celsius);
            Assert.AreEqual(motor.Battery.Voltage, motorDto.BatteryVoltage / 57.45);
            Assert.AreEqual(motor.Battery.Temperature.Value, motorDto.BatteryTemperature);
            Assert.AreEqual(motor.Battery.Temperature.Unit, TemperatureUnits.Celsius);
            Assert.AreEqual(motor.Battery.Capacity, motorDto.BatteryCapacity);
            Assert.AreEqual(motor.RotationsPerMinute, motorDto.RotationsPerMinute * 10);
            Assert.AreEqual(motor.Warning, Warnings.None);
            Assert.AreEqual(motor.Failure, Failures.None);
        }

        [TestMethod]
        public void IsBoatConverting()
        {
            var boatDto = _seeds.GenerateRandomBoatDto();
            var boat = BoatCommand.Execute(boatDto);

            Assert.AreEqual(boat.Height.LowerPoint, boatDto.HeightLowerPoint * 0.0859536);
            Assert.AreEqual(boat.Height.HigherPoint, boatDto.HeightHigherPoint * 0.0859536);
            Assert.AreEqual(boat.Pitch, Math.Asin((boatDto.HeightHigherPoint - boatDto.HeightLowerPoint) * 0.0859536 / 1200.0) * (180.0 / Math.PI));
            Assert.AreEqual(boat.Roll, boatDto.Roll - 127);
            Assert.AreEqual(boat.Speed, Math.Sqrt(2.0 / 1024.0 * boatDto.Speed));
            Assert.AreEqual(boat.FoilAngle, boatDto.FoilAngle - 127);
        }
    }
}
