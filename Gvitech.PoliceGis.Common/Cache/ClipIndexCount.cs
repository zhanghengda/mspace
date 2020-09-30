using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common.Cache
{
    public class ClipIndexCount : BaseViewModel
    {
        private static int _gviClipCustomePlaneIndex;
        public static int GviClipCustomePlaneIndex
        {
            get { return _gviClipCustomePlaneIndex; }
            set { _gviClipCustomePlaneIndex = value; } 
        }

        private static int _gviClipBoxIndex;
        public static int GviClipBoxIndex
        {
            get { return _gviClipBoxIndex; }
            set { _gviClipBoxIndex = value; }
        }
    }
}
