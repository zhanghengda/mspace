using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.UavModule.Dto
{
    class CamData
    {
        public uint camType { get; set; }
        public uint camRecState { get; set; }
        public uint camModeState { get; set; }
        public uint camZoomState { get; set; }
        public uint camMountVersion { get; set; }
        public uint camCamPitchAngle { get; set; }
        public uint camCamHeadAngle { get; set; }
        public uint camCamRoolAngle { get; set; }
        public uint camCamZoom { get; set; }
    }
}
