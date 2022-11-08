﻿using System;
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
        private readonly ApplicationDbContext _db;

        const string SessionKeyCart = "ShoppingCart";

        public MoviesController(IMovieService MovieService, ApplicationDbContext db)
        {
            _MovieService = MovieService;
            _db = db;
        }

        public IActionResult Index()
        {
            var movies = _MovieService.GetAllMovies();
            return View(movies);
        }

        public IActionResult Create()
        {
           
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

       // [HttpPost]
        public IActionResult AddCopy(string Title)
        {
            //Title = Title.Trim();
            var test = Title;

            int Id = _db.Movies.Where(x => x.Title == Title).Select(x => x.Id).First();

            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);
            sessionObject.MovieIds.Add(Id);
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

            return RedirectToAction("ShowCart");
        }

        // [HttpPost]
        public IActionResult RemoveCopy(string Title)
        {
            var test = Title;

            int Id = _db.Movies.Where(x => x.Title == Title).Select(x => x.Id).First();

            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);
            sessionObject.MovieIds.Remove(Id);
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

            return RedirectToAction("ShowCart");
        }


        public IActionResult ShowCart()
        {
            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);

            if (sessionObject == null)
            {
                return View();
            }

            var SCart = (from m in _db.Movies.ToList().OrderBy(x => x.Title)
                         join s in sessionObject.MovieIds
                         on m.Id equals s
                         select new ShoppingVM()
                         {
                             Title = m.Title,
                             Price = m.Price,
                         });   

            return View(SCart);
        }

        [HttpPost]
        public IActionResult submitOrder(string firstName, string lastName, string billingAdress, string billingzip,
            string deliveryAdress, string deliveryZip, string email, string phoneNumber)
        {

            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);


            Customers cust = new Customers()
            {
                FirstName = firstName,
                LastName = lastName,
                BillingAdress = billingAdress,
                BillingZip = billingzip,
                DeliveryAddress = deliveryAdress,
                DeliveryZip = deliveryZip,
                EmailAdress = email,
                PhoneNo = phoneNumber
            };


            Orders ord = new Orders()
            {
                OrderDate = DateTime.Now,
                CustomerId = cust.Id,
                Customer = cust,
            };

            _db.Orders.AddRange(
                new Orders
                {
                    OrderDate = DateTime.Now,
                    CustomerId = cust.Id,
                    Customer = cust,
                }
                );

            int counter = sessionObject.MovieIds.Count();
            for (int i = 0; i < counter; i++) {
                _db.OrderRows.AddRange(
                    new OrderRows
                    {
                        OrderId = _db.Orders.Max(o => o.Id),
                        MovieId = sessionObject.MovieIds[i],
                        Price = (_db.Movies.FirstOrDefault(m => m.Id == sessionObject.MovieIds[i])).Price
                    }
                    );
                _db.SaveChanges();
            }
            int counter2 = sessionObject.MovieIds.Count();
            for (int j = 0; j < counter2; j++)
            {
                sessionObject.MovieIds.RemoveAt(0);
            }
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

            return View("ShowCart");
        }
    }
}

