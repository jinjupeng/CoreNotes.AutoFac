using CoreNotes.AutoFac.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.DTO;
using CoreNotes.AutoFac.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    [Authorize("Permission")]
    [Route("[controller]/[action]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // TODO：应该根据当前登录用户的角色来显示不同的菜单树
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

        // TODO：应该根据当前登录用户的角色来显示不同的菜单树
        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
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

        // TODO：应该根据当前登录用户的角色来显示不同的菜单树
        /// <summary>
        /// 获取侧边菜单树（不包含按钮）
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public MessageModel<List<MenuDto>> GetSidebarMenuTree()
        {
            MessageModel<List<MenuDto>> message = new MessageModel<List<MenuDto>>();
            var result = _menuService.GetSidebarMenuTree();
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
