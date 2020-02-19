using Autofac;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.IService.Base;
using CoreNotes.AutoFac.Repository;
using CoreNotes.AutoFac.Service;

namespace CoreNotes.AutoFac.Ioc
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region 命名注册

            // builder.RegisterType<StudentService>().Named<IStudentService>("studentService1");
            // builder.RegisterType<StudentService>().Named<IStudentService>("studentService2");
            #endregion

            #region 属性注册
            // Autofac默认RegisterType注册的对象是瞬时的生命周期
            // 默认是瞬时的，也就是
            // builder.RegisterType<Service>().InstancePerDependency();

            // 单例：
            // SingleInstance()

            // 作用域Scope：
            // InstancePerLifetimeScope()

            builder.RegisterType<StudentService>().As<IStudentService>().SingleInstance();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().SingleInstance();

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();

            builder.RegisterType<RoleService>().As<IRoleService>().SingleInstance();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().SingleInstance();

            builder.RegisterType<UserRoleService>().As<IUserRoleService>().SingleInstance();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().SingleInstance();

            builder.RegisterType<PermissionService>().As<IPermissionService>().SingleInstance();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>().SingleInstance();

            builder.RegisterType<ModuleService>().As<IModuleService>().SingleInstance();
            builder.RegisterType<ModuleRepository>().As<IModuleRepository>().SingleInstance();

            builder.RegisterType<ModulePermissionService>().As<IModulePermissionService>().SingleInstance();
            builder.RegisterType<ModulePermissionRepository>().As<IModulePermissionRepository>().SingleInstance();

            builder.RegisterType<RoleModulePermissionService>().As<IRoleModulePermissionService>().SingleInstance();
            builder.RegisterType<RoleModulePermissionRepository>().As<IRoleModulePermissionRepository>().SingleInstance();

            builder.RegisterType<MenuService>().As<IMenuService>().SingleInstance();
            builder.RegisterType<MenuRepository>().As<IMenuRepository>().SingleInstance();

            #endregion

            #region AOP


            #endregion

        }
    }
}
