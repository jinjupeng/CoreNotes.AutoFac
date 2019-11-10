using CoreNotes.AutoFac.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.DTO;

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
    }
}
