using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages_Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movies { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get;set; }

        public SelectList? Genres { get; set; }

        
        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get;set; }


        public async Task OnGetAsync()
        {
            var moviesQuery = from m in _context.Movie
                              select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                moviesQuery = moviesQuery.Where(s => s.Title.Contains(SearchString));
            }

            Movies = await moviesQuery.ToListAsync();
        }
    }
}
