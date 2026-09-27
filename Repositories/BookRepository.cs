using System;
using example_app.Database.Connector;
using example_csharp_app.Dto;
using example_csharp_app.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;
using System.Globalization;
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
        var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"CALL public.\"sp_InsertBook\" ({dto.Id}, {dto.Title}, {dto.Author}, {dto.YearPublished}, {dto.BookIndex}::XML)"
        );

        return 1; // Процедура в pgsql не возвращает значение - это делают функции; даже rowcount только в функции

        // _dbContext.Add(dto);
        // return await _dbContext.SaveChangesAsync();
    }

    public async Task<BookDto?> GetBookAsync(Guid bookId, bool enableEdit = true)
    {
        var query = _dbContext.Books.FromSqlInterpolated(
            $"SELECT * FROM \"sp_GetBookById\"({bookId})"
        );

        return await query.FirstOrDefaultAsync();

        // var query = _dbContext.Books.Where(x => x.Id == bookId);
        // if(enableEdit == false)
        // {
        //     query = query.AsNoTracking();
        // }
        // return await query.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<BookDto>> GetBookListAsync(BookFilterRequest request)
    {
        
        var books = await _dbContext.Books.FromSqlInterpolated($"SELECT * FROM \"sp_SearchBooks\" ({request.Title}, {request.Author}, {request.Index})").ToListAsync();
        return books;

        // IQueryable<BookDto> query = _dbContext.Books.AsNoTracking();
        // if(request.Author != null)
        // {
        //     query = query.Where(x => x.Author.Contains(request.Author));
        // }
        // if(request.Title != null)
        // {
        //     query = query.Where(x => x.Title.Contains(request.Title));
        // }
        // if(request.Index != null)
        // {
        //     var interminentQuery = await query.ToListAsync();
        //     return interminentQuery.Where(x => x.BookIndex.Contains(request.Index)).ToList(); 
        //     // решение плохое

        //     // xml ~~ xml - operation does not exist
        //     //query = query.Where(x => x.BookIndex.Contains(request.Index));
        // }

        // return await query.ToListAsync();
    }

    public async Task<int> RemoveBookAsync(BookDto dto)
    {
        var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"CALL \"sp_DeleteBook\"({dto.Id})"
        );
        return 1; // Процедура в pgsql не возвращает значение - это делают функции; даже rowcount только в функции

        // _dbContext.Books.Remove(dto);
        // return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateBookAsync(BookDto dto)
    {
        var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            $"CALL \"sp_UpdateBook\"({dto.Id}, {dto.Title}, {dto.Author}, {dto.YearPublished.ToString()}, {dto.BookIndex})"
        );

        return 1; // Процедура в pgsql не возвращает значение - это делают функции; даже rowcount только в функции

        // _dbContext.Books.Update(dto);
        // return await _dbContext.SaveChangesAsync();
    }
}
