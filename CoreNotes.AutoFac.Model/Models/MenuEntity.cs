using System.Collections;
using System.Collections.Generic;

namespace CoreNotes.AutoFac.Model.Models
{
    public class MenuEntity
    {
        public Hashtable item { get; set; }
        public List<MenuEntity> children { get; set; }
    }
}
