using ApplicationConfig;
using LiteDB;
using Mmc.Mspace.Const.ConstPath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.LocalConfigService
{
    public class LiteDbHelper
    {
        public LiteDatabase LiteDb { get; private set; }

        public bool OpenDb(string dbPath)
        {
            var db = new LiteDatabase(dbPath);
            LiteDb = db;
            return db != null;
        }

        public LiteFileInfo UpLoadFile(string key, string fileName)
        {
            // 从文件系统上传一个文件到数据库
            return LiteDb.FileStorage.Upload(key, fileName);
        }

        public List<T> GetCollections<T>(string key)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.FindAll().ToList<T>();
        }


        public List<T> GetCollections<T>(string key, Expression<Func<T, bool>> predicate)
        {
            var cols = LiteDb.GetCollection<T>(key);
            if (cols.Count() == 0)
                return null;
            var res = cols.Find(predicate);
            if (res.HasValues())
                return res.ToList<T>();
            return null;
        }

        public List<T> GetCollectionsQuery<T>(string key, Query query)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.Find(query).ToList();
        }

        public int Insert<T>(string key, T value)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.Insert(value);
        }

        public int Delete<T>(string key, Expression<Func<T, bool>> predicate)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.Delete(predicate);
        }

        public int Delete<T>(string key, Query query)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.Delete(query);
        }

        public bool Update<T>(string key, T newVaule)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.Update(newVaule);
        }

        public bool Exists<T>(string key, Query query)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.Exists(query);
        }

        public bool Exists<T>(string key, Expression<Func<T, bool>> predicate)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.Exists(predicate);
        }
        public T FindOne<T>(string key, Expression<Func<T, bool>> predicate)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.FindOne(predicate);
        }

        public T FindOne<T>(string key, Query query)
        {
            var cols = LiteDb.GetCollection<T>(key);
            return cols.FindOne(query);
        }
    }
}
