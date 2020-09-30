using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class PoiInfo : BindableBase
    {
        private double _alt;
        private string _img_url;
        private double _lat;
        private double _lng;
        public double alt
        {
            get { return this._alt; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._alt, value, "alt"); }
        }

        /// <summary>
        /// 1=点，0=巡航线区间，2=巡航线终点
        /// </summary>
        public short camType { get; set; }

        /// <summary>
        /// 标注标签id
        /// 如 1:默认，2:排污等
        /// </summary>
        public int cat_id { get; set; }

        public string cat_Name { get; set; }
        public string detail { get; set; }
        /// <summary>
        /// 水平方向角
        /// </summary>
        public double heading { get; set; }

        public string icon_url { get; set; }
        /// <summary>
        /// 上传至服务器的图片地址
        /// </summary>
        public string img_server_url { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string img_url
        {
            get { return this._img_url; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._img_url, value, "img_url"); }
        }

        public double lat
        {
            get { return this._lat; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._lat, value, "lat"); }
        }

        public double lng
        {
            get { return this._lng; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._lng, value, "lng"); }
        }

        public string marker_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int num { get; set; }

        public double pitch { get; set; }
        public double roll { get; set; }
        public string title { get; set; }
        /// <summary>
        /// 标注类型
        /// 默认为1  1:普通标记   0:动画导航标记
        /// </summary>
        public string type { get; set; }

        public string Address { get; set; }
    }
   


}
