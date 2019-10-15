using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;

namespace CoreNotes.AutoFac.Service
{
    /// <summary>
    /// 学生逻辑处理
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="studentRepository"></param>
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public string GetStuName(long id)
        {
            // 模拟从数据库获取数据，然后处理数据
            var stu = _studentRepository.Get(id);
            return stu.Name;
        }
    }
}
