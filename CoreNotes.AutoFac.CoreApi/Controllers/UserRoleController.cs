using System;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    [Authorize("Permission")]
    [Route("[controller]/[action]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<UserRole>> Get(int id)
        {
            var model = await _userRoleService.QueryById(id).ConfigureAwait(false);
            MessageModel<UserRole> message = new MessageModel<UserRole>
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
        public async Task<MessageModel<PageModel<UserRole>>> GetList(int pageIndex, int pageSize)
        {
            var data = await _userRoleService.QueryPage(null, pageIndex, pageSize).ConfigureAwait(false);
            var message = new MessageModel<PageModel<UserRole>>
            {
                Msg = "获取成功！",
                Success = data.DataCount > 0,
                Response = data
            };
            return message;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post(UserRole userRole)
        {
            var data = new MessageModel<string>();

            if (userRole != null)
            {
                userRole.IsDelete = false;
                userRole.CreateTime = DateTime.Now;
                userRole.ModifyTime = DateTime.Now;
                var result = await _userRoleService.Add(userRole).ConfigureAwait(false);
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
        /// 更新
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put(UserRole userRole)
        {
            var data = new MessageModel<string>();
            if (userRole != null && userRole.Id > 0)
            {
                userRole.ModifyTime = DateTime.Now;
                var result = await _userRoleService.Update(userRole).ConfigureAwait(false);
                data.Success = result;
                if (data.Success)
                {
                    data.Response = userRole?.Id.ObjToString();
                    data.Msg = "添加成功";
                }
            }

            // TODO：添加日志到数据库
            return data;
        }

        /// <summary>
        /// 删除单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var userRoleDetail = await _userRoleService.QueryById(id).ConfigureAwait(false);
                userRoleDetail.IsDelete = true;
                data.Success = await _userRoleService.Update(userRoleDetail).ConfigureAwait(false);
                if (data.Success)
                {
                    data.Msg = "删除成功";
                    data.Response = userRoleDetail?.Id.ObjToString();
                }
            }

            return data;
        }
    }
}