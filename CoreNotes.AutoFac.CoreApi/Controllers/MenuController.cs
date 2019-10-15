using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _MenuService;
        public MenuController(IMenuService MenuService)
        {
            _MenuService = MenuService;
        }

        [Route("Menu/GetMenuTree")]
        public List<MenuEntity> GetMenuTree()
        {
            var result = _MenuService.GetMenuTree();
            return result;
        }
    }
}
