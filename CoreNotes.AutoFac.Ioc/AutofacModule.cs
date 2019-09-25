using Autofac;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Repository;
using CoreNotes.AutoFac.Service;

namespace CoreNotes.AutoFac.Ioc
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentService>().As<IStudentService>().SingleInstance();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().SingleInstance();
        }
    }
}
