using Autofac;
using CoreNotes.AutoFac.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System;
using CoreNotes.AutoFac.CoreApi.Filters;
using CoreNotes.AutoFac.CoreApi.Middleware;
using Serilog;

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


        // ������ʱ�����ã�ʹ�ø÷���ע����������У�ʹ��DIע�룩
        public void ConfigureServices(IServiceCollection services)
        {
            /* ʹ��΢�����õ�DI
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            */
            // ��һ�֣��Զ��������������ȫ���쳣
            services.AddMvc(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
            });
            services.AddControllers();

            #region Swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.1.0",
                    Title = "CoreNotes.Autofac.CoreApi",
                    Description = "Api Server",
                    Contact = new OpenApiContact { Name = "jinjupeng", Email = "2365697576@qq.com", Url = new Uri("https://github.com/jinjupeng") }
                });
            });
            #endregion
        }

        #region AutoFac��DIʵ��

        // This is the default if you don't have an environment specific method.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add things to the Autofac ContainerBuilder.
            builder.RegisterModule(new AutofacModule());
        }
        #endregion

        // �÷���������ʱ�����ã�ͨ���÷�������HTTP����ܵ�
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            #region Swagger
            // �����м����������Swagger��ΪJSON���ս��
            app.UseSwagger();
            // �����м�������swagger ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                // http://localhost:<port>/
                c.RoutePrefix = string.Empty;
            });
            #endregion
            // �ڶ��֣��Զ����м��ExceptionMiddleware������ܵ����ڲ���ȫ���쳣
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseStaticFiles(); // ���ʾ�̬�ļ��м��

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
