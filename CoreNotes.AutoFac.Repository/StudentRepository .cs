using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Repository.Base;

namespace CoreNotes.AutoFac.Repository
{
    /// <summary>
    /// 学生仓储
    /// </summary>
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public string GetName(string id)
        {
            return "学生张三"; // 假设返回数据库数据
        }

    }
}
