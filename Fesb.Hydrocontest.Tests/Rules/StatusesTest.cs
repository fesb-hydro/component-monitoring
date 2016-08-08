using Fesb.Hydrocontest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fesb.Hydrocontest.Tests.Rules
{
    [TestClass]
    public class StatusesTest
    {
        private readonly Seeds _seeds;

        public StatusesTest()
        {
            _seeds = new Seeds();
        }

        [TestMethod]
        public void AreAllButMotorAlwaysReturningOk()
        {
            var radio = _seeds.GenerateRandomRadio();
            var boat = _seeds.GenerateRandomBoat();

            Assert.AreEqual(radio.GetStatus(), Statuses.Ok);
            Assert.AreEqual(boat.GetStatus(), Statuses.Ok);
        }

        [TestMethod]
        public void IsMotorReturningOkOnNoFailureAndWarning()
        {
            var motor = _seeds.GenerateRandomMotorWithSpecifiedWarningAndFailure(Warnings.None, Failures.None);

            Assert.AreEqual(motor.GetStatus(), Statuses.Ok);
        }

        [TestMethod]
        public void IsMotorReturningWarning()
        {
            var motor = _seeds.GenerateRandomMotorWithSpecifiedWarningAndFailure(Warnings.BatteryOverheated, Failures.None);

            Assert.AreEqual(motor.GetStatus(), Statuses.Warning);
        }

        [TestMethod]
        public void IsMotorReturningFailure()
        {
            var motor = _seeds.GenerateRandomMotorWithSpecifiedWarningAndFailure(Warnings.None, Failures.MotorError);

            Assert.AreEqual(motor.GetStatus(), Statuses.Failure);
        }

        [TestMethod]
        public void IsMotorPrioritizingFailureOverWarning()
        {
            var motor = _seeds.GenerateRandomMotorWithSpecifiedWarningAndFailure(Warnings.LowVoltage, Failures.InternalPowerSourceError);

            Assert.AreEqual(motor.GetStatus(), Statuses.Failure);
        }
    }
}
