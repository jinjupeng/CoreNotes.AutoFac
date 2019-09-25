using CoreNotes.AutoFac.Model;

namespace CoreNotes.AutoFac.IRepository
{
    /// <summary>
    /// 学生仓储接口
    /// </summary>
    public interface IStudentRepository : IBaseRepository<StudentEntity>
    {
        string GetName(long id);
    }
}
