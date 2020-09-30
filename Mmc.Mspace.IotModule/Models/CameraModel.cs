using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.Models
{
    public class CameraModel: BindableBase
    {

//        | 字段名称     | 类型   | 字段说明                 |
//| ------------ | ------ | -------------------- |  
//| message       | string | 描述信息| 
//| status   | int | 1成功 其他失败           |   
//| data   | array | 返回数据           |   
//| id   | int | 序号           |  
//| name       | string | 摄像头编号| 
//| sn       | string | 简号| 
//| category_id       | string | 摄像头类型| 
//| installation_method       | string | 安装方式| 
//| high_low       | string | 区域| 
//| type       | string |  图层类型| 
//| status       | int | 1正常其他非正常状态| 
//| longitude       | string | 经度| 
//| latitude       | string | 纬度| 
//| height       | string | 高度| 
//| blind_spot       | string | 盲区角度| 
//| view_angle       | string | 视野角度| 
//| initial_angle       | string | 摄像头初始角度 | 
//| video_stream       | string | 视频流地址 | 
//| video_type       | int | 视频类型| 
//| area_id       | int | 所属网格|  
//| remark       | string | 备注信息|  
//| add_time       | string | 添加时间|  
        private bool _isSelected;
        private string _showname;
        public string newGuid
        {
            get;
            set;
        }
        public string id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string sn
        {
            get;
            set;
        }
        public string category_id
        {
            get;
            set;
        }
        public string installation_method
        {
            get;
            set;
        }
        public string high_low
        {
            get;
            set;
        }
        public string geom
        {
            get;
            set;
        }
        public string type
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }
        public string longitude
        {
            get;
            set;
        }
        public string latitude
        {
            get;
            set;
        }
        public string height
        {
            get;
            set;
        }
        public string blind_spot
        {
            get;
            set;
        }
        public string view_angle
        {
            get;
            set;
        }
        public string initial_angle
        {
            get;
            set;
        }
        public string video_stream
        {
            get;
            set;
        }
        public string video_type
        {
            get;
            set;
        }
        public string area_id
        {
            get;
            set;
        }


        public string remark
        {
            get;
            set;
        }
        public string add_time
        {
            get;
            set;
        }
        public bool IsSelected
        {
            get
            {
                return this._isSelected;
            }
            set
            {
                this._isSelected = value;
                base.NotifyPropertyChanged("IsSelected");
            }
        }

        private bool isForward;
        public bool IsForward
        {
            get
            {
                return this.isForward;
            }
            set
            {
                this.isForward = value;
                base.NotifyPropertyChanged("IsForward");
            }
        }
        public string showname
        {
            get
            {
                return this._showname;
            }
            set
            {
                this._showname = value;
                base.NotifyPropertyChanged("showname");
            }
        }
    }
}
