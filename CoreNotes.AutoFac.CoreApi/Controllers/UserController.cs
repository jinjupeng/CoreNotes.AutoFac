using System;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<User>> Get(int id)
        {
            var model = await _userService.QueryById(id).ConfigureAwait(false);
            MessageModel<User> message = new MessageModel<User>
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
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name">登录名或昵称</param>
        /// <param name="status">用户状态：启用/禁用</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<User>>> GetList(int pageIndex, int pageSize, string name, int status)
        {
            var data = await _userService.QueryPage(pageIndex, pageSize, name, status).ConfigureAwait(false);
            var message = new MessageModel<PageModel<User>>
            {
                Msg = "获取成功！",
                Success = data.DataCount > 0,
                Response = data
            };
            return message;
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post(User user)
        {
            var data = new MessageModel<string>();

            if (user != null)
            {
                user.IsDelete = false;
                user.CreateTime = DateTime.Now;
                user.UpdateTime = DateTime.Now;
                user.LastErrTime = DateTime.Now;
                var result = await _userService.Add(user).ConfigureAwait(false);
                data.Success = result > 0;
                if (data.Success)
                {
                    data.Response = result.ObjToString();
                    data.Msg = "添加成功";
                }
            }
            // TODO：md5加密密码

            // TODO：添加日志到数据库
            return data;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put( User user)
        {
            var data = new MessageModel<string>();
            if (user != null && user.Id > 0)
            {
                user.LastErrTime = DateTime.Now;
                user.UpdateTime = DateTime.Now;
                var result = await _userService.Update(user).ConfigureAwait(false);
                data.Success = result;
                if (data.Success)
                {
                    data.Response = user?.Id.ObjToString();
                    data.Msg = "添加成功";
                }
            }
            // TODO：md5加密密码

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
                var userDetail = await _userService.QueryById(id).ConfigureAwait(false);
                userDetail.IsDelete = true;
                data.Success = await _userService.Update(userDetail).ConfigureAwait(false);
                if (data.Success)
                {
                    data.Msg = "删除成功";
                    data.Response = userDetail?.Id.ObjToString();
                }
            }

            return data;
        }

        // TODO：修改密码

        // TODO：退出登录

    }
}