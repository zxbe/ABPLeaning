using System;
using Volo.Abp.Domain.Entities.Auditing;
using Wallee.BookStore.Enums;

namespace Wallee.BookStore.Entities
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public BookType Type { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }
    }
}
