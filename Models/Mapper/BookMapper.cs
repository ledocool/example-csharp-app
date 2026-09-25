using System;
using example_csharp_app.Dto;
using example_csharp_app.Models.Model;

namespace example_csharp_app.Models.Mapper;

public static class BookMapper
{
    public static BookModel MapToModel(this BookDto dto)
    {
        return new BookModel()
        {
            Author = dto.Author,
            BookIndex = dto.BookIndex,
            Id = dto.Id,
            Title = dto.Title,
            YearPublished = dto.YearPublished
        };
    }

    public static BookDto MapToDto(this BookCreateModel model)
    {
        return new BookDto()
        {
            Id = Guid.NewGuid(),
            Author = model.Author,
            BookIndex = model.BookIndex,
            Title = model.Title,
            YearPublished = model.YearPublished
        };
    }
}
