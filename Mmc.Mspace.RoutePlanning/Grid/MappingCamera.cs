using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Grid
{
   public class MappingCamera
    {
        /// <summary>
        /// 相机名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 像幅长（用了前端Width）
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 像幅宽
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 焦距
        /// </summary>
        public double Focus { get; set; }

        public MappingCamera() { }
    }
}
