using CoreNotes.AutoFac.IRepository.Base;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IRepository
{
    /// <summary>
    /// 学生仓储接口
    /// </summary>
    public interface IStudentRepository : IBaseRepository<Student>
    {
        string GetName(string id);
    }
}
