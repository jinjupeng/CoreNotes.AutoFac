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


        // 在运行时被调用，使用该方法注册服务到容器中（使用DI注入）
        public void ConfigureServices(IServiceCollection services)
        {
            /* 使用微软内置的DI
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            */
            // 第一种：自定义过滤器并捕获全局异常
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

        #region AutoFac的DI实现

        // This is the default if you don't have an environment specific method.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add things to the Autofac ContainerBuilder.
            builder.RegisterModule(new AutofacModule());
        }
        #endregion

        // 该方法在运行时被调用，通过该方法配置HTTP请求管道
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            #region Swagger
            // 启动中间件服务生成Swagger作为JSON的终结点
            app.UseSwagger();
            // 启用中间件服务对swagger ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                // http://localhost:<port>/
                c.RoutePrefix = string.Empty;
            });
            #endregion
            // 第二种：自定义中间件ExceptionMiddleware并加入管道用于捕获全局异常
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseStaticFiles(); // 访问静态文件中间件

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
