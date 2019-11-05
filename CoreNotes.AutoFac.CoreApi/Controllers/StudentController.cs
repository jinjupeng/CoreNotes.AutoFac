using System;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

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

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Student>> Get(string id)
        {
            var model = await _studentService.QueryById(id);
            MessageModel<Student> message = new MessageModel<Student>
            {
                Msg = "获取成功！",
                Success = true,
                Response = model
            };
            return message;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Student>>> GetList()
        {
            var data = await _studentService.QueryPage(a => a.IsDelete == 0);
            MessageModel<PageModel<Student>> message = new MessageModel<PageModel<Student>>
            { 
                Msg = "获取成功！",
                Success = data.DataCount > 0,
                Response = data
            };
            return message;
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Add(Student student)
        {
            var data = new MessageModel<string>();
            student.Id = Guid.NewGuid().ToString("N");
            student.IsDelete = 0;
            student.CreateDate = DateTime.Now;
            

            var result = await _studentService.Add(student);
            data.Success = result > 0;
            if (data.Success)
            {
                data.Response = result.ObjToString();
                data.Msg = "添加成功";
            }
            return data;
        }

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Edit(Student student)
        {
            var data = new MessageModel<string>();
            student.UpdateDate = DateTime.Now;

            var result = await _studentService.Update(student);
            data.Success = result;
            if (data.Success)
            {
                data.Response = student.Id;
                data.Msg = "添加成功";
            }
            return data;
        }
    }
}
