using System.Threading.Tasks;
using CoreNotes.AutoFac.IService.Base;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IService
{
    public interface IUserService: IBaseService<User>
    {
        Task<PageModel<User>> QueryPage(int intPageIndex, int intPageSize, string name, int status);
    }
}