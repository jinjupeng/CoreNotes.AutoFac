using System.Threading.Tasks;
using CoreNotes.AutoFac.IService.Base;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IService
{
    public interface IUserService: IBaseService<User>
    {
        /// <summary>
        /// 自定义分页查询
        /// </summary>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<PageModel<User>> QueryPage(int intPageIndex, int intPageSize, string name, int status);

        // 保存用户信息到User表，保存用户的角色信息到UserRole表
        bool SaveUserAndRole(User user);
    }
}