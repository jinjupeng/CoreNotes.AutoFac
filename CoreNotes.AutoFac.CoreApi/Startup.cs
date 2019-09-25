using CoreNotes.AutoFac.IRepository;
using CoreNotes.AutoFac.IService;
using CoreNotes.AutoFac.Repository;
using CoreNotes.AutoFac.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreNotes.AutoFac.CoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region 微软官方自带的DI
        /*


        Transient： 每一次GetService都会创建一个新的实例
        Scoped：  在同一个Scope内只初始化一个实例 ，可以理解为（ 每一个request级别只创建一个实例，同一个http request会在一个 scope内）
        Singleton ：整个应用程序生命周期以内只创建一个实例 
        https://juejin.im/post/5d6736fff265da03c128abca .net core 无处不在的依赖注入
        https://cloud.tencent.com/developer/article/1023209   ASP.NET Core依赖注入解读&使用Autofac替代实现
        */
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   // ServiceCollection 负责注册

            // IServiceProvider 负责提供实例


            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddControllers();
        }
        #endregion

        #region AutoFac的实现

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
