using System;
using SqlSugar;

namespace CoreNotes.AutoFac.Model.Models
{
	public class Permission
	{
		[SugarColumn(IsPrimaryKey = true, IsNullable = true, IsIdentity = true)]
		public int Id { get; set; }
		public string Path { get; set; }
		public string Name { get; set; }
		public bool IsButton { get; set; }
		public bool IsHide { get; set; }
		public int Pid { get; set; }
		public int Mid { get; set; }
		public int OrderSort { get; set; }
		public string Icon { get; set; }
		public string Description { get; set; }
		public bool Enabled { get; set; }
		public int CreateId { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateTime { get; set; }
		public int ModifyId { get; set; }
		public string ModifyBy { get; set; }
		public DateTime ModifyTime { get; set; }
		public bool IsDelete { get; set; }
	}
}