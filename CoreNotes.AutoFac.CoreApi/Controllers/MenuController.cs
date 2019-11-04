using CoreNotes.AutoFac.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public ActionResult<List<MenuEntity>> GetMenuTree()
        {
            return _menuService.GetMenuTree();
        }
    }
}
