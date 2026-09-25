using System;
using example_csharp_app.Dto;
using example_csharp_app.Models.Request;

namespace example_csharp_app.Interfaces;

public interface IBookRepository
{
    Task<int> AddBookAsync(BookDto dto);
    Task<int> RemoveBookAsync(BookDto dto);
    Task<BookDto?> GetBookAsync(Guid bookId, bool enableEdit = true);
    Task<IReadOnlyCollection<BookDto>> GetBookListAsync(BookFilterRequest request);
    Task<int> UpdateBookAsync(BookDto dto);
}
