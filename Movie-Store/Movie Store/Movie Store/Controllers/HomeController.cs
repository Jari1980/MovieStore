using Microsoft.AspNetCore.Mvc;
using Movie_Store.Models;
using Movie_Store.Data;
using Movie_Store.Services;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Store.Helper;
using Movie_Store.Models.ViewModells;

namespace Movie_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            FrontPageVM frontPage = new();
            frontPage.MostPopular = _db.OrderRows.GroupBy(o => o.MovieId)
                .Select(p => new FiveMostPopularMoviesVM()
                {
                    //var id = m.Key
                    Title = _db.Movies.Where(m => m.Id == p.Key).FirstOrDefault().Title,
                    test = _db.Movies.Where(m => m.Id == p.Key).FirstOrDefault().MovieImg,         
                    BuyCount = p.Count()
                })
                .OrderByDescending(c => c.BuyCount)
                .Take(5)
                .ToList();
            frontPage.FiveNewest = _db.Movies.OrderByDescending(m => m.ReleaseYear)
                .Select(m => new FiveNewestVM()
                {
                    Title = m.Title,
                    Img = m.MovieImg     
                })
                .Take(5)
                .ToList();
            frontPage.FiveOldest = _db.Movies.OrderBy(m => m.ReleaseYear)
                .Select(m => new FiveOldestVM()
                {
                    Title = m.Title,
                    Img = m.MovieImg
                })
                .Take(5)
                .ToList();
            frontPage.FiveCheapest = _db.Movies.OrderBy(m => m.Price)
                .Select(m => new FiveCheapestVM()
                {
                    Title = m.Title,
                    Img = m.MovieImg
                })
                .Take(5)
                .ToList();
            //var mostExpensiveOrderId = _db.OrderRows.GroupBy(o => o.OrderId)
            //    .OrderByDescending(o => o.Sum(or => or.Price)).FirstOrDefault().Count
                
                var mostExpensiveOrder = _db.Orders
                .OrderByDescending(o => o.OrderRows.Sum(or => or.Price)).Take(1).First();                
                //.Max(o => ((int)o.Sum(o => o.Price)));

            double mostExpensiveCount = _db.OrderRows.Where(o => o.OrderId == mostExpensiveOrder.Id).Sum(o => o.Price);

            frontPage.CustomerExp = _db.OrderRows.Where(ordR => ordR.Id == mostExpensiveOrder.Id)
                .Join(_db.Orders,
                ordR => ordR.OrderId,
                ord => ord.Id,
                (ordR, ord) => new { ordR, ord })
                .Join(_db.Customers,
                o => o.ord.CustomerId,
                cus => cus.Id,
                (o, cus) => new CustomerExpVM
                {
                    FirstName = cus.FirstName,
                    LastName = cus.LastName,
                    Sum = mostExpensiveCount,
                }).ToList();


            return View(frontPage);
        }

        //public IActionResult FiveMostPopularMovies()
        //{
        //    var MostPopular = _db.OrderRows.GroupBy(o => o.MovieId)
        //        .Select(m => new FiveMostPopularMoviesVM()
        //        {
        //            Title = _db.Movies.Find(m.Key).Title,
        //            BuyCount = m.Count()
        //        })
        //        .OrderByDescending(c => c.BuyCount)
        //        .Take(5)
        //        .ToList();


        //    var MostPopularMovies = _db.OrderRows.GroupBy(o => o.MovieId).OrderByDescending(o => o.Count()).Take(5)
        //        .Join(_db.Movies,
        //        ord => ord.Key,
        //        mov => mov.Id,
        //        (ord, mov) => new FiveMostPopularMoviesVM
        //        {
        //            Title = mov.Title,
        //            test = mov.Director
        //        }
        //        ).ToList();
        //    //_db.Movies.Select(m => m.Title).OrderByDescending(m => m).Take(5).ToList();

        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}