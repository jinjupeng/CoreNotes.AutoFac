using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CoreNotes.AutoFac.Model
{
    public class MenuEntity
    {
        public Hashtable item { get; set; }
        public List<MenuEntity> children { get; set; }
    }
}
