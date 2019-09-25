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
        Transient�� Transient������ÿ������ʱ������������ñ�������������״̬������Repository��ApplicationService����
        Scoped�� Scoped ������ÿ������ʱ���������������ں������������ͬһ��Scope��ֻ��ʼ��һ��ʵ�� ���������Ϊ�� ÿһ��request����ֻ����һ��ʵ����ͬһ��http request����һ�� scope�ڣ�
        Singleton ������Ӧ�ó���������������ֻ����һ��ʵ�� 
        https://juejin.im/post/5d6736fff265da03c128abca .net core �޴����ڵ�����ע��
        https://cloud.tencent.com/developer/article/1023209   ASP.NET Core����ע����&ʹ��Autofac���ʵ��

        .net core ��DI�ĺ���
        IServiceCollection ����ע��
        // IServiceProvider �����ṩʵ��
        */
        #region ΢��ٷ��Դ���DI

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddScoped<IStudentService, StudentService>();
            // services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddControllers();
        }


        #endregion

        #region AutoFac��ʵ��
        /*
         * ����д����.net 3.0�ᱨ��ConfigureServices returning an System.IServiceProvider isn't supported.
         * https://github.com/aspnet/AspNetCore.Docs/issues/11441
         * https://stackoverflow.com/questions/56385277/configure-autofac-in-asp-net-core-3-0-preview-5-or-higher
         * https://stackoverflow.com/questions/37063652/autofac-module-registrations
                public IServiceProvider ConfigureServices(IServiceCollection services)
                {
                    var builder = new ContainerBuilder();

                    // ע������д��
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
