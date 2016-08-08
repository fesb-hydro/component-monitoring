using System.Collections.Generic;
using System.Linq;
using Fesb.Hydrocontest.Domain.Commands;
using Fesb.Hydrocontest.Infrastructure.Services;

namespace Fesb.Hydrocontest.Infrastructure.Parsers
{
    public enum BoatIds
    {
        HeightLowerPoint  = 13,
        HeightHigherPoint = 14,
        Roll              = 15,
        Speed             = 16,
        FoilAngle         = 17
    }

    public abstract class BoatParser
    {
        public static BoatCommand.CreateDto Parse(IList<InputDto> boatData)
        {
            return new BoatCommand.CreateDto
            (
                heightLowerPoint:  boatData.Single(x => x.Id == (int)BoatIds.HeightLowerPoint ).Value,
                heightHigherPoint: boatData.Single(x => x.Id == (int)BoatIds.HeightHigherPoint).Value,
                roll:              boatData.Single(x => x.Id == (int)BoatIds.Roll             ).Value,
                speed:             boatData.Single(x => x.Id == (int)BoatIds.Speed            ).Value,
                foilAngle:         boatData.Single(x => x.Id == (int)BoatIds.FoilAngle        ).Value
            );
        }
    }
}
