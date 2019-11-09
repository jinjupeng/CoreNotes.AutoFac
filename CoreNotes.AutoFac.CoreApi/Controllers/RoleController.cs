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
    /// <summary>
    /// 用户模块接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Role>> Get(int id)
        {
            var model = await _roleService.QueryById(id).ConfigureAwait(false);
            MessageModel<Role> message = new MessageModel<Role>
            {
                Msg = "获取成功！",
                Success = true,
                Response = model
            };
            return message;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Role>>> GetList(int pageIndex, int pageSize, string name)
        {
            var data = await _roleService.QueryPage(pageIndex, pageSize, name).ConfigureAwait(false);
            var message = new MessageModel<PageModel<Role>>
            {
                Msg = "获取成功！",
                Success = data.DataCount > 0,
                Response = data
            };
            return message;
        }
        /// <summary>
        /// 不分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<Role>>> GetAll()
        {
            var message = new MessageModel<List<Role>>();
            var data = await _roleService.Query(a => a.IsDelete == false && a.Enabled == true).ConfigureAwait(false);
            if (data.Count >= 0)
            {
                message.Msg = "查询成功！";
                message.Success = true;
                message.Response = data;
            }
            return message;
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post(Role role)
        {
            var data = new MessageModel<string>();

            if (role != null)
            {
                role.IsDelete = false;
                role.CreateTime = DateTime.Now;
                role.ModifyTime = DateTime.Now;
                var result = await _roleService.Add(role).ConfigureAwait(false);
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
        /// 更新用户信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put(Role role)
        {
            var data = new MessageModel<string>();
            if (role != null && role.Id > 0)
            {
                role.ModifyTime = DateTime.Now;
                var result = await _roleService.Update(role).ConfigureAwait(false);
                data.Success = result;
                if (data.Success)
                {
                    data.Response = role?.Id.ObjToString();
                    data.Msg = "添加成功";
                }
            }

            // TODO：添加日志到数据库
            return data;
        }

        /// <summary>
        /// 删除单条用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var roleDetail = await _roleService.QueryById(id).ConfigureAwait(false);
                roleDetail.IsDelete = true;
                data.Success = await _roleService.Update(roleDetail).ConfigureAwait(false);
                if (data.Success)
                {
                    data.Msg = "删除成功";
                    data.Response = roleDetail?.Id.ObjToString();
                }
            }

            return data;
        }

    }
}