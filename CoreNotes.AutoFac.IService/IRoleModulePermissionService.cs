using System.Collections.Generic;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IService.Base;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IService
{
    public interface IRoleModulePermissionService: IBaseService<RoleModulePermission>
    {
        Task<List<RoleModulePermission>> GetRoleModule();
    }
}