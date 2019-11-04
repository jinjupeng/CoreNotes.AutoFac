using CoreNotes.AutoFac.IService.Base;
using CoreNotes.AutoFac.Model.Models;

namespace CoreNotes.AutoFac.IService
{
    /// <summary>
    /// 学生逻辑处理interface
    /// </summary>
    public interface IStudentService: IBaseService<Student>
    {

        string GetStuName(string id);

    }

}
