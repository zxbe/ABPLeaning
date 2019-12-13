using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users;
using Volo.Abp.Users.EntityFrameworkCore;
using Wallee.BookStore.Entities;
using Wallee.BookStore.Users;

namespace Wallee.BookStore.EntityFrameworkCore
{
    public static class BookStoreDbContextModelCreatingExtensions
    {
        public static void ConfigureBookStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable("AbpUsers"); //Sharing the same table "AbpUsers" with the IdentityUser
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                //Moved customization to a method so we can share it with the BookStoreMigrationsDbContext class
                b.ConfigureCustomUserProperties();
            });
            builder.Entity<Book>(b =>
            {
                b.ToTable("Books");
                b.ConfigureByConvention();
                b.ConfigureBookProperties();
            });
            /* Configure your own tables/entities inside the ConfigureBookStore method */
            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(BookStoreConsts.DbTablePrefix + "YourEntities", BookStoreConsts.DbSchema);

            //    //...
            //});
        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> b)
            where TUser : class, IUser
        {
            //b.Property<string>(nameof(AppUser.MyProperty))...
        }
        public static void ConfigureBookProperties<TBook>(this EntityTypeBuilder<TBook> b)
            where TBook : Book
        {
            b.Property(it => it.Name).IsRequired().HasMaxLength(100);
        }
    }
}