using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class PoiType : BindableBase
    {
        private string _typeName;
        /// <summary>
        /// 类型名称
        /// </summary>
        public string cat_name
        {
            get
            {
                return this._typeName;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._typeName, value, "TypeName");
            }
        }

        /// <summary>
        /// id
        /// </summary>
        public int cat_id { get; set; }
        /// <summary>
        /// 图标路径
        /// </summary>
        public string cat_url { get; set; }

        public override string ToString()
        {
            return _typeName.ToString();
        }
    }
}
