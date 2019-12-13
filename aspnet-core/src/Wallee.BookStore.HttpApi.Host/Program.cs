using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Wallee.BookStore
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            try
            {
                Log.Information("Starting Wallee.BookStore.HttpApi.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                /*
                  ①var containerBuilder=new ContainerBuilder();
                  ②services.AddObjectAccessor<ContainerBuilder>(containerBuilder)).UseServiceProviderFactory<ContainerBuilder>((IServiceProviderFactory<ContainerBuilder>) new AbpAutofacServiceProviderFactory(containerBuilder));
                    AddObjectAccessor()扩展方以单例的形式法将ObjectAccessor<T>注册到IServiceCollection中，并且插入顶部，便于检索
                    UseServiceProviderFactory()以对象型适配器模式用AbpAutofacServiceProviderFactory<ContainerBuilder>初始化一个ServiceFactoryAdapter<TContainerBuilder>,后者是HostBuilder里面的一个属性。
                */
                .UseAutofac()
                .UseSerilog();
    }
}
