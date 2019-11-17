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

        // 请注意，下边三个实体参数，只是做传参作用，所以忽略下，不然会认为缺少字段
        [SugarColumn(IsIgnore = true)]
        public virtual Role Role { get; set; }

        [SugarColumn(IsIgnore = true)]
        public virtual Module Module { get; set; }

        [SugarColumn(IsIgnore = true)]
        public virtual Permission Permission { get; set; }
    }
}