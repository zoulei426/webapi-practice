using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace YuLinTu.Practice.Books
{
    public class BookRepositoryTests : PracticeDomainTestBase
    {
        private readonly IRepository<Book, Guid> bookRepository;

        public BookRepositoryTests()
        {
            bookRepository = GetRequiredService<IRepository<Book, Guid>>();
        }

        [Fact]
        public async Task Should_Create_A_Book()
        {
            var book = new Book
            {
                Name = "大话设计模式",
                Type = BookType.Science,
                PublishDate = new DateTime(2007, 12, 1)
            };

            var result = await bookRepository.InsertAsync(book, true);

            result.Id.ShouldNotBe(Guid.Empty);
        }
    }
}