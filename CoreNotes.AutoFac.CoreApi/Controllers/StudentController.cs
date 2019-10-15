using CoreNotes.AutoFac.IService;
using Microsoft.AspNetCore.Mvc;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    /// <summary>
    /// 学生模块接口
    /// </summary>
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Route("Student/GetStuNameById")]
        public string GetStuNameById(long id)
        {
            return _studentService.GetStuName(id);
        }
    }
}
