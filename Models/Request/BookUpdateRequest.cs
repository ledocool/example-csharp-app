using System;
using System.ComponentModel.DataAnnotations;
using example_csharp_app.Filters;

namespace example_csharp_app.Models.Request;

public class BookUpdateRequest
{
    [Required]
    public Guid Id { set; get; } = Guid.Empty;
    [Required]
    public string Title { set; get; } = string.Empty;
    [Required]
    public string Author { set; get; } = string.Empty;
    [Required]
    public DateTime YearPublished { set; get; }
    [Required, Xml]
    public string BookIndex { set; get; } = string.Empty;
}
