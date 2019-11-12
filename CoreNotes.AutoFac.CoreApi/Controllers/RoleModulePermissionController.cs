using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    public class RoleModulePermissionController : ControllerBase
    {
        private readonly IRoleModulePermissionService _roleModulePermissionService;

        public RoleModulePermissionController(IRoleModulePermissionService roleModulePermissionService)
        {
            _roleModulePermissionService = roleModulePermissionService;
        }

        // TODO：保存角色分配的权限

        /// <summary>
        /// 保存角色分配的权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post(string roleId, string array)
        {
            var data = new MessageModel<string>();

            if (!string.IsNullOrWhiteSpace(roleId) && !string.IsNullOrWhiteSpace(array))
            {
                var arr = array.Split(",");
                var list = new List<RoleModulePermission>();
                foreach (var item in arr)
                {
                    var roleModulePermission = new RoleModulePermission
                    {
                        RoleId = roleId.ObjToInt(),
                        CreateTime = DateTime.Now,
                        IsDelete = false,
                        ModifyTime = DateTime.Now,
                        ModuleId = 1,
                        PermissionId =  1
                    };
                    list.Add(roleModulePermission);
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
    }
}