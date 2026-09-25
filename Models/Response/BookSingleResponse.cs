using System;
using example_csharp_app.Models.Model;

namespace example_csharp_app.Models.Response;

public class BookSingleResponse
{
    public BookModel? Book { set; get; }
}
