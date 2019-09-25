using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreNotes.AutoFac.Ioc;
using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Repository;
using CoreNotes.AutoFac.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CoreNotes.AutoFac.CoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        /*
        Transient： Transient服务在每次请求时被创建，它最好被用于轻量级无状态服务（如Repository和ApplicationService服务）
        Scoped： Scoped 服务在每次请求时被创建，生命周期横贯整次请求；在同一个Scope内只初始化一个实例 ，可以理解为（ 每一个request级别只创建一个实例，同一个http request会在一个 scope内）
        Singleton ：整个应用程序生命周期以内只创建一个实例 
        https://juejin.im/post/5d6736fff265da03c128abca .net core 无处不在的依赖注入
        https://cloud.tencent.com/developer/article/1023209   ASP.NET Core依赖注入解读&使用Autofac替代实现

        .net core 中DI的核心
        IServiceCollection 负责注册
        // IServiceProvider 负责提供实例
        */
        #region 微软官方自带的DI

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddScoped<IStudentService, StudentService>();
            // services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddControllers();
        }


        #endregion

        #region AutoFac的实现
        /*
         * 这种写法在.net 3.0会报错：ConfigureServices returning an System.IServiceProvider isn't supported.
         * https://github.com/aspnet/AspNetCore.Docs/issues/11441
         * https://stackoverflow.com/questions/56385277/configure-autofac-in-asp-net-core-3-0-preview-5-or-higher
         * https://stackoverflow.com/questions/37063652/autofac-module-registrations
                public IServiceProvider ConfigureServices(IServiceCollection services)
                {
                    var builder = new ContainerBuilder();

                    // 注意以下写法
                    builder.RegisterType<StudentService>().As<IStudentService>();
                    builder.RegisterType<StudentRepository>().As<IStudentRepository>();

                    builder.Populate(services);
                    this.ApplicationContainer = builder.Build();

                    return new AutofacServiceProvider(this.ApplicationContainer);
                }*/

        // This is the default if you don't have an environment specific method.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add things to the Autofac ContainerBuilder.
            builder.RegisterModule(new AutofacModule());
        }
        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
