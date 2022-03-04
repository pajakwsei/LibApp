using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using LibApp.Interfaces;

namespace LibApp.Controllers
{
   
    public class BooksController : Controller
    {
        private readonly IBookRepository _books;
        private readonly IGenreRepository _genre;

        public BooksController(IBookRepository books, IGenreRepository genre)
        {
            _books = books;
            _genre = genre;
        }

        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Owner, User, StoreManager")]
        public IActionResult Index()
        {
            var books = _books.GetBooks();

            return View(books);
        }
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Owner, User, StoreManager")]
        public IActionResult Details(int id)
        {
            var book = _books.GetBookById(id);

            return View(book);
        }
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Owner, User, StoreManager")]
        public IActionResult Edit(int id)
        {
            var book = _books.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _genre.GetGenres()
            };

            return View("BookForm", viewModel);
        }

        public IActionResult New()
        {
            var viewModel = new BookFormViewModel
            {
                Genres = _genre.GetGenres()
            };

            return View("BookForm", viewModel);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Save(Book book)
        {
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _books.AddBook(book);
            }
            else
            {
                var bookInDb = _books.GetBookById(book.Id);
                bookInDb.Name = book.Name;
                bookInDb.AuthorName = book.AuthorName;
                bookInDb.GenreId = book.GenreId;
                bookInDb.ReleaseDate = book.ReleaseDate;
                bookInDb.DateAdded = book.DateAdded;
                bookInDb.NumberInStock = book.NumberInStock;
            }

            try
            {
                _books.Save();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }
    }
}
