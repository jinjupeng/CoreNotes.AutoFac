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

        #region ΢��ٷ��Դ���DI
        /*


        Transient�� ÿһ��GetService���ᴴ��һ���µ�ʵ��
        Scoped��  ��ͬһ��Scope��ֻ��ʼ��һ��ʵ�� ���������Ϊ�� ÿһ��request����ֻ����һ��ʵ����ͬһ��http request����һ�� scope�ڣ�
        Singleton ������Ӧ�ó���������������ֻ����һ��ʵ�� 
        https://juejin.im/post/5d6736fff265da03c128abca .net core �޴����ڵ�����ע��
        https://cloud.tencent.com/developer/article/1023209   ASP.NET Core����ע����&ʹ��Autofac���ʵ��
        */
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   // ServiceCollection ����ע��

            // IServiceProvider �����ṩʵ��


            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddControllers();
        }
        #endregion

        #region AutoFac��ʵ��

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
