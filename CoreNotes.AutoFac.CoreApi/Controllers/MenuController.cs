using CoreNotes.AutoFac.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.DTO;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<List<MenuDto>> GetMenuTree()
        {
            MessageModel<List<MenuDto>> message = new MessageModel<List<MenuDto>>();
            var result = _menuService.GetMenuTree();
            if (result != null)
            {
                message.Msg = "查询成功！";
                message.Response = result;
                message.Success = true;
            }

            return message;
        }

        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<List<Permission>> GetMenuTreeList()
        {
            MessageModel<List<Permission>> message = new MessageModel<List<Permission>>();
            var result = _menuService.GetMenuTreeList();
            if (result != null)
            {
                message.Msg = "查询成功！";
                message.Response = result;
                message.Success = true;
            }

            return message;
        }

        [HttpDelete]
        public MessageModel<string> Delete(int id)
        {
            MessageModel<string> message = new MessageModel<string>();
            var result = _menuService.DeleteMenu(id);
            if (result)
            {
                message.Msg = "删除成功！";
                message.Success = true;
                message.Response = null;
            }

            return message;
        }
    }
}
