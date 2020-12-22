using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace YuLinTu.Practice.Books
{
    public class BookAppServiceTests : PracticeApplicationTestBase
    {
        private readonly IBookAppService bookAppService;

        public BookAppServiceTests()
        {
            bookAppService = GetRequiredService<IBookAppService>();
        }

        [Fact]
        public async Task Should_Create_A_Book()
        {
            var book = new CreateUpdateBookDto
            {
                Name = "大话设计模式",
                Type = BookType.Science,
                PublishDate = new DateTime(2007, 12, 1)
            };

            var result = await bookAppService.CreateAsync(book);

            result.Id.ShouldNotBe(Guid.Empty);
        }
    }
}