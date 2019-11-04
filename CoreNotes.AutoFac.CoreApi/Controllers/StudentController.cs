using System;
using CoreNotes.AutoFac.IService;
using Microsoft.AspNetCore.Mvc;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    /// <summary>
    /// 学生模块接口
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult GetStuNameById(/* string id */)
        {
            string id = "236d37d4f3c14dd489b1c7270c7670ad";
            throw new Exception("出错了");
			return Ok(_studentService.GetStuName(id));
        }
    }
}
