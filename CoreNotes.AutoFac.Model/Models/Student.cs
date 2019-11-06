using System;
using SqlSugar;

namespace CoreNotes.AutoFac.Model.Models
{
    /// <summary>
    /// 学生实体
    /// </summary>
    public class Student
    {
        [SugarColumn(IsPrimaryKey = true, IsNullable = true, IsIdentity = false)]
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 成绩
        /// </summary>
        public int Grade { get; set; }

        public int Age { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
    }
}
