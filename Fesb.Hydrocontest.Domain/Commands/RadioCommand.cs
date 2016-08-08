using Fesb.Hydrocontest.Domain.Entities;
using Fesb.Hydrocontest.Domain.ValueObjects;

namespace Fesb.Hydrocontest.Domain.Commands
{
    public abstract class RadioCommand
    {
        public static Radio Execute(CreateDto rawRadioData)
        {
            return new Radio
            (
                mode:           rawRadioData.Mode,
                throttle:       rawRadioData.Throttle / 2.54,
                steer:          (rawRadioData.Steer - 127) * 30,
                potentiometer:  new Potentiometer
                                (
                                   left:  rawRadioData.PotentiometerLeft  / 2.54,
                                   right: rawRadioData.PotentiometerRight / 2.54
                                )
            );
        }

        public class CreateDto
        {
            public int Mode               { get; }
            public int Throttle           { get; }
            public int Steer              { get; }
            public int PotentiometerLeft  { get; }
            public int PotentiometerRight { get; }

            public CreateDto
            (
                int mode,
                int throttle,
                int steer,
                int potentiometerLeft,
                int potentiometerRight
            )
            { 
                Mode               = mode;
                Throttle           = throttle;
                Steer              = steer;
                PotentiometerLeft  = potentiometerLeft;
                PotentiometerRight = potentiometerRight;
            }
        }
    }
}
