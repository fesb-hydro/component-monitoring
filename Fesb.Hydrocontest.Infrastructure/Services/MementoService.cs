using Fesb.Hydrocontest.Domain.Entities;
using Fesb.Hydrocontest.Infrastructure.Extensions;

namespace Fesb.Hydrocontest.Infrastructure.Services
{
    public interface IMementoService
    {
        bool AreAllStatesUnchanged(Radio radio, Motor motor, Boat boat);
    }

    public class MementoService : IMementoService
    {
        private Radio _previousStateOfRadio;
        private Motor _previousStateOfMotor;
        private Boat  _previousStateOfBoat;

        public bool AreAllStatesUnchanged(Radio radio, Motor motor, Boat boat)
        {
            if 
            (
                _previousStateOfRadio.IsNull() &&
                _previousStateOfMotor.IsNull() &&
                _previousStateOfBoat .IsNull()
            )
            {
                _previousStateOfRadio = radio;
                _previousStateOfMotor = motor;
                _previousStateOfBoat  = boat;
                return false;
            }

            var areAllStatesUnchanged = _previousStateOfRadio == radio
                                     && _previousStateOfMotor == motor 
                                     && _previousStateOfBoat  == boat;

            if (!areAllStatesUnchanged)
            {
                _previousStateOfRadio = radio;
                _previousStateOfMotor = motor;
                _previousStateOfBoat  = boat;
            }

            return areAllStatesUnchanged;
        }
    }
}
