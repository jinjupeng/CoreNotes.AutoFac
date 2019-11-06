using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Service.Base;

namespace CoreNotes.AutoFac.Service
{
    public class UserService: BaseService<User>, IUserService
    {
        
    }
}