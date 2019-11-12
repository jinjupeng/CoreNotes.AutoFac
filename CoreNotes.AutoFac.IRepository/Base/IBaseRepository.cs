using System;
using System.Collections.Generic;
using CoreNotes.AutoFac.Model;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreNotes.AutoFac.IRepository.Base
{
    /// <summary>
    /// 基类仓储interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        Task<T> QueryById(object objId);
        Task<T> QueryById(object objId, bool blnUseCache = false);
        Task<List<T>> QueryByIDs(object[] lstIds);

        Task<int> Add(T model);
        Task<int> Add(List<T> list);
        Task<bool> DeleteById(object id);

        Task<bool> Delete(T model);

        Task<bool> DeleteByIds(object[] ids);

        Task<bool> Update(T model);
        Task<bool> Update(List<T> list);
        Task<bool> Update(T entity, string strWhere);

        Task<bool> Update(T entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        Task<List<T>> Query();
        Task<List<T>> Query(string strWhere);
        Task<List<T>> Query(Expression<Func<T, bool>> whereExpression);
        Task<List<T>> Query(Expression<Func<T, bool>> whereExpression, string strOrderByFileds);
        Task<List<T>> Query(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression, bool isAsc = true);
        Task<List<T>> Query(string strWhere, string strOrderByFileds);

        Task<List<T>> Query(Expression<Func<T, bool>> whereExpression, int intTop, string strOrderByFileds);
        Task<List<T>> Query(string strWhere, int intTop, string strOrderByFileds);

        Task<List<T>> Query(
            Expression<Func<T, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);
        Task<List<T>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);


        Task<PageModel<T>> QueryPage(Expression<Func<T, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null);

        Task<List<TResult>> QueryMuch<T1, T2, T3, TResult>(
            Expression<Func<T1, T2, T3, object[]>> joinExpression,
            Expression<Func<T1, T2, T3, TResult>> selectExpression,
            Expression<Func<T1, T2, T3, bool>> whereLambda = null) where T1 : class, new();
    }
}
