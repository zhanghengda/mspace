using ApplicationConfig;
using LiteDB;
using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.LocalConfigService
{
    /// <summary>
    /// LiteDb类似表的集合抽象接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseConfigCols<T> where T : IUserInfo
    {
        string ConfigKey { get; }
        /// <summary>
        /// 增加新的记录
        /// </summary>
        int Add(T item);

        /// <summary>
        /// 更新记录
        /// </summary>
        bool Update(T item);
        /// <summary>
        /// 查找集合中所有记录
        /// </summary>
        List<T> FindAll();

        /// <summary>
        /// 查找集合中单个记录
        /// </summary>
        T FindOne(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 查找记录
        /// </summary>
        List<T> Find(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 记录是否存在
        /// </summary>
        bool Exist(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 删除记录
        /// </summary>
        int Delete(Expression<Func<T, bool>> predicate);
    }


    public class BaseConfigCols<T> : IBaseConfigCols<T> where T : IUserInfo
    {

        public BaseConfigCols(LiteDbHelper litedbHelper, string configKey, string userName)
        {
            LiteDbHelper = litedbHelper;
            ConfigKey = configKey;
            CurUserName = userName;
        }
        protected LiteDbHelper LiteDbHelper;
        /// <summary>
        /// 集合键值
        /// </summary>
        public string ConfigKey { get; protected set; }
        /// <summary>
        /// 系统当前用户名称
        /// </summary>
        public string CurUserName { get; protected set; }

        /// <summary>
        /// 增加新的记录
        /// </summary>
        public virtual int Add(T item)
        {
            item.UserName = CurUserName;
            return LiteDbHelper.Insert(ConfigKey, item);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        public bool Update(T item)
        {
            item.UserName = CurUserName;
            return LiteDbHelper.Update(ConfigKey, item);
        }

        /// <summary>
        /// 查找所有记录
        /// </summary>
        public List<T> FindAll()
        {
            return LiteDbHelper.GetCollections<T>(ConfigKey, p => p.UserName == CurUserName);
        }


        public List<T> FindQuery(Query query)
        {
            return LiteDbHelper.GetCollectionsQuery<T>(ConfigKey, query);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return LiteDbHelper.Delete<T>(ConfigKey, predicate.AndAlso(p => p.UserName == CurUserName));
        }

        /// <summary>
        /// 查找单个记录
        /// </summary>
        public T FindOne(Expression<Func<T, bool>> predicate)
        {
            var res = LiteDbHelper.GetCollections<T>(ConfigKey, predicate.AndAlso(p => p.UserName == CurUserName));
            if (res?.Count > 0)
                return res[0];
            return default(T);
        }

        /// <summary>
        /// 记录是否存在
        /// </summary>
        public bool Exist(Expression<Func<T, bool>> predicate)
        {

            var value = FindOne(predicate);
            return value != null;
        }

        /// <summary>
        /// 查找记录
        /// </summary>
        public List<T> Find(Expression<Func<T, bool>> predicate)
        {
            return LiteDbHelper.GetCollections<T>(ConfigKey, predicate.AndAlso(p => p.UserName == CurUserName));
        }
    }


}
