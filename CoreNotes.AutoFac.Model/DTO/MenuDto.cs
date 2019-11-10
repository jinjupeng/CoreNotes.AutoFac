using System.Collections.Generic;

namespace CoreNotes.AutoFac.Model.DTO
{
    /// <summary>
    /// 菜单树
    /// </summary>
    public class MenuDto
    {
        public int Id { get; set; } // public int Value { get; set; }
        public int Pid { get; set; } // 父节点
        public string Label { get; set; }
        public int Order { get; set; }
        public bool IsBtn { get; set; }
        public bool Disabled { get; set; }
        public List<MenuDto> Children { get; set; }
        public List<MenuDto> Btns { get; set; }
    }
}