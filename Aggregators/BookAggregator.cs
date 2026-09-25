using System;
using example_csharp_app.Interfaces;
using example_csharp_app.Models.Mapper;
using example_csharp_app.Models.Request;
using example_csharp_app.Models.Response;

namespace example_csharp_app.Aggregators;

public class BookAggregator : IBookAggregator
{
    private readonly IBookService _bookService;

    public BookAggregator(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<BookOperationResponse> AddBookAsync(BookCreateRequest request)
    {
        var result = await _bookService.AddBookAsync(request.MapToModel());
        return new BookOperationResponse()
        {
            IsSuccess = result > 0
        };
    }

    public async Task<BookSingleResponse> GetBookAsync(Guid bookId)
    {
        var book = await _bookService.GetBookAsync(bookId);
        return new BookSingleResponse()
        {
            Book = book
        };
    }

    public async Task<BookListResponse> GetBookListAsync(BookFilterRequest request)
    {
        // should make separate filter object, but feel lazy
        var bookList = await _bookService.GetBooksAsync(request);
        return new BookListResponse()
        {
            Books = bookList  
        };
    }

    public async Task<BookOperationResponse> RemoveBookAsync(Guid bookId)
    {
        var result = await _bookService.RemoveBookAsync(bookId);
        return new BookOperationResponse()
        {
            IsSuccess = result > 0  
        };
    }

    public async Task<BookOperationResponse> UpdateBookAsync(BookUpdateRequest request)
    {
        var result = await _bookService.UpdateBookAsync(request.Id, request.MapToModel());
        return new BookOperationResponse()
        {
            IsSuccess = result > 0
        };
    }
}
