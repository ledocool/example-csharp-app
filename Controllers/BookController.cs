using example_csharp_app.Interfaces;
using example_csharp_app.Models.Request;
using example_csharp_app.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace example_csharp_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookAggregator _bookAggregator;

        public BookController(IBookAggregator bookAggregator)
        {
            _bookAggregator = bookAggregator;
        }

        [SwaggerResponse(200, "Получить список книг", typeof(BookListResponse))]
        [HttpGet()]
        public async Task<IActionResult> GetList([FromQuery]BookFilterRequest request)
        {
            var bookList = await _bookAggregator.GetBookListAsync(request);
            return Ok(bookList);
        }

        [SwaggerResponse(200, "Получить список книг", typeof(BookSingleResponse))]
        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBook([FromRoute] Guid bookId)
        {
            var result = await _bookAggregator.GetBookAsync(bookId);
            return Ok(result);
        }

        [SwaggerResponse(200, "Добавить книгу", typeof(BookOperationResponse))]
        [HttpPost()]
        public async Task<IActionResult> AddBook([FromBody]BookCreateRequest request)
        {
            var result = await _bookAggregator.AddBookAsync(request);
            return Ok(result);
        }

        [SwaggerResponse(200, "Обновить книгу", typeof(BookOperationResponse))]
        [HttpPut()]
        public async Task<IActionResult> UpdateBook([FromBody]BookUpdateRequest request)
        {
            var result = await _bookAggregator.UpdateBookAsync(request);
            return Ok(result);
        }

        [SwaggerResponse(200, "Удалить книгу", typeof(BookOperationResponse))]
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> RemoveBook([FromRoute]Guid bookId)
        {
            var result = await _bookAggregator.RemoveBookAsync(bookId);
            return Ok(result);
        }
    }
}
