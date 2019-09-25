using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.Model;

namespace CoreNotes.AutoFac.Repository
{
    /// <summary>
    /// 学生仓储
    /// </summary>
    public class StudentRepository : BaseRepository<StudentEntity>, IStudentRepository
    {
        public string GetName(long id)
        {
            return "学生张三"; // 假设返回数据库数据
        }
    }
}
