using Volo.Abp.Modularity;

namespace Wallee.BookStore
{
    [DependsOn(
        typeof(BookStoreApplicationModule),
        typeof(BookStoreDomainTestModule)
        )]
    public class BookStoreApplicationTestModule : AbpModule
    {

    }
}