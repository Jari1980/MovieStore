using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Store.Data;
using Movie_Store.Models;
using Movie_Store.Services;
using Movie_Store.Helper;
using Movie_Store.Models.ViewModells;

namespace Movie_Store.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _MovieService;

        const string SessionKeyCart = "ShoppingCart";

        public MoviesController(IMovieService MovieService)
        {
            _MovieService = MovieService;
        }

        public IActionResult Index()
        {
            var movies = _MovieService.GetAllMovies();
            return View(movies);
        }

        public IActionResult Create()
        {
            // _addMovieService.AddMovie();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,ReleaseYear,Price")] Movies movies)
        {
            if (ModelState.IsValid)
            {
               
                _MovieService.AddMovie(movies);
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        public IActionResult Delete(int? Id)
        {
            var movies = _MovieService.GetMovies(Id);
            return View(movies);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Movies movies)
        {
            _MovieService.DeleteMovie(movies);

            return View(movies);
        }

        public IActionResult ShoppingView()
        {
            var shoppingList = _MovieService.GetAllMovies();
            return View(shoppingList);
        }

        public IActionResult AddToCart(int Id)
        {
            if (HttpContext.Session.Get<CartVM>(SessionKeyCart) == default)
            {
                HttpContext.Session.Set<CartVM>(SessionKeyCart, new CartVM());
            }
            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);
            sessionObject.MovieIds.Add(Id);
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);
            return RedirectToAction("ShoppingView");
        }

        public IActionResult RemoveFromCart(int Id)
        {
            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);
            sessionObject.MovieIds.Remove(Id);
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);
            return RedirectToAction("ShoppingView");
        }

        //public IActionResult ShowCart()
        //{
        //    var Cart = HttpContext.Session.Get<CartVM>(SessionKeyCart);

        //    return View(Cart);
        //}


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    if (_MovieService.Movies == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
        //    }
        //    var movies = await _MovieService.Movies.FindAsync(id);
        //    if (movies != null)
        //    {
        //        _MovieService.Movies.Remove(movies);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete([Bind("Id,Title,Director,ReleaseYear,Price")] Movies movies)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        _MovieService.DeleteMovie(movies);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(movies);
        //}
    }
}


//        private readonly ApplicationDbContext _context;

//        public MoviesController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Movies
//        public async Task<IActionResult> Index()
//        {
//              return View(await _context.Movies.ToListAsync());
//        }


//        // GET: Movies/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Movies == null)
//            {
//                return NotFound();
//            }

//            var movies = await _context.Movies
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (movies == null)
//            {
//                return NotFound();
//            }

//            return View(movies);
//        }

//        // GET: Movies/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Movies/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Title,Director,ReleaseYear,Price")] Movies movies)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(movies);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(movies);
//        }

//        // GET: Movies/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Movies == null)
//            {
//                return NotFound();
//            }

//            var movies = await _context.Movies.FindAsync(id);
//            if (movies == null)
//            {
//                return NotFound();
//            }
//            return View(movies);
//        }

//        // POST: Movies/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,ReleaseYear,Price")] Movies movies)
//        {
//            if (id != movies.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(movies);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!MoviesExists(movies.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(movies);
//        }

//        // GET: Movies/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Movies == null)
//            {
//                return NotFound();
//            }

//            var movies = await _context.Movies
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (movies == null)
//            {
//                return NotFound();
//            }

//            return View(movies);
//        }

//        // POST: Movies/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Movies == null)
//            {
//                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
//            }
//            var movies = await _context.Movies.FindAsync(id);
//            if (movies != null)
//            {
//                _context.Movies.Remove(movies);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool MoviesExists(int id)
//        {
//          return _context.Movies.Any(e => e.Id == id);
//        }
//    }
//}
