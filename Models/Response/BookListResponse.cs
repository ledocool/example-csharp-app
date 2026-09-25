using System;
using example_csharp_app.Models.Model;

namespace example_csharp_app.Models.Response;

public class BookListResponse
{
    public IReadOnlyCollection<BookModel> Books { set; get; } = [];
}
