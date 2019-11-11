using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IService.Base;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    /// <summary>
    /// 接口模块
    /// </summary>
    [Route("[controller]/[action]")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Module>> Get(int id)
        {
            var model = await _moduleService.QueryById(id).ConfigureAwait(false);
            MessageModel<Module> message = new MessageModel<Module>
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
        public async Task<MessageModel<PageModel<Module>>> GetList(int pageIndex, int pageSize, string name)
        {
            var data = await _moduleService.QueryPage(a => a.IsDelete == false, pageIndex, pageSize).ConfigureAwait(false);
            var message = new MessageModel<PageModel<Module>>
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
        public async Task<MessageModel<List<Module>>> GetAll()
        {
            var message = new MessageModel<List<Module>>();
            var data = await _moduleService.Query(a => a.IsDelete == false && a.Enabled == true).ConfigureAwait(false);
            if (data.Count >= 0)
            {
                message.Msg = "查询成功！";
                message.Success = true;
                message.Response = data;
            }

            return message;
        }

        /// <summary>
        /// 新增接口信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post(Module module)
        {
            var data = new MessageModel<string>();

            if (module != null)
            {
                var arr = module.LinkUrl.Split("/");
                if (arr.Length != 3)
                {
                    data.Msg = "参数错误";
                    return data;
                }

                module.Controller = arr[1];
                module.Action = arr[2];
                module.IsDelete = false;
                module.CreateTime = DateTime.Now;
                module.ModifyTime = DateTime.Now;
                var result = await _moduleService.Add(module).ConfigureAwait(false);
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
        /// 更新接口信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put(Module module)
        {
            var data = new MessageModel<string>();
            if (module != null && module.Id > 0)
            {
                var arr = module.LinkUrl.Split("/");
                if (arr.Length != 3)
                {
                    data.Msg = "参数错误";
                    return data;
                }

                module.Controller = arr[1];
                module.Action = arr[2];
                module.ModifyTime = DateTime.Now;
                var result = await _moduleService.Update(module).ConfigureAwait(false);
                data.Success = result;
                if (data.Success)
                {
                    data.Response = module?.Id.ObjToString();
                    data.Msg = "添加成功";
                }
            }

            // TODO：添加日志到数据库
            return data;
        }

        /// <summary>
        /// 删除单条接口数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var moduleDetail = await _moduleService.QueryById(id).ConfigureAwait(false);
                moduleDetail.IsDelete = true;
                data.Success = await _moduleService.Update(moduleDetail).ConfigureAwait(false);
                if (data.Success)
                {
                    data.Msg = "删除成功";
                    data.Response = moduleDetail?.Id.ObjToString();
                }
            }

            return data;
        }
    }
}