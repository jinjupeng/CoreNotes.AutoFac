using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    /// <summary>
    /// 学生模块接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult GetName(/* string id */)
        {
            string id = "236d37d4f3c14dd489b1c7270c7670ad";
            // throw new Exception("出错了");
            MessageModel<string> message = new MessageModel<string>
            {
                Msg = "获取成功！",
                Success = true,
                Response = _studentService.GetStuName(id)
            };
            return Ok(message);
        }
    }
}
