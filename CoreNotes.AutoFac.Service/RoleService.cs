using System.Threading.Tasks;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Service.Base;

namespace CoreNotes.AutoFac.Service
{
    public class RoleService: BaseService<Role>, IRoleService
    {
        public IRoleRepository RoleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            RoleRepository = roleRepository;
        }
        public Task<PageModel<Role>> QueryPage(int pageIndex, int pageSize, bool enabled, string name)
        {
            return RoleRepository.QueryPage(pageIndex, pageSize, enabled, name);
        }
    }
}