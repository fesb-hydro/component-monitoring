using System.Collections.Generic;
using System.Linq;
using Fesb.Hydrocontest.Domain.Commands;
using Fesb.Hydrocontest.Infrastructure.Services;

namespace Fesb.Hydrocontest.Infrastructure.Parsers
{
    public enum RadioIds
    {
        Mode               = 1,
        Throttle           = 2,
        Steer              = 3,
        PotentiometerLeft  = 4,
        PotentiometerRight = 5
    }

    public abstract class RadioParser
    {
        public static RadioCommand.CreateDto Parse(IList<InputDto> radioData)
        {
             return new RadioCommand.CreateDto
             (
                 mode:               radioData.Single(x => x.Id == (int)RadioIds.Mode              ).Value,
                 throttle:           radioData.Single(x => x.Id == (int)RadioIds.Throttle          ).Value,
                 steer:              radioData.Single(x => x.Id == (int)RadioIds.Steer             ).Value,
                 potentiometerLeft:  radioData.Single(x => x.Id == (int)RadioIds.PotentiometerLeft ).Value,
                 potentiometerRight: radioData.Single(x => x.Id == (int)RadioIds.PotentiometerRight).Value
             );
        }
    }
}
