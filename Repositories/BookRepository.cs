using System;
using example_app.Database.Connector;
using example_csharp_app.Dto;
using example_csharp_app.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using example_csharp_app.Models.Request;

namespace example_csharp_app.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _dbContext;

    public BookRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddBookAsync(BookDto dto)
    {
        _dbContext.Add(dto);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<BookDto?> GetBookAsync(Guid bookId, bool enableEdit = true)
    {
        var query = _dbContext.Books.Where(x => x.Id == bookId);
        if(enableEdit == false)
        {
            query = query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<BookDto>> GetBookListAsync(BookFilterRequest request)
    {
        IQueryable<BookDto> query = _dbContext.Books.AsNoTracking();
        if(request.Author != null)
        {
            query = query.Where(x => x.Author.Contains(request.Author));
        }
        if(request.Title != null)
        {
            query = query.Where(x => x.Title.Contains(request.Title));
        }
        if(request.Index != null)
        {
            var interminentQuery = await query.ToListAsync();
            return interminentQuery.Where(x => x.BookIndex.Contains(request.Index)).ToList(); // ничего лучше не придумал
            // xml ~~ xml - operation does not exist
            //query = query.Where(x => x.BookIndex.Contains(request.Index));
        }

        return await query.ToListAsync(); // можно возвращать query как IEnumerable, но в некоторых случаях это будет приводить к исключению
                                            // например если контекст инжектится фабрикой
    }

    public async Task<int> RemoveBookAsync(BookDto dto)
    {
        _dbContext.Books.Remove(dto);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateBookAsync(BookDto dto)
    {
        _dbContext.Books.Update(dto);
        return await _dbContext.SaveChangesAsync();
    }
}
