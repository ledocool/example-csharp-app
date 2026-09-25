using System;
using example_csharp_app.Dto;
using example_csharp_app.Interfaces;
using example_csharp_app.Models.Mapper;
using example_csharp_app.Models.Model;
using example_csharp_app.Models.Request;

namespace example_csharp_app.Services;

public class BookService : IBookService
{  
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<int> AddBookAsync(BookCreateModel model)
    {
        var dto = model.MapToDto();
        var result = await _bookRepository.AddBookAsync(dto);
        return result;
    }

    public async Task<BookModel?> GetBookAsync(Guid bookId)
    {
        var result = await _bookRepository.GetBookAsync(bookId, enableEdit: false);
        return result?.MapToModel();
    }

    public async Task<IReadOnlyCollection<BookModel>> GetBooksAsync(BookFilterRequest request)
    {
        var result = await _bookRepository.GetBookListAsync(request);
        return result.Select(x => x.MapToModel()).ToList();
    }

    public async Task<int> RemoveBookAsync(Guid bookId)
    {
        var book = await GetBookForEdit(bookId);
        return await _bookRepository.RemoveBookAsync(book);
    }

    public async Task<int> UpdateBookAsync(Guid bookId, BookUpdateModel model)
    {
        var book = await GetBookForEdit(bookId);
        
        book.Author = model.Author;
        book.BookIndex = model.BookIndex;
        book.Title = model.Title;
        book.YearPublished = model.YearPublished;

        return await _bookRepository.UpdateBookAsync(book);
    }

    private async Task<BookDto> GetBookForEdit(Guid bookId)
    {
        var book = await _bookRepository.GetBookAsync(bookId);
        return book ?? throw new ArgumentException();
    }
}