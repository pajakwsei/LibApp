using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Rep
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext context;

        public BookRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddBook(Book book)
        {
            this.context.Books.Add(book);
        }

        public void DeleteBook(int bookId)
        {
            this.context.Books.Remove(this.GetBookById(bookId));
        }

        public Book GetBookById(int bookId)
        {
            return this.context.Books.Include(b => b.Genre).Where(b => b.Id == bookId).SingleOrDefault();
        }

        public IEnumerable<Book> GetBooks()
        {
            return this.context.Books.Include(b => b.Genre);
        }

        public void UpdateBook(Book book)
        {
            this.context.Entry(book).State = EntityState.Modified;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
