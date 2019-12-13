using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Wallee.BookStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //注入abp相关服务
            /*
            注解：AbpApplicationFactory.Create<TStartUpModule>(services, optionsAction)得到一个AbpApplicationWithExternalServiceProvider，继承自IAbpApplication和AbpApplicationBase
                在该类构造函数中，以单例方式将自己注入到DI中：services.AddSingleton<IAbpApplicationWithExternalServiceProvider>(this);
                在基类AbpApplicationBase中做了很多事情：
                ①：检查services:IServiceCollection，和startUpModule:Type不能为空
                ②：从构造函数中传入的参数给StartUpModule和Services属性赋值
                ③：添加一个空的IObjectAccessor<IServiceProvider>,它的value为空。
                ④：初始化一个AbpApplicationCreationOptions，并作为构造函数传入的optionsAction：Action<AbpApplicationCreationOtion>的参数，执行这个传入的Action。
                ⑤：将本身与IAbpApplication和IModuleContainer绑定作为单例注入IServiceCollection中。IAbpApplication继承自IModuleContainer。
                ⑥：执行services.AddCoreServices()扩展方法，该方法执行了AddOptions、AddLogging和AddLocalization
                ⑦：执行services.AddCoreAbpServices()扩展方法，该方法需要两个参数：IAbpApplication和AbpApplicationCreationOptions，它执行了如下逻辑：
                   ①：初始化了三个对象：ModuleLoader、AssemeblyFinder（构造该类需要一个IAbpApplication类型的参数）和TypeFinder(构造该类型需要一个AssemeblyFinder类型的参数)。
                   ②：检查IServiceCollection当中是否注册了IConfiguration，如果没有注册，则使用services.ReplaceConfiguration这个扩展方法来注入一个，这个方法内部使用ConfigurationHelper.BuildConfiguration(applicationCreationOptions.Configuration)
                   ③：将刚才新建的三个对象IModuleLoader<==>ModuleLoader、IAssemeblyFinder<==>AssemeblyFinder和ITypeFinder<==>TypeFinder以单例的形式注入容器
                   ④：执行services.AddAssemblyOf<IAbpApplication>()
                   ⑤：执行Services.Configure<AbpModuleLifeCycleOptions>()方法，方法内部将四个Contributor添加到了AbpModuleLifeCycleOptions的Contributors上面，Contributors是一个自定义ITypeList<IModuleLifecycleContributor>类型的属性。
                ⑧：执行LoadModules方法，返回的结果赋值给Modules属性，Modules属性的类型是IReadOnlyList<IAbpModuleDescriptor>。
             */
            Console.WriteLine("start to configure service");
            services.AddApplication<BookStoreHttpApiHostModule>(option =>
            {

            });
            Console.WriteLine("end to configure services");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}
