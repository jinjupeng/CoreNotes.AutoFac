using System;
using SqlSugar;

namespace CoreNotes.AutoFac.Model.Models
{
    public class RoleModulePermission
    {
        [SugarColumn(IsPrimaryKey = true, IsNullable = true, IsIdentity = true)]
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public int RoleId { get; set; }
        public int? ModuleId { get; set; }
        public int PermissionId { get; set; }
        public int CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public int ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}