﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Libro.Data.Migrations
{
    [DbContext(typeof(LibroDbContext))]
    partial class LibroDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.Property<int>("AuthorsAuthorID")
                        .HasColumnType("int");

                    b.Property<int>("BooksBookID")
                        .HasColumnType("int");

                    b.HasKey("AuthorsAuthorID", "BooksBookID");

                    b.HasIndex("BooksBookID");

                    b.ToTable("AuthorBook");
                });

            modelBuilder.Entity("Libro.Data.Models.Author", b =>
                {
                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorID");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorID = 1,
                            Name = "Author 1"
                        },
                        new
                        {
                            AuthorID = 2,
                            Name = "Author 2"
                        },
                        new
                        {
                            AuthorID = 3,
                            Name = "Author 3"
                        });
                });

            modelBuilder.Entity("Libro.Data.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookID"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookID");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookID = 1,
                            Genre = "Genre 1",
                            PublicationDate = new DateTime(2023, 7, 16, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9170),
                            Title = "Book 1"
                        },
                        new
                        {
                            BookID = 2,
                            Genre = "Genre 2",
                            PublicationDate = new DateTime(2023, 7, 9, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9211),
                            Title = "Book 2"
                        },
                        new
                        {
                            BookID = 3,
                            Genre = "Genre 3",
                            PublicationDate = new DateTime(2023, 7, 2, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9214),
                            Title = "Book 3"
                        });
                });

            modelBuilder.Entity("Libro.Data.Models.BookCopy", b =>
                {
                    b.Property<int>("CopyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CopyId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.HasKey("CopyId");

                    b.HasIndex("BookId");

                    b.ToTable("BookCopies");

                    b.HasData(
                        new
                        {
                            CopyId = 1,
                            BookId = 1,
                            IsAvailable = false
                        },
                        new
                        {
                            CopyId = 2,
                            BookId = 1,
                            IsAvailable = true
                        },
                        new
                        {
                            CopyId = 3,
                            BookId = 2,
                            IsAvailable = false
                        },
                        new
                        {
                            CopyId = 4,
                            BookId = 2,
                            IsAvailable = true
                        },
                        new
                        {
                            CopyId = 5,
                            BookId = 3,
                            IsAvailable = false
                        });
                });

            modelBuilder.Entity("Libro.Data.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<int>("BookCopyId")
                        .HasColumnType("int");

                    b.Property<int>("PatronId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ReservationId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Libro.Data.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("BookCopyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Borrowdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatronId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Libro.Data.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DateJoined = new DateTime(2023, 6, 23, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9257),
                            Email = "",
                            Name = "Musab",
                            Password = "password1",
                            Role = "Patron",
                            Username = "user1"
                        },
                        new
                        {
                            ID = 2,
                            DateJoined = new DateTime(2023, 7, 3, 0, 43, 28, 46, DateTimeKind.Local).AddTicks(9262),
                            Email = "",
                            Name = "Mazen",
                            Password = "password2",
                            Role = "Admin",
                            Username = "user2"
                        });
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.HasOne("Libro.Data.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsAuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Libro.Data.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksBookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Libro.Data.Models.BookCopy", b =>
                {
                    b.HasOne("Libro.Data.Models.Book", null)
                        .WithMany("Copies")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Libro.Data.Models.Book", b =>
                {
                    b.Navigation("Copies");
                });
#pragma warning restore 612, 618
        }
    }
}