using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    /// <summary>
    /// 权限分配接口
    /// </summary>
    //[Authorize("Permission")]
    [Route("[controller]/[action]")]
    public class RoleModulePermissionController : ControllerBase
    {
        private readonly IRoleModulePermissionService _roleModulePermissionService;
        private readonly IPermissionService _permissionService;

        public RoleModulePermissionController(IRoleModulePermissionService roleModulePermissionService, IPermissionService permissionService)
        {
            _roleModulePermissionService = roleModulePermissionService;
            _permissionService = permissionService;
        }

        /// <summary>
        /// 保存角色分配的权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post(int roleId, string array)
        {
            var data = new MessageModel<string>();

            if (roleId > 0 && !string.IsNullOrWhiteSpace(array))
            {
                var roleModulePermissions = await _roleModulePermissionService.Query(a => a.RoleId == roleId.ObjToInt()).ConfigureAwait(false);
                var arr = array.Split(",").ToList();
                var remove = roleModulePermissions.Where(d => !arr.Contains(d.PermissionId.ObjToString())).Select(c => (object)c.Id);
                // 保存之前删除已存在的权限
                data.Success |= await _roleModulePermissionService.DeleteByIds(remove.ToArray()).ConfigureAwait(false);
                
                var list = new List<RoleModulePermission>();
                foreach (var item in arr)
                {
                    var rmpItem = roleModulePermissions.Where(d => d.PermissionId == item.ObjToInt());
                    if (!rmpItem.Any())
                    {
                        var moduleId = (await _permissionService.Query(p => p.Id == item.ObjToInt()).ConfigureAwait(false)).FirstOrDefault()?.Mid;
                        var roleModulePermission = new RoleModulePermission
                        {
                            RoleId = roleId.ObjToInt(),
                            CreateTime = DateTime.Now.Date,
                            IsDelete = false,
                            ModifyTime = DateTime.Now.Date,
                            ModuleId = moduleId,
                            PermissionId = item.ObjToInt()
                        };
                        list.Add(roleModulePermission);
                    }

                };
                var result = await _roleModulePermissionService.Add(list).ConfigureAwait(false);
                data.Success = result > 0;
                if (data.Success)
                {
                    data.Response = result.ObjToString();
                    data.Msg = "添加成功";
                }
            }

            // TODO：添加日志到数据库
            return data;
        }

        /// <summary>
        /// 通过角色id获取菜单和按钮【无权限】
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<List<int>>> GetPermissionIdByRoleId(int roleId = 0)
        {
            var data = new MessageModel<List<int>>();

            var roleModulePermissions = await _roleModulePermissionService.Query(d => d.IsDelete == false && d.RoleId == roleId).ConfigureAwait(false);
            var permissionTrees = (from child in roleModulePermissions
                orderby child.Id
                select child.PermissionId.ObjToInt()).ToList();

            data.Success = true;
            if (data.Success)
            {
                data.Response = permissionTrees;
                data.Msg = "获取成功";
            }

            return data;
        }
    }
}