using Mmc.Mspace.Models.HttpResult;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mmc.Mspace.Services.HttpService
{
    public class RequestResultReSolve : Singleton<RequestResultReSolve>, IRequestResultReSolve
    {
        public static RequestResultReSolve GetDefault(object obj)
        {
            return Singleton<RequestResultReSolve>.Instance;
        }

        public HttpResult<T> ReSolveRequestResult<T>(string requestResult, Dictionary<string, Type> keys) where T : class, new()
        {
            HttpResult<T> result = null;
            bool flag = string.IsNullOrEmpty(requestResult);
            HttpResult<T> result2;
            if (flag)
            {
                result2 = result;
            }
            else
            {
                try
                {
                    SystemLog.Log("开始解析Http请求结果", 0);
                    object obj = JsonConvert.DeserializeObject(requestResult);
                    bool flag2 = obj == null;
                    if (flag2)
                    {
                        return result;
                    }
                    result = new HttpResult<T>
                    {
                        RequestResult = Activator.CreateInstance<T>()
                    };
                    JObject jobj = obj as JObject;
                    RequestResultReSolve.SetPropertyValue(jobj, "code", result);
                    RequestResultReSolve.SetPropertyValue(jobj, "msg", result);
                    bool flag3 = IDictionaryExtension.HasValues<string, Type>(keys);
                    if (flag3)
                    {
                        IEnumerableExtension.ForEach<KeyValuePair<string, Type>>(keys, delegate (KeyValuePair<string, Type> kvp)
                        {
                            RequestResultReSolve.SetPropertyValue(jobj, kvp.Key, result.RequestResult);
                        });
                    }
                    SystemLog.Log("解析Http请求结果结束", 0);
                }
                catch (Exception ex)
                {
                    SystemLog.Log("开始解析Http请求结果异常", LogMessageType.ERROR);
                    SystemLog.Log(ex);
                    throw ex;
                }
                result2 = result;
            }
            return result2;
        }

        private static bool SetPropertyValue(JObject jobj, string alias, object targetObj)
        {
            bool flag = string.IsNullOrEmpty(alias);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                JToken jtoken = null;
                bool flag2 = !jobj.TryGetValue(alias, out jtoken);
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    bool flag3 = jtoken == null;
                    if (flag3)
                    {
                        result = false;
                    }
                    else
                    {
                        PropertyInfo propertyByAlias = RequestResultReSolve.GetPropertyByAlias(targetObj.GetType(), targetObj, alias);
                        bool flag4 = propertyByAlias == null;
                        if (flag4)
                        {
                            result = false;
                        }
                        else
                        {
                            Type propertyType = propertyByAlias.PropertyType;
                            object value = (Type.GetTypeCode(propertyType) == TypeCode.Object) ? JsonConvert.DeserializeObject(jtoken.ToString(), propertyType) : ((JValue)jtoken).Value;
                            propertyByAlias.SetValue(targetObj, value);
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        private static PropertyInfo GetPropertyByAlias(Type type, object obj, string alias)
        {
            bool flag = obj == null || string.IsNullOrEmpty(alias);
            PropertyInfo result;
            if (flag)
            {
                result = null;
            }
            else
            {
                PropertyInfo[] properties = type.GetProperties();
                bool flag2 = !CollectionExtension.HasValues<PropertyInfo>(properties);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        string aliasName = PropertyInfoExtension.GetAliasName(propertyInfo);
                        bool flag3 = PropertyInfoExtension.GetAliasName(propertyInfo).Equals(alias);
                        if (flag3)
                        {
                            return propertyInfo;
                        }
                    }
                    result = null;
                }
            }
            return result;
        }

        private static T GetValue<T>(JObject obj, string key)
        {
            T result = default(T);
            JToken jtoken;
            bool flag = obj.TryGetValue(key, out jtoken);
            if (flag)
            {
                result = StringExtension.ParseTo<T>(jtoken.ToString(), default(T));
            }
            return result;
        }
    }
}