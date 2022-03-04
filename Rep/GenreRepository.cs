using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using LibApp.Data;
using LibApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Rep
{

    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext context;

        public GenreRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void AddGenre(Genre genre)
        {
            context.Genre.Add(genre);
        }

        public void DeleteGenre(int genreId)
        {
            context.Genre.Remove(GetGenreById(genreId));
        }

        public Genre GetGenreById(int genreId)
        {
            return context.Genre.Find(genreId);
        }

        public IEnumerable<Genre> GetGenres()
        {
            return context.Genre;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateGenre(Genre genre)
        {
            context.Genre.Update(genre);
        }
    }
}
