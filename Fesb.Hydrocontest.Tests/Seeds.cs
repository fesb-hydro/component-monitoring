using System;
using Fesb.Hydrocontest.Domain;
using Fesb.Hydrocontest.Domain.Commands;
using Fesb.Hydrocontest.Domain.Entities;
using Fesb.Hydrocontest.Domain.ValueObjects;

namespace Fesb.Hydrocontest.Tests
{
    public class Seeds
    {
        private readonly Random _randomGenerator = new Random();

        public RadioCommand.CreateDto GenerateRandomRadioDto()
        {
            return new RadioCommand.CreateDto
             (
                 mode:               _randomGenerator.Next(),
                 throttle:           _randomGenerator.Next(),
                 steer:              _randomGenerator.Next(),
                 potentiometerLeft:  _randomGenerator.Next(),
                 potentiometerRight: _randomGenerator.Next()
             );
        }

        public MotorCommand.CreateDto GenerateRandomMotorDtoWithSpecifiedWarningAndFailure(int warning, int failure)
        {
            return new MotorCommand.CreateDto
            (
                controllerTemperature: _randomGenerator.Next(),
                batteryVoltage:        _randomGenerator.Next(),
                batteryCapacity:       _randomGenerator.Next(),
                batteryTemperature:    _randomGenerator.Next(),
                rotationsPerMinute:    _randomGenerator.Next(),
                warning:               warning,
                failure:               failure
            );    
        }

        public BoatCommand.CreateDto GenerateRandomBoatDto()
        {
             return new BoatCommand.CreateDto
            (
                heightLowerPoint:  _randomGenerator.Next(),
                heightHigherPoint: _randomGenerator.Next(),
                roll:              _randomGenerator.Next(),
                speed:             _randomGenerator.Next(),
                foilAngle:         _randomGenerator.Next()
            );
        }

        public Radio GenerateRandomRadio()
        {
            return new Radio
            (
                mode:          _randomGenerator.Next(),
                throttle:      _randomGenerator.Next(),
                steer:         _randomGenerator.Next(),
                potentiometer: new Potentiometer
                               (
                                   left:  _randomGenerator.Next(),
                                   right: _randomGenerator.Next()
                               )
            );
        }

        public Motor GenerateRandomMotorWithSpecifiedWarningAndFailure(Warnings warning, Failures failure)
        {
            return new Motor
            (
                controllerTemperature: new Temperature
                                       (
                                            value: _randomGenerator.Next(), 
                                            unit:  TemperatureUnits.Celsius
                                        ),
                battery:                new Battery
                                        (
                                            voltage:     _randomGenerator.Next(),
                                            capacity:    _randomGenerator.Next(),
                                            temperature: new Temperature
                                                         (
                                                             value: _randomGenerator.Next(),
                                                             unit:  TemperatureUnits.Celsius
                                                         )
                                        ),
                rotationsPerMinute:    _randomGenerator.Next(),
                warning:               warning,
                failure:               failure
            );
        }

        public Boat GenerateRandomBoat()
        {
            return new Boat
            (
                height:    new Height
                           (
                               lowerPoint: _randomGenerator.Next(),
                               higherPoint: _randomGenerator.Next()
                           ),
                pitch:     _randomGenerator.Next(),
                roll:      _randomGenerator.Next(),
                speed:     _randomGenerator.Next(),
                foilAngle: _randomGenerator.Next()
            );
        }
    }
}
