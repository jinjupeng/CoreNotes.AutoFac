using System.Collections.Generic;
using CoreNotes.AutoFac.IService.Base;
using CoreNotes.AutoFac.Model.DTO;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IService
{
    public interface IMenuService: IBaseService<Permission>
    {
        List<MenuDto> GetMenuTree();

        List<Permission> GetMenuTreeList();

        bool DeleteMenu(int id);
    }
}
