using System;
using System.Threading.Tasks;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Model;
using CoreNotes.AutoFac.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace CoreNotes.AutoFac.CoreApi.Controllers
{
    /// <summary>
    /// 学生模块接口
    /// </summary>
    //[Authorize("Permission")]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
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
        public async Task<MessageModel<PageModel<Student>>> GetList(int pageIndex, int pageSize)
        {
            var data = await _studentService.QueryPage(a => a.IsDelete == false, pageIndex, pageSize);
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
            if (student != null)
            {
                student.Id = Guid.NewGuid().ToString("N");
                student.IsDelete = false;
                student.CreateDate = DateTime.Now;


                var result = await _studentService.Add(student);
                data.Success = result > 0;
                if (!data.Success) return data;
                data.Response = result.ObjToString();
            }

            data.Msg = "添加成功";
            // TODO：添加日志到数据库
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
            if (student != null)
            {
                student.UpdateDate = DateTime.Now;

                var result = await _studentService.Update(student);
                data.Success = result;
                if (!data.Success) return data;
                data.Response = student.Id;
                data.Msg = "添加成功";
            }

            // TODO：添加日志到数据库
            return data;
        }
        /// <summary>
        /// 新增或更新学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> CreateOrUpdate(Student student)
        {
            var data = new MessageModel<string>();
            
            if (string.IsNullOrWhiteSpace(student.Id))
            {
                student.Id = Guid.NewGuid().ToString("N");
                student.IsDelete = false;
                student.CreateDate = DateTime.Now;
                var result = await _studentService.Add(student);
                data.Success = result > 0;
                if (data.Success)
                {
                    data.Response = result.ObjToString();
                    data.Msg = "添加成功";
                }
                else
                {
                    data.Msg = "添加失败";
                }
            }
            else
            {
                var result = await _studentService.Update(student);
                data.Success = result;
                if (data.Success)
                {
                    data.Response = student.Id;
                    data.Msg = "添加成功";
                }
                else
                {
                    data.Msg = "添加失败";
                }
            }
            // TODO：添加日志到数据库
            return data;
        }

        /// <summary>
        /// 删除单条用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(string id)
        {
            var data = new MessageModel<string>();
            if (string.IsNullOrWhiteSpace(id))
            {
                var studentDetail = await _studentService.QueryById(id);
                studentDetail.IsDelete = true;
                data.Success = await _studentService.Update(studentDetail);
                if (data.Success)
                {
                    data.Msg = "删除成功";
                    data.Response = studentDetail?.Id.ObjToString();
                }
            }

            return data;
        }

        // TODO：多条删除
    }
}
