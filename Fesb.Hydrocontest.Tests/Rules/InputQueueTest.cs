using Fesb.Hydrocontest.Infrastructure.Parsers;
using Fesb.Hydrocontest.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fesb.Hydrocontest.Tests.Rules
{
    [TestClass]
    public class InputQueueTest
    {
        [TestMethod]
        public void IsWaitingForAllValuesBeforeBundling()
        {
            var inputQueueService = new InputQueueService();
            var response = inputQueueService.Enqueue(new InputDto {Id = (int)RadioIds.Mode, Value = 1});

            Assert.AreEqual(response.Status, QueueStatuses.NoChanges);
        }

        [TestMethod]
        public void IsReturningAllNullsBeforeAnyBundling()
        {
            var inputQueueService = new InputQueueService();
            var response = inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.Mode, Value = 1 });

            Assert.AreEqual(response.Output, null);
        }

        [TestMethod]
        public void IsUpdatingNewComingValuesBeforeBundlingPointComes()
        {
            var inputQueueService = new InputQueueService();

            inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.Mode,              Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.Mode,              Value = 2 });
            inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.Throttle,          Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.Steer,             Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.PotentiometerLeft, Value = 1 });

            var response = inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.PotentiometerRight, Value = 1 });

            Assert.AreEqual(response.Status, QueueStatuses.ChangesToRadio);
            Assert.AreEqual(response.Output.Radio.Mode, 2);
        }

        [TestMethod]
        public void IsRadioBundling()
        {
            var inputQueueService = new InputQueueService();

            inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.Mode,              Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.Throttle,          Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.Steer,             Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.PotentiometerLeft, Value = 1 });

            var response = inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.PotentiometerRight, Value = 1 });

            Assert.AreEqual(response.Status, QueueStatuses.ChangesToRadio);
            Assert.AreNotEqual(response.Output.Radio, null);
        }

        [TestMethod]
        public void IsMotorBundling()
        {
            var inputQueueService = new InputQueueService();

            inputQueueService.Enqueue(new InputDto { Id = (int)MotorIds.ControllerTemperature, Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)MotorIds.BatteryVoltage,        Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)MotorIds.BatteryCapacity,       Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)MotorIds.BatteryTemperature,    Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)MotorIds.RotationsPerMinute,    Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)MotorIds.Warnings,              Value = 1 });

            var response = inputQueueService.Enqueue(new InputDto { Id = (int)MotorIds.Failures, Value = 1 });

            Assert.AreEqual(response.Status, QueueStatuses.ChangesToMotor);
            Assert.AreNotEqual(response.Output.Motor, null);
        }

        [TestMethod]
        public void IsBoatBundling()
        {
            var inputQueueService = new InputQueueService();

            inputQueueService.Enqueue(new InputDto { Id = (int)BoatIds.HeightLowerPoint,  Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)BoatIds.HeightHigherPoint, Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)BoatIds.Roll,              Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)BoatIds.Speed,             Value = 1 });

            var response = inputQueueService.Enqueue(new InputDto { Id = (int)BoatIds.FoilAngle, Value = 1 });

            Assert.AreEqual(response.Status, QueueStatuses.ChangesToBoat);
            Assert.AreNotEqual(response.Output.Boat, null);
        }

        [TestMethod]
        public void CanHandleMultipleUnfinishedBundles()
        {
            var inputQueueService = new InputQueueService();

            inputQueueService.Enqueue(new InputDto { Id = (int)BoatIds.HeightLowerPoint,  Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)BoatIds.HeightHigherPoint, Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)BoatIds.Roll,              Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)MotorIds.Failures,         Value = 1 });
            inputQueueService.Enqueue(new InputDto { Id = (int)MotorIds.Warnings,         Value = 1 });

            var response = inputQueueService.Enqueue(new InputDto { Id = (int)RadioIds.Steer, Value = 1 });

            Assert.AreEqual(response.Status, QueueStatuses.NoChanges);
            Assert.AreEqual(response.Output, null);
        }
    }
}
