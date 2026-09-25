using System;
using example_csharp_app.Models.Request;
using example_csharp_app.Models.Response;

namespace example_csharp_app.Interfaces;

public interface IBookAggregator
{
    Task<BookListResponse> GetBookListAsync(BookFilterRequest request);
    Task<BookSingleResponse> GetBookAsync(Guid bookId);
    Task<BookOperationResponse> UpdateBookAsync(BookUpdateRequest request);
    Task<BookOperationResponse> AddBookAsync(BookCreateRequest request);
    Task<BookOperationResponse> RemoveBookAsync(Guid bookId);
}
