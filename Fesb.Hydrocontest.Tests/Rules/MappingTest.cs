using Fesb.Hydrocontest.Domain;
using Fesb.Hydrocontest.Domain.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fesb.Hydrocontest.Tests.Rules
{
    [TestClass]
    public class MappingTest
    {
        private readonly Seeds _seeds;

        public MappingTest()
        {
            _seeds = new Seeds();
        }

        [TestMethod]
        public void IsMappingOnNoWarningAndFailure()
        {
            var motorDto = _seeds.GenerateRandomMotorDtoWithSpecifiedWarningAndFailure(0, 0);
            var motor = MotorCommand.Execute(motorDto);

            Assert.AreEqual(motor.Warning, Warnings.None);
            Assert.AreEqual(motor.Failure, Failures.None);
        }

        [TestMethod]
        public void IsMappingWarning()
        {
            var motorDto = _seeds.GenerateRandomMotorDtoWithSpecifiedWarningAndFailure(2, 0);
            var motor = MotorCommand.Execute(motorDto);

            Assert.AreEqual(motor.Warning, Warnings.HighCurrent);
        }

        [TestMethod]
        public void IsMappingFailure()
        {
            var motorDto = _seeds.GenerateRandomMotorDtoWithSpecifiedWarningAndFailure(0, 128);
            var motor = MotorCommand.Execute(motorDto);

            Assert.AreEqual(motor.Failure, Failures.BatteryTemperatureSensor);
        }

        [TestMethod]
        public void IsMappingOtherFailure()
        {
            var motorDto = _seeds.GenerateRandomMotorDtoWithSpecifiedWarningAndFailure(0, 32768);
            var motor = MotorCommand.Execute(motorDto);

            Assert.AreEqual(motor.Failure, Failures.Other);
        }
    }
}
