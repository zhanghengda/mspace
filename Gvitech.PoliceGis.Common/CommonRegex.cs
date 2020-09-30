using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common
{
    public  class CommonRegex
    {
        /// <summary>
        /// 手机匹配正则表达式
        /// </summary>
        public const string TelRegex = @"^0{0,1}((13[0-9])|(15[0-9])|(18[0-9])|(14[0-9])|(17[0-9]))+\d{8}$";// @"^1[3578]\d{9}$";

        /// <summary>
        /// 数字和小数点
        /// </summary>
        public const string NumberAndDel = @"[^\d.\d]";

        /// <summary>
        /// 数字的正则，只能包含数字
        /// </summary>
        public const string NumberRegex = @"^\d+(\.\d+)?$";
    }
}
