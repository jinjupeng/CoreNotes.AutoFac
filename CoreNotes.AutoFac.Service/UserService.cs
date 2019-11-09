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

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
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

        public Task<int> SaveUserAndRole(User user)
        {
            // 使用事务
            return UserRepository.Add(user);
        }
    }
}