using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.Service
{
    public class MenuService : IMenuService
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
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<MenuEntity> GetMenuTree()
        {
            // 模拟从数据库获取菜单List
            List<Hashtable> list = _menuRepository.GetMenuList();

            Func<int, List<MenuEntity>> func = null;
            func = new Func<int, List<MenuEntity>>(m => {
                List<MenuEntity> t = new List<MenuEntity>();
                foreach (var item in list.Where(h => h["pid"].ToString() == m.ToString()))
                {
                    var childs = func(Convert.ToInt32(item["id"]));
                    t.Add(new MenuEntity()
                    {
                        item = item,
                        children = childs
                    });
                }
                return t;
            });
            return func(-1);
        }
    }
}
