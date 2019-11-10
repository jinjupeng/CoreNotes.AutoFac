using System.Collections.Generic;
using CoreNotes.AutoFac.IRepository.Base;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IRepository
{
    public interface IMenuRepository: IBaseRepository<Permission>
    {
        List<Permission> GetMenuList();
    }
}
