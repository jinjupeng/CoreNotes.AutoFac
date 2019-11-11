using System.Threading.Tasks;
using CoreNotes.AutoFac.IRepository.Base;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IRepository
{
    public interface IRoleRepository: IBaseRepository<Role>
    {
        Task<PageModel<Role>> QueryPage(int pageIndex, int pageSize, bool enabled, string name);
    }
}