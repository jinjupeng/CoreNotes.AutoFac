using System.Linq;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Service.Base;
using SqlSugar;

namespace CoreNotes.AutoFac.Service
{
    public class UserService: BaseService<User>, IUserService
    {
        public IUserRepository UserRepository;
        public IRoleRepository RoleRepository;
        public IUserRoleService UserRoleService;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleService userRoleService)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            UserRoleService = userRoleService;
        }

        /// <summary>
        /// 自定义分页查询
        /// </summary>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Task<PageModel<User>> QueryPage(int intPageIndex, int intPageSize, string name, int status)
        {
            return UserRepository.QueryPage(intPageIndex, intPageSize, name, status);
        }

        public bool SaveUserAndRole(User user)
        {
            return UserRepository.SaveUserAndRole(user);
        }

        /// <summary>
        /// 根据登录用户信息获取角色字符串（可能不止一个角色）
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>
        public async Task<string> GetUserRoleNameStr(string loginName, string loginPwd)
        {
            string roleName = "";
            var user = (await UserRepository.Query(a => a.LoginName == loginName && a.Pwd == loginPwd)).FirstOrDefault();
            var roleList = await RoleRepository.Query(a => a.IsDelete == false);
            if (user != null)
            {
                var userRoles = await UserRoleService.Query(ur => ur.UserId == user.Id);
                if (userRoles.Count > 0)
                {
                    var arr = userRoles.Select(ur => ur.RoleId.ObjToString()).ToList();
                    var roles = roleList.Where(d => arr.Contains(d.Id.ObjToString()));

                    roleName = string.Join(',', roles.Select(r => r.RoleName).ToArray());
                }
            }
            return roleName;
        }
    }
}