using System;
using Fesb.Hydrocontest.Domain.Entities;
using Fesb.Hydrocontest.Domain.ValueObjects;

namespace Fesb.Hydrocontest.Domain.Commands
{
    public abstract class BoatCommand
    {
        public static Boat Execute(CreateDto rawBoatData)
        {
            return new Boat
            (
                height:    new Height
                           (
                               lowerPoint:  rawBoatData.HeightLowerPoint  * 0.0859536,
                               higherPoint: rawBoatData.HeightHigherPoint * 0.0859536
                           ),
                pitch:     Math.Asin((rawBoatData.HeightHigherPoint - rawBoatData.HeightLowerPoint) * 0.0859536 / 1200.0) * (180.0 / Math.PI),
                roll:      rawBoatData.Roll - 127,
                speed:     Math.Sqrt(2.0 / 1024.0 * rawBoatData.Speed),
                foilAngle: rawBoatData.FoilAngle - 127
            );
        }

        public class CreateDto
        {
            public int HeightLowerPoint  { get; }
            public int HeightHigherPoint { get; }
            public int Roll              { get; }
            public int Speed             { get; }
            public int FoilAngle         { get; }

            public CreateDto
            (
                int heightLowerPoint,
                int heightHigherPoint,
                int roll,
                int speed,
                int foilAngle
            )
            {
                HeightLowerPoint  = heightLowerPoint;
                HeightHigherPoint = heightHigherPoint;
                Roll              = roll;
                Speed             = speed;
                FoilAngle         = foilAngle;
            }
        }
    }
}
