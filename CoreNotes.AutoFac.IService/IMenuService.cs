using CoreNotes.AutoFac.Model;
using System.Collections.Generic;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IService
{
    public interface IMenuService
    {
        List<MenuEntity> GetMenuTree();
    }
}
