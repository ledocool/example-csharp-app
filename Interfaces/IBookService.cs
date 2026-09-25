using System;
using example_csharp_app.Models.Model;
using example_csharp_app.Models.Request;

namespace example_csharp_app.Interfaces;

public interface IBookService
{
    Task<IReadOnlyCollection<BookModel>> GetBooksAsync(BookFilterRequest request);
    Task<BookModel?> GetBookAsync(Guid bookId);
    Task<int> UpdateBookAsync(Guid bookId, BookUpdateModel model);
    Task<int> AddBookAsync(BookCreateModel model);
    Task<int> RemoveBookAsync(Guid bookId);
}
