using System.Collections.Generic;

namespace Fesb.Hydrocontest.Domain.Mappers
{
    public abstract class Mapper
    {
        public class Warning
        {
            public static Dictionary<int, Warnings> Map = new Dictionary<int, Warnings>()
            {
                { 0,  Warnings.None                 },
                { 1,  Warnings.LowVoltage           },
                { 2,  Warnings.HighCurrent          },
                { 4,  Warnings.ControllerOverheated },
                { 8,  Warnings.BatteryOverheated    },
                { 16, Warnings.MotorOverheated      }
            };
        }

        public class Failure
        {
            public static Dictionary<int, Failures> Map = new Dictionary<int, Failures>()
            {
                { 0,     Failures.None                                  },
                { 1,     Failures.InputSignalNotPresent                 },
                { 2,     Failures.PowerOffRequest                       },
                { 4,     Failures.MemoryError                           },
                { 8,     Failures.SettingsChange                        },
                { 16,    Failures.MotorError                            },
                { 32,    Failures.InternalPowerSourceError              },
                { 64,    Failures.HalPositionLearningProcedureCompleted },
                { 128,   Failures.BatteryTemperatureSensor              },
                { 256,   Failures.MotorError                            },
                { 32768, Failures.Other                                 }
            };
        }
    }
}
