using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Service.Base;

namespace CoreNotes.AutoFac.Service
{
    public class RoleModulePermissionService : BaseService<RoleModulePermission>, IRoleModulePermissionService
    {
        private readonly IRoleModulePermissionRepository _roleModulePermissionRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IRoleRepository _roleRepository;

        public RoleModulePermissionService(IModuleRepository moduleRepository, IRoleRepository roleRepository, IRoleModulePermissionRepository roleModulePermissionRepository)
        {
            _moduleRepository = moduleRepository;
            _roleRepository = roleRepository;
            _roleModulePermissionRepository = roleModulePermissionRepository;
        }

        /// <summary>
        /// 获取全部 角色接口(按钮)关系数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> GetRoleModule()
        {
            var roleModulePermissions = await _roleModulePermissionRepository.Query(a => a.IsDelete == false);
            var roles = await _roleRepository.Query(a => a.IsDelete == false);
            var modules = await _moduleRepository.Query(a => a.IsDelete == false);

            if (roleModulePermissions.Count > 0)
            {
                foreach (var item in roleModulePermissions)
                {
                    item.Role = roles.FirstOrDefault(d => d.Id == item.RoleId);
                    item.Module = modules.FirstOrDefault(d => d.Id == item.ModuleId);
                }

            }
            return roleModulePermissions;
        }
    }
}