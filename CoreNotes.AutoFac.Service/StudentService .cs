using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model.Models;
using CoreNotes.AutoFac.Service.Base;

namespace CoreNotes.AutoFac.Service
{
	/// <summary>
	/// 学生逻辑处理
	/// </summary>
	public class StudentService : BaseService<Student>, IStudentService
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

        public string GetStuName(string id)
        {
            var stu = _studentRepository.QueryById(id);
            return stu.Result.Name;
        }

    }
}
