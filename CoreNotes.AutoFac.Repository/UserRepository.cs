using System;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Repository.Base;
using SqlSugar;

namespace CoreNotes.AutoFac.Repository
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// 自定义分页查询条件
        /// </summary>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<PageModel<User>> QueryPage(int intPageIndex, int intPageSize, string name, int status)
        {
            RefAsync<int> totalCount = 0;
            var list = await Task.Run(() => Db.Queryable<User>()
                .Where( a => a.IsDelete == false)
                .WhereIF(!string.IsNullOrWhiteSpace(name), a => a.LoginName == name)
                .WhereIF(status != 0, a => a.Status == status)
                .ToPageListAsync(intPageIndex, intPageSize, totalCount)
            );
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal())).ObjToInt();
            return new PageModel<User>() { DataCount = totalCount, PageCount = pageCount, Page = intPageIndex, PageSize = intPageSize, Data = list };
        }

    }
}