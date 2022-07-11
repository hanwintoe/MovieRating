using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRating.Data;
using MovieRating.Models;

namespace MovieRating.Controllers
{
    public class MoviesController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            List<MovieDto> movieDtos = new();
            var rating = _context.Ratings.ToList();
            var movie = _context.Movies.ToList();

            var result = (from mv in _context.Movies
                          join rt in _context.Ratings
                          on mv.Id equals rt.MovieId
                          select new
                          {
                              mv.Id,
                              mv.Title,
                              rt.RateCount,
                              rt.RateTotal
                          }).ToList();
            movieDtos.AddRange(from rst in result
                               let movieDto = new MovieDto()
                               {
                                   Id = rst.Id,
                                   Title = rst.Title,
                                   RateCount = rst.RateCount,
                                   RateTotal = rst.RateTotal

                               }
                               select movieDto);
            return View(movieDtos);
        }

      
        [HttpPost]
        public async Task<JsonResult> PostRating(int rating, int mid)
        {
            var curRt = _context.Ratings.FirstOrDefault(r => r.MovieId == mid);

            curRt.RateCount += 1;
            curRt.RateTotal += rating;
            
            _context.Ratings.Update(curRt);
            await _context.SaveChangesAsync();

            return Json(" You rate this " + rating.ToString() + " star(s)");
            
        }
    }
}
