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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();

        modelBuilder
            .Entity<Author>()
            .HasData(
                new Author { AuthorID = 1, Name = "Author 1" },
                new Author { AuthorID = 2, Name = "Author 2" },
                new Author { AuthorID = 3, Name = "Author 3" }
            // Add more authors as needed
            );

        // Seed Books
        modelBuilder
            .Entity<Book>()
            .HasData(
                new Book
                {
                    BookID = 1,
                    Title = "Book 1",
                    Genre = "Genre 1",
                    PublicationDate = DateTime.Now.AddDays(-7)
                },
                new Book
                {
                    BookID = 2,
                    Title = "Book 2",
                    Genre = "Genre 2",
                    PublicationDate = DateTime.Now.AddDays(-14)
                },
                new Book
                {
                    BookID = 3,
                    Title = "Book 3",
                    Genre = "Genre 3",
                    PublicationDate = DateTime.Now.AddDays(-21)
                }
            );

        modelBuilder
            .Entity<BookCopy>()
            .HasData(
                new BookCopy
                {
                    CopyId = 1,
                    BookId = 1,
                    IsAvailable = false
                },
                new BookCopy
                {
                    CopyId = 2,
                    BookId = 1,
                    IsAvailable = true
                },
                new BookCopy
                {
                    CopyId = 3,
                    BookId = 2,
                    IsAvailable = false
                },
                new BookCopy
                {
                    CopyId = 4,
                    BookId = 2,
                    IsAvailable = true
                },
                new BookCopy
                {
                    CopyId = 5,
                    BookId = 3,
                    IsAvailable = false
                }
            );

        // Seed Users
        modelBuilder
            .Entity<User>()
            .HasData(
                new User
                {
                    ID = 1,
                    Username = "user1",
                    Password = "password1",
                    Role = UserRole.Patron,
                    DateJoined = DateTime.Now.AddDays(-30),
                    Name = "Musab"
                },
                new User
                {
                    ID = 2,
                    Username = "user2",
                    Password = "password2",
                    Role = UserRole.Admin,
                    DateJoined = DateTime.Now.AddDays(-20),
                    Name = "Mazen"
                }
            );

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(
            Console.WriteLine,
            new[] { DbLoggerCategory.Database.Command.Name },
            LogLevel.Information
        );
    }
}
