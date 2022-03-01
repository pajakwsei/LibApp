using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LibApp.Interfaces;

namespace LibApp.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookRepository repository;
        private readonly IGenreRepository genreRepository;

        public BooksController(IBookRepository repository, IGenreRepository genreRepository)
        {
            this.repository = repository;
            this.genreRepository = genreRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var book = repository.GetBookById(id);

            if (book == null)
            {
                return Content("Book not found");
            }

            return View(book);
        }

        [Authorize(Roles = "Owner,StoreManager")]
        public IActionResult Edit(int id)
        {
            var book = repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = genreRepository.GetGenres().ToList()
            };

            return View("BookForm", viewModel);
        }

        [Authorize(Roles = "Owner,StoreManager")]
        public IActionResult New()
        {
            var viewModel = new BookFormViewModel
            {
                Genres = genreRepository.GetGenres().ToList()
            };

            return View("BookForm", viewModel);
        }
    }
}