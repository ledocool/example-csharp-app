using System;
using System.ComponentModel.DataAnnotations;

namespace example_csharp_app.Dto;

public class BookDto
{
    // Для описания модели использовал fluent api в AppDbContext
    public Guid Id { set; get; }
    public string Title { set; get; } = string.Empty;
    public string Author { set; get; } = string.Empty;
    public DateTime YearPublished { set; get; }
    public string BookIndex { set; get; } = string.Empty;
}
