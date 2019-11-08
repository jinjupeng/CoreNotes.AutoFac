using System;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Repository.Base;
using SqlSugar;

namespace CoreNotes.AutoFac.Repository
{
    public class RoleRepository: BaseRepository<Role>, IRoleRepository
    {
        /// <summary>
        /// 自定义分页查询
        /// </summary>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="name">角色名</param>
        /// <returns></returns>
        public async Task<PageModel<Role>> QueryPage(int intPageIndex, int intPageSize, string name)
        {
            RefAsync<int> totalCount = 0;
            var list = await Task.Run(() => Db.Queryable<Role>()
                .Where(a => a.IsDelete == false)
                .WhereIF(!string.IsNullOrWhiteSpace(name), a => a.RoleName == name)
                .ToPageListAsync(intPageIndex, intPageSize, totalCount)
            );
            int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal())).ObjToInt();
            return new PageModel<Role>() { DataCount = totalCount, PageCount = pageCount, Page = intPageIndex, PageSize = intPageSize, Data = list };
        }
    }
}