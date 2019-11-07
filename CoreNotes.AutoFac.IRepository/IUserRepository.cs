using System.Threading.Tasks;
using CoreNotes.AutoFac.IRepository.Base;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IRepository
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<PageModel<User>> QueryPage(int intPageIndex, int intPageSize, string name, int status);
    }
}