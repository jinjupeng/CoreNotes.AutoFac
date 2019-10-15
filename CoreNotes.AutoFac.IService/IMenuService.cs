using CoreNotes.AutoFac.Model;
using System.Collections.Generic;

namespace CoreNotes.AutoFac.IService
{
    public interface IMenuService
    {
        List<MenuEntity> GetMenuTree();
    }
}
