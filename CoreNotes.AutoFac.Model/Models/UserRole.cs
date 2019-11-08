using System;
using SqlSugar;

namespace CoreNotes.AutoFac.Model.Models
{
    public class UserRole
    {
        [SugarColumn(IsPrimaryKey = true, IsNullable = true, IsIdentity = true)]
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public int ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}