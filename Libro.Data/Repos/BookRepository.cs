﻿using Libro.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Data.Repos
{
    public class BookRepository : IBookRepository
    {
        private readonly LibroDbContext _dbContext;

        public BookRepository(LibroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public Book GetBookById(int bookId)
        {
            return _dbContext.Books.FirstOrDefault(book => book.BookID == bookId);
        }

        public List<Book> GetAllBooks()
        {
            return _dbContext.Books.ToList();
        }

        public void UpdateBook(Book book)
        {
            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }

        public void DeleteBook(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }
        }

        public bool AddAuthorToBook(int bookId, int authorId)
        {
            var author = _dbContext.Authors.FirstOrDefault(author => author.AuthorID == authorId);
            var book = _dbContext.Books.FirstOrDefault(book => book.BookID == bookId);
            if (book != null)
            {
                book.Authors.Add(author);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
