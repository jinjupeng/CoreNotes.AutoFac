using CoreNotes.AutoFac.Model;
using System.Collections;
using System.Collections.Generic;

namespace CoreNotes.AutoFac.IRepository
{
    public interface IMenuRepository
    {
        List<Hashtable> GetMenuList();
    }
}
