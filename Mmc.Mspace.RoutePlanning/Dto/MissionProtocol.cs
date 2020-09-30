using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Dto
{
   public class MissionProtocol
    {
        public enum  FlightCommand
        {
            Home = 0,
            Normal =16,
            TakeOff =22,
            Landing =21,
            Velocity =178,
            Yaw =115,
            PitchAngle = 205,
            TakePhoto = 203
        }
    }
}
