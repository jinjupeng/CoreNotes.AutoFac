using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using CoreNotes.AutoFac.Model.DTO;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Service.Base;

namespace CoreNotes.AutoFac.Service
{
    public class MenuService : BaseService<Permission>, IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="menuRepository"></param>
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }


        /// <summary>
        /// Func委托实现生成菜单树
        /// </summary>
        /// <returns></returns>
        public List<MenuDto> GetMenuTree()
        {
            List<Permission> list = _menuRepository.GetMenuList();

            Func<int, List<MenuDto>> func = null;
            func = new Func<int, List<MenuDto>>(m => {
                List<MenuDto> t = new List<MenuDto>();
                foreach (var item in list.Where(h => h.Pid == m && h.IsDelete == false))
                {
                    var childs = func(item.Id);
                    t.Add(new MenuDto()
                    {
                        // Item = item,
                        Id = item.Id,
                        Pid = item.Pid,
                        Label = item.Name,
                        IsBtn = item.IsButton,
                        Order = item.OrderSort,
                        Children = childs
                    });
                }
                return t;
            });
            return func(0);
        }
    }
}
