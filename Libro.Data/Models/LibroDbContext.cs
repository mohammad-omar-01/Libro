using Libro.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class LibroDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<BookCopy> BookCopies { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public LibroDbContext(DbContextOptions<LibroDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(
            Console.WriteLine,
            new[] { DbLoggerCategory.Database.Command.Name },
            LogLevel.Information
        );
    }
}
