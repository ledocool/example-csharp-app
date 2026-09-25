using System;
using example_csharp_app.Models.Model;
using example_csharp_app.Models.Request;

namespace example_csharp_app.Models.Mapper;

public static class RequestMapper
{
    public static BookCreateModel MapToModel(this BookCreateRequest request)
    {
        return new BookCreateModel()
        {
            Author = request.Author,
            BookIndex = request.BookIndex.Trim(),
            Title = request.Title,
            YearPublished = request.YearPublished
        };
    }

    public static BookUpdateModel MapToModel(this BookUpdateRequest request)
    {
        return new BookUpdateModel()
        {
            Author = request.Author,
            BookIndex = request.BookIndex.Trim(),
            Title = request.Title,
            YearPublished = request.YearPublished
        };
    }
}
