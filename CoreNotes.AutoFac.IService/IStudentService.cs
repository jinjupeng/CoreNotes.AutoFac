using System;

namespace CoreNotes.AutoFac.IService
{
    /// <summary>
    /// 学生逻辑处理interface
    /// </summary>
    public interface IStudentService
    {
        string GetStuName(long id);
    }
}
