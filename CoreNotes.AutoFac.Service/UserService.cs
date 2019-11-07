using System.Threading.Tasks;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Service.Base;

namespace CoreNotes.AutoFac.Service
{
    public class UserService: BaseService<User>, IUserService
    {
        public IUserRepository UserRepository;

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public Task<PageModel<User>> QueryPage(int intPageIndex, int intPageSize, string name, int status)
        {
            return UserRepository.QueryPage(intPageIndex, intPageSize, name, status);
        }
    }
}