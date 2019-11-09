using System;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Repository.Base;
using CoreNotes.AutoFac.Repository.sugar;
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

        /// <summary>
        /// 更新用户和角色表关系
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool SaveUserAndRole(User user)
        {
            var db = new SimpleClient(Db);
            #region 使用事务

            var result = Db.Ado.UseTran(() =>
            {
                user.LastErrTime = DateTime.Now;
                user.UpdateTime = DateTime.Now;
                db.Update(user);
                
                var userRole = Db.Queryable<UserRole>().Where(a => a.UserId == user.Id).First();
                if (userRole != null)
                {
                    userRole.RoleId = user.RoleId;
                    userRole.ModifyTime = DateTime.Now;
                    db.Update(userRole);
                }
                else
                {
                    var userRoleEntity = new UserRole
                    {
                        IsDelete = false,
                        UserId = user.Id,
                        RoleId = user.RoleId,
                        CreateTime = DateTime.Now,
                        ModifyTime = DateTime.Now
                    };

                    db.Insert(userRoleEntity);
                }

            });
            #endregion

            return result.IsSuccess;
        }
    }
}