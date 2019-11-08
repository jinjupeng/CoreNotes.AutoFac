using System.Threading.Tasks;
using CoreNotes.AutoFac.IService.Base;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IService
{
    public interface IRoleService: IBaseService<Role>
    {
        Task<PageModel<Role>> QueryPage(int pageIndex, int pageSize, string name);
    }
}