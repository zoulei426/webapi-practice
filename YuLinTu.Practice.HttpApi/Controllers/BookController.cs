using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using YuLinTu.Practice.Books;

namespace YuLinTu.Practice.Controllers
{
    [RemoteService]
    [Route("books")]
    public class BookController : AbpController
    {
        private readonly IBookAppService bookAppService;

        public BookController(IBookAppService bookAppService)
        {
            Check.NotNull(bookAppService, nameof(bookAppService));

            this.bookAppService = bookAppService;
        }
    }
}