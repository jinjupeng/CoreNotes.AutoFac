using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace CoreNotes.AutoFac.CoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ConfigurationBuilder是配置文件的核心
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // 配置Serilog
            Log.Logger = new LoggerConfiguration()
                // 读取配置文件
                .ReadFrom.Configuration(configuration)
                // 最小的日志输出级别
                // .MinimumLevel.Information()
                // 配置日志输出到控制台
                .MinimumLevel.Debug()
                // 日志调用类命名空间如果以"Microsoft"开头，覆盖日志输出最小级别为Information
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                //.Enrich.FromLogContext()
                .WriteTo.Console()
                // 创建logger
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // 注册第三方容器的入口
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
