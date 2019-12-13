using Wallee.BookStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Wallee.BookStore
{
    [DependsOn(
        typeof(BookStoreEntityFrameworkCoreTestModule)
        )]
    public class BookStoreDomainTestModule : AbpModule
    {

    }
}