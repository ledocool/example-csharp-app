using example_csharp_app.Dto;
using Microsoft.EntityFrameworkCore;
using System;

namespace example_app.Database.Connector;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BookDto> Books { set; get; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BookDto>()
            .ToTable("Books")
            .HasKey(x => x.Id);

        builder.Entity<BookDto>()
            .Property(x => x.Author)
            .IsRequired();
        builder.Entity<BookDto>()
            .Property(x => x.Title)
            .IsRequired();
        builder.Entity<BookDto>()
            .Property(x => x.YearPublished)
            .IsRequired();
        builder.Entity<BookDto>()
            .Property(x => x.BookIndex)
            .IsRequired();

        builder.Entity<BookDto>()
            .Property(x => x.BookIndex)
            .HasColumnType("xml");
    }
}
