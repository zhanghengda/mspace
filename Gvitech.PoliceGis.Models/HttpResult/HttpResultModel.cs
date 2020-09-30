using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.HttpResult
{
    public  class HttpResultModel
    {
        /// <summary>
        /// 构造方法，初始化code=0,msg=""
        /// </summary>
        public HttpResultModel()
        {
            status = "1";
            message = "";
        }
        /// <summary>
        /// 业务返回值
        /// </summary>
        public Object data { get; set; }

        /// <summary>
        /// 操作结果，一般1是是成功，其他均为失败
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string message { get; set; }
    }
}
