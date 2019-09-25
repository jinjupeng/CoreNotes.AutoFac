using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CoreNotes.AutoFac.CoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // ��һ�֣�ʹ���Դ�DI
                webBuilder.UseStartup<Startup>();
                // �ڶ��֣����AutoFac��Ϊ��������
                // �����֣����AutoFac�ӹ�����ע��
            });
    }
}
