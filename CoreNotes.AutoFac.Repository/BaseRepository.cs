using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.Model;
using System;
using System.Linq;

namespace CoreNotes.AutoFac.Repository
{
    /// <summary>
    /// 基类仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public virtual T Get(long id)
        {
            //没有连接数据库，利用反射，造个假数据返回用于测试
            T instance = Activator.CreateInstance<T>();

            var stuEntity = instance as StudentEntity;
            if (stuEntity != null)
            {
                stuEntity.Id = id;
                stuEntity.Name = "学生张三";
                stuEntity.Grade = 99;
                return stuEntity as T;
            }

            return instance;
        }

        public virtual IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual long Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
