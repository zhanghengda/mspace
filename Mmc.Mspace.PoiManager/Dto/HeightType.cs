using Gvitech.CityMaker.RenderControl;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class HeightType : BindableBase
    {
        /// <summary>
        /// 1是海拔高度，3是贴所有对象
        /// </summary>
        public gviHeightStyle HeightStyle { get; set; }

        private string _HeighName;
        /// <summary>
        /// 类型名称
        /// </summary>
        public string HeighName
        {
            get
            {
                return this._HeighName;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._HeighName, value, "HeighName");
            }
        }
        public override string ToString()
        {
            return _HeighName.ToString();
        }
    }
}
