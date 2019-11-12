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

        public List<Permission> GetMenuTreeList()
        {
            List<Permission> list = _menuRepository.GetMenuList();

            Func<int, List<Permission>> func = null;
            func = m => {
                List<Permission> t = new List<Permission>();
                foreach (var item in list.Where(h => h.Pid == m && h.IsDelete == false))
                {
                    var childs = func(item.Id);
                    t.Add(new Permission()
                    {
                        Id = item.Id,
                        Pid = item.Pid,
                        Name = item.Name,
                        Path = item.Path,
                        Enabled = item.Enabled,
                        Mid = item.Mid,
                        IsHide = item.IsHide,
                        Icon = item.Icon,
                        IsButton = item.IsButton,
                        OrderSort = item.OrderSort,
                        CreateTime = item.CreateTime,
                        Children = childs
                    });
                }
                return t;
            };
            return func(0);
        }

        public List<MenuDto> GetSidebarMenuTree()
        {
            List<Permission> list = _menuRepository.GetMenuList();

            Func<int, List<MenuDto>> func = null;
            func = new Func<int, List<MenuDto>>(m => {
                List<MenuDto> t = new List<MenuDto>();
                foreach (var item in list.Where(h => h.Pid == m && h.IsDelete == false && h.IsButton == false))
                {
                    var childs = func(item.Id);
                    t.Add(new MenuDto()
                    {
                        Id = item.Id,
                        Pid = item.Pid,
                        Path = item.Path,
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

        /// <summary>
        /// 删除菜单，注意：如果删除的父级菜单，则子级菜单都会被删除，如果删除子级菜单，则父级不删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMenu(int id)
        {
            // TODO：菜单删除逻辑有问题
            var result1 = BaseDal.Query(a => a.Id == id).Result.FirstOrDefault();
            if (result1 == null)
            {
                return false;
            }
            var result2 = BaseDal.Query(a => a.Pid == id).Result;
            var list = new List<Permission>();
            result2.Add(result1);
            foreach (var value in result2)
            {
                value.IsDelete = true;
                list.Add(value);
            }
            
            return BaseDal.Update(list).Result;
        }
    }
}
