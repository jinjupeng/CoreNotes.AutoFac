using CoreNotes.AutoFac.IRepository;
using System.Collections.Generic;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Repository.Base;

namespace CoreNotes.AutoFac.Repository
{
    public class MenuRepository : BaseRepository<Permission>, IMenuRepository
    {
        public List<Permission> GetMenuList()
        {
            return Db.Queryable<Permission>().ToList();
        }

    }
}
