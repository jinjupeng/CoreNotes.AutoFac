using System;
using SqlSugar;

namespace CoreNotes.AutoFac.Model.Models
{
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsNullable = true, IsIdentity = true)]
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Pwd { get; set; }
        public string RealName { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime LastErrTime { get; set; }
        public int ErrorCount { get; set; }
        public int Sex { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        public bool IsDelete { get; set; }

        /// <summary>
        /// 用来保存从前端传入的角色id
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int RoleId { get; set; }

        /// <summary>
        /// 用来向前端显示角色名
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string RoleName { get; set; }
    }
}