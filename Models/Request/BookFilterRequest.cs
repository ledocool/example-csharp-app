using System;

namespace example_csharp_app.Models.Request;

public class BookFilterRequest
{
    public string? Author { set; get; }
    public string? Index { set; get; }
    public string? Title { set; get; }
}
