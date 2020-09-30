using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common.Cache
{
    public class NavigationCountIndex : BaseViewModel
    {
        private static int _navigationNameIndex;
        public static int NavigationNameIndexdex
        {
            get
            {
                return _navigationNameIndex;
            }
            set
            {
                _navigationNameIndex = value;
            }
        }
    }
 
}
