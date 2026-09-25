using System;

namespace example_csharp_app.Models.Model;

public class BookModel
{
    public Guid Id { set; get; }
    public string Title { set; get; } = string.Empty;
    public string Author { set; get; } = string.Empty;
    public DateTime YearPublished { set; get; }
    public string BookIndex { set; get; } = string.Empty;
}
