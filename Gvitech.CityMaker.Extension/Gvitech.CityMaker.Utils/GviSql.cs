using Gvitech.CityMaker.FdeCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gvitech.CityMaker.Utils
{
    /// <summary>
    /// FdeSql转换
    /// </summary>
    public class GviSql
    {
        /// <summary>
        ///     创建IN的sql语句
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValues">字段值</param>
        /// <param name="isCharType">
        ///     字符类型
        ///     ture 形（fieldName in （'1'，'2'，..'n'） ）
        ///     false 形（fieldName in （1，2，..n） ）
        /// </param>
        /// <returns></returns>
        public static string CreateInSql(string fieldName, System.Collections.Generic.List<string> fieldValues, bool isCharType)
        {
            if (fieldValues == null || fieldValues.Count == 0 || string.IsNullOrEmpty(fieldName))
            {
                return null;
            }
            return GviSql.CreateInSql(fieldName, fieldValues.ToArray(), isCharType);
        }

        /// <summary>
        ///     处理时间格式
        /// </summary>
        /// <param name="time"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string OperateTimeSql(System.DateTime time, gviConnectionType type)
        {
            string fieldValue;
            switch (type)
            {
                case gviConnectionType.gviConnectionMySql5x:
                case gviConnectionType.gviConnectionFireBird2x:
                    fieldValue = string.Format(" cast('{0}' as date) ", time.ToString("d"));
                    break;
                case gviConnectionType.gviConnectionOCI11:
                    fieldValue = string.Format("to_date('{0}','YYYY-MM-DD') ", time.ToString("d"));
                    break;
                default:
                    fieldValue = string.Format(" cast('{0}' as date) ", time.ToString("d"));
                    break;
            }
            return fieldValue;
        }

        /// <summary>
        ///     创建IN的sql语句
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValues">字段值</param>
        /// <param name="isCharType">
        ///     字符类型
        ///     ture 形（fieldName in （'1'，'2'，..'n'） ）
        ///     false 形（fieldName in （1，2，..n） ）
        /// </param>
        /// <returns></returns>
        public static string CreateInSql(string fieldName, string[] fieldValues, bool isCharType)
        {
            if (fieldValues == null || fieldValues.Length == 0 || string.IsNullOrEmpty(fieldName))
            {
                return null;
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (fieldValues.Length == 1)
            {
                string fieldValue = fieldValues[0];
                if (isCharType)
                {
                    fieldValue = string.Format("'{0}'", fieldValue);
                }
                sb.AppendFormat("{0}={1}", fieldName, fieldValue);
            }
            else
            {
                for (int i = 0; i < fieldValues.Length; i++)
                {
                    string fieldValue = fieldValues[i];
                    if (isCharType)
                    {
                        fieldValue = string.Format("'{0}'", fieldValue);
                    }
                    if (i == 0)
                    {
                        sb.AppendFormat("{0} in ({1},", fieldName, fieldValue);
                    }
                    else if (i == fieldValues.Length - 1)
                    {
                        sb.AppendFormat("{0})", fieldValue);
                    }
                    else
                    {
                        sb.AppendFormat("{0},", fieldValue);
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取SubFields
        /// </summary>
        /// <param name="lstFields"></param>
        /// <returns></returns>
        public static string GetSubFields(System.Collections.Generic.List<string> lstFields)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string item in lstFields)
            {
                sb.Append("," + item);
            }
            sb.Remove(0, 1);
            return sb.ToString();
        }
    }
}
