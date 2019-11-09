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

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();

            builder.RegisterType<RoleService>().As<IRoleService>().SingleInstance();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().SingleInstance();

            builder.RegisterType<UserRoleService>().As<IUserRoleService>().SingleInstance();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().SingleInstance();

            builder.RegisterType<MenuService>().As<IMenuService>().SingleInstance();
            builder.RegisterType<MenuRepository>().As<IMenuRepository>().SingleInstance();
        }
    }
}
