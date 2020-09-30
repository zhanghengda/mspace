using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.UavModule.Dto
{
    public class SocketData
    {
        public int cmdRequest { get; set; }
        public int cmdType { get; set; }
        public int mountType { get; set; }
        public int cmdFunction { get; set; }
        public int cmdStep { get; set; }
        public int cmdPlus { get; set; }
        public int cmdZoom { get; set; }
    }
}
