using System;
using System.Linq;
using System.Collections.Generic;
using Fesb.Hydrocontest.Domain.Commands;
using Fesb.Hydrocontest.Domain.Entities;
using Fesb.Hydrocontest.Infrastructure.Parsers;
using Fesb.Hydrocontest.Infrastructure.Extensions;
using Fesb.Hydrocontest.Infrastructure.Helpers;

namespace Fesb.Hydrocontest.Infrastructure.Services
{
    public interface IInputQueueService
    {
        Result<QueueStatuses, OutputDto> Enqueue(InputDto input);
    }

    public class InputQueueService : IInputQueueService
    {
        private readonly List<InputDto>  _queue;
        private readonly Tuple<int, int> _radioIdsExtremeValues;
        private readonly Tuple<int, int> _motorIdsExtremeValues;
        private readonly Tuple<int, int> _boatIdsExtremeValues;

        public InputQueueService()
        {
            _queue = new List<InputDto>();
            _radioIdsExtremeValues = new Tuple<int, int>
            (
                Enum.GetValues(typeof(RadioIds)).Cast<int>().Min(),
                Enum.GetValues(typeof(RadioIds)).Cast<int>().Max()
            );
            _motorIdsExtremeValues = new Tuple<int, int>
            (
                Enum.GetValues(typeof(MotorIds)).Cast<int>().Min(),
                Enum.GetValues(typeof(MotorIds)).Cast<int>().Max()
            );
            _boatIdsExtremeValues  = new Tuple<int, int>
            (
                Enum.GetValues(typeof(BoatIds)).Cast<int>().Min(),
                Enum.GetValues(typeof(BoatIds)).Cast<int>().Max()
            );
        }

        public Result<QueueStatuses, OutputDto> Enqueue(InputDto input)
        {
            var existentEnqueuedElementWithSameId = _queue
                .SingleOrDefault(x => x.Id == input.Id);

            if (existentEnqueuedElementWithSameId.IsNotNull())
            {
                _queue.Remove(existentEnqueuedElementWithSameId);
            }

            _queue.Add(input);
            return this.Digest(input.Id);
        }

        private Result<QueueStatuses, OutputDto> Digest(int id)
        {
            if (id.IsWithinRange(_radioIdsExtremeValues.Minimum(), _radioIdsExtremeValues.Maximum()))
            {
                return Digest(typeof(Radio));
            }

            if (id.IsWithinRange(_motorIdsExtremeValues.Minimum(), _motorIdsExtremeValues.Maximum()))
            {
                return Digest(typeof(Motor));
            }

            if (id.IsWithinRange(_boatIdsExtremeValues.Minimum(), _boatIdsExtremeValues.Maximum()))
            {
                return Digest(typeof(Boat));
            }

            return new Result<QueueStatuses, OutputDto>
            (
                status: QueueStatuses.NoChanges
            );
        }

        private Result<QueueStatuses, OutputDto> Digest(Type type)
        {
            var map = new Dictionary<Type, Func<Result<QueueStatuses, OutputDto>>>()
            {
                { typeof(Radio), ProcessRadio },
                { typeof(Motor), ProcessMotor },
                { typeof(Boat),  ProcessBoat  }
            };

            return map[type]();
        }

        private Result<QueueStatuses, OutputDto> ProcessRadio()
        {
            var radioData =
                _queue
                .Where(x => x.Id.IsWithinRange(_radioIdsExtremeValues.Minimum(), _radioIdsExtremeValues.Maximum()))
                .ToList();
            if (radioData.Count() == (_radioIdsExtremeValues.Maximum() - _radioIdsExtremeValues.Minimum() + 1))
            {
                var radio = RadioCommand.Execute(RadioParser.Parse(radioData));
                return new Result<QueueStatuses, OutputDto>
                (
                    status: QueueStatuses.ChangesToRadio,
                    output: new OutputDto(radio: radio)
                );
            }

            return new Result<QueueStatuses, OutputDto>
            (
                status: QueueStatuses.NoChanges
            );
        }

        private Result<QueueStatuses, OutputDto> ProcessMotor()
        {
            var motorData =
                _queue
                .Where(x => x.Id.IsWithinRange(_motorIdsExtremeValues.Minimum(), _motorIdsExtremeValues.Maximum()))
                .ToList();
            if (motorData.Count() == (_motorIdsExtremeValues.Maximum() - _motorIdsExtremeValues.Minimum() + 1))
            {
                var motor = MotorCommand.Execute(MotorParser.Parse(motorData));
                return new Result<QueueStatuses, OutputDto>
                (
                    status: QueueStatuses.ChangesToMotor,
                    output: new OutputDto(motor: motor)
                );
            }

            return new Result<QueueStatuses, OutputDto>
            (
                status: QueueStatuses.NoChanges
            );
        }

        private Result<QueueStatuses, OutputDto> ProcessBoat()
        {
            var boatData =
                _queue
                .Where(x => x.Id.IsWithinRange(_boatIdsExtremeValues.Minimum(), _boatIdsExtremeValues.Maximum()))
                .ToList();
            if (boatData.Count() == (_boatIdsExtremeValues.Maximum() - _boatIdsExtremeValues.Minimum() + 1))
            {
                var boat = BoatCommand.Execute(BoatParser.Parse(boatData));
                return new Result<QueueStatuses, OutputDto>
                (
                    status: QueueStatuses.ChangesToBoat,
                    output: new OutputDto(boat: boat)
                );
            }

            return new Result<QueueStatuses, OutputDto>
            (
                status: QueueStatuses.NoChanges
            );
        }      
    }

    public enum QueueStatuses
    {
        NoChanges,
        ChangesToRadio,
        ChangesToMotor,
        ChangesToBoat
    }

    public class InputDto
    {
        public int Id    { get; set; }
        public int Value { get; set; }
    }

    public class OutputDto
    {
        public Radio Radio { get; }
        public Motor Motor { get; }
        public Boat Boat   { get; }

        public OutputDto
        (
            Radio radio = null,
            Motor motor = null,
            Boat boat   = null
        )
        {
            Radio = radio;
            Motor = motor;
            Boat = boat;
        }
    }
}
