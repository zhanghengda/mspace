using Gvitech.CityMaker.FdeGeometry;
using System;

namespace Mmc.Mspace.IotModule.Models
{
    public class PatroledDataForClient
    {
        public DateTime? Time { get; set; }
        public IPoint Point { get; set; }
        public bool IsLegalPoint { get; set; }
        /// <summary>
        /// for mark a connection changed
        /// </summary>
        public string connect_name { get; set; }
    }
}
