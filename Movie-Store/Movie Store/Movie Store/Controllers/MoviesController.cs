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

        public IActionResult Buy(int Id)
        {
            if (HttpContext.Session.Get<CartVM>(SessionKeyCart) == default)
            {
                HttpContext.Session.Set<CartVM>(SessionKeyCart, new CartVM());
            }
            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);
            sessionObject.MovieIds.Add(Id);
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);
            return RedirectToAction("Shopping");
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
            var count = sessionObject.MovieIds.Count();
            return Json(new { Value = count });
            //return RedirectToAction("ShoppingView");
        }

        public IActionResult RemoveFromCart(int Id)
        {
            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);

            if (sessionObject != null)
            {
                sessionObject.MovieIds.Remove(Id);
                HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

            }
            var count = sessionObject.MovieIds.Count();
            return Json(new { Value = count });
            //return RedirectToAction("ShoppingView");
        }

        // [HttpPost]
        public IActionResult AddCopyFromCart(string Title)
        {
            //Title = Title.Trim();
            var test = Title;

            int Id = _db.Movies.Where(x => x.Title == Title).Select(x => x.Id).First();

            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);
            sessionObject.MovieIds.Add(Id);
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

            return RedirectToAction("ShowCart");
            

        }
        public IActionResult AddCopy(string Title)
        {
            //Title = Title.Trim();
            var test = Title;

            int Id = _db.Movies.Where(x => x.Title == Title).Select(x => x.Id).First();

            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);
            sessionObject.MovieIds.Add(Id);
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

            return RedirectToAction("ShowCart");
            //var count = sessionObject.MovieIds.Count();
            //return Json(new { Value = count });

        }

        // [HttpPost]
        public IActionResult RemoveCopyFromCart(string Title)
        {
            var test = Title;

            int Id = _db.Movies.Where(x => x.Title == Title).Select(x => x.Id).First();

            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);
            sessionObject.MovieIds.Remove(Id);
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

            return RedirectToAction("ShowCart");
        }
        public IActionResult RemoveCopy(string Title)
        {
            var test = Title;

            int Id = _db.Movies.Where(x => x.Title == Title).Select(x => x.Id).First();

            CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);
            sessionObject.MovieIds.Remove(Id);
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

            //return RedirectToAction("ShowCart");
            var count = sessionObject.MovieIds.Count();
            return Json(new { Value = count });
        }

        public IActionResult Shopping()
        {
            var shoppingList = _MovieService.GetAllMovies();
            return View(shoppingList);
        }

        public IActionResult Orders()
        {
            //return RedirectToRoute("CustomerData", "Hej");
            return View();
        }
        //return RedirectToRoute("AlbumHome", new { albumName });


       // [Route("/Movies/CustomerData", Name = "HAHAAAA")]
        [HttpPost]
        public IActionResult CustomerData(int Id)
        {

            string email = _db.Customers.Where(y => y.Id == Id).FirstOrDefault().EmailAdress.ToString();
            //string email = _db.Customers.Where(y => y.Id == Id).FirstOrDefault().EmailAdress.ToString();

            var Orders = _db.Orders.Where(x => x.CustomerId == Id)
                .Join(_db.OrderRows,
                order => order.Id,
                orderR => orderR.OrderId,
                (order, orderR) => new { order, orderR })
                .Join(_db.Movies,
                ord => ord.orderR.MovieId, a => a.Id, (ord, a) => new { ord, a })
                .Join(_db.Customers,
                cust => cust.ord.order.CustomerId, c => c.Id, (cust, c) => new { cust, c })
                .Select(m => new OrderVM
                {
                    eMail = m.c.EmailAdress,
                    FirstName = m.c.FirstName,
                    LastName = m.c.LastName,
                    OrderRow = m.cust.ord.orderR.Id,
                    Order = m.cust.ord.order.Id,
                    Price = m.cust.ord.orderR.Price,
                    Title = m.cust.a.Title
                }).ToList();

            //var OrderQuery = _db.Orders.Where(x => x.CustomerId == Id).Zip(_db.OrderRows).ToList();
            //var NewOrderQuery = OrderQuery.Zip(_db.Movies, _db.Customers).Select(movie => new OrderVM
            //{
            //    eMail = movie.Third.EmailAdress,
            //    FirstName = movie.Third.FirstName,
            //    LastName = movie.Third.LastName,
            //    OrderRow = movie.First.First.Id,
            //    Order = movie.First.Second.Order.Id,
            //    Price = movie.First.Second.Price,
            //    Title = movie.Second.Title
            //}).ToList();


            //return RedirectToRoute("CustomerData", new { email });
            return View(Orders);
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

            //_db.Customers.AddRange(
            //new Customers
            //{
            //    FirstName = firstName,
            //    LastName = lastName,
            //    BillingAdress = billingAdress,
            //    BillingZip = billingzip,
            //    DeliveryAddress = deliveryAdress,
            //    DeliveryZip = deliveryZip,
            //    EmailAdress = email,
            //    PhoneNo = phoneNumber
            //}
            //);
           


            Orders ord = new Orders()
            {
                OrderDate = DateTime.Now,
                CustomerId = cust.Id,
                Customer = cust,
            };

            //_db.Orders.AddRange(
            //    new Orders
            //    {
            //        OrderDate = DateTime.Now,
            //        CustomerId = cust.Id,
            //        Customer = cust,
            //    }
            //    );

            //Test
            int counter = sessionObject.MovieIds.Count();
            var orderRows = new List<OrderRows>();
            for (int i = 0; i < counter; i++) {
                //ord.OrderRows.Add(_db.OrderRows. Max(o => o.Id);
                //var Test = _db.OrderRows.TakeLast(1);
                //ord.OrderRows.Add(Test);
                //_db.OrderRows.UpdateRange();
                //_db.UpdateRange();
                orderRows.Add(new OrderRows
                {
                    Order = ord,
                    MovieId = sessionObject.MovieIds[i],
                    Price = (_db.Movies.FirstOrDefault(m => m.Id == sessionObject.MovieIds[i])).Price
                });
            }
            _db.OrderRows.AddRange(orderRows);
            _db.SaveChanges();


            //_db.SaveChanges();
            TempData["ePost"] = email;
            TempData["movCount"] = counter;
            int counter2 = sessionObject.MovieIds.Count();
            for (int j = 0; j < counter2; j++)
            {
                sessionObject.MovieIds.RemoveAt(0);
            }
            HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

            return RedirectToAction("PurchaseDone");
        }

        
        public IActionResult PurchaseDone()
        {
            //if (TempData["ePost"] == null) { 

            int movieCount = (int)TempData["movCount"];
            
            var receipt = _db.Customers.OrderByDescending(x => x.Id).Where(x => x.EmailAdress == TempData["ePost"].ToString())
                .Join(_db.Orders,
                customer => customer.Id,
                order => order.CustomerId,
                (customer, order) => new { customer, order })
                .Join(_db.OrderRows,
                cust => cust.order.Id,
                orderR => orderR.OrderId,
                (cust, orderR) => new { cust, orderR })
                .Join(_db.Movies,
                ord => ord.orderR.MovieId,
                mov => mov.Id,
                (ord, mov) => new ReceiptVM
                {
                    Price = mov.Price,
                    Title = mov.Title
                }).ToList().Take(movieCount);
                return View(receipt);
        }


            //var lastId = _db.Orders.OrderBy(x => x.Id).Last().Id;

            //var receipt = _db.Orders.Where(x => x.Id == lastId)
            // .Join(_db.OrderRows,
            // order => order.Id,
            // orderR => orderR.OrderId,
            // (order, orderR) => new { order, orderR })
            // .Join(_db.Movies,
            //ord => ord.orderR.MovieId,
            //m => m.Id,
            //(ord, m) => new ReceiptVM
            //{
            //    Price = m.Price,
            //    Title = m.Title
            //}).ToList();

          
    



    // CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);

    // if (sessionObject == null)
    // {
    //     return View();
    // }

    // var SCart = (from m in _db.Movies.ToList().OrderBy(x => x.Title)
    //              join s in sessionObject.MovieIds
    //              on m.Id equals s
    //              select new ShoppingVM()
    //              {
    //                  Title = m.Title,
    //                  Price = m.Price,
    //              });
    //return View(SCart);


    //public IActionResult Final()
    //{
    //    CartVM sessionObject = HttpContext.Session.Get<CartVM>(SessionKeyCart);



    //    int counter2 = sessionObject.MovieIds.Count();
    //    for (int j = 0; j < counter2; j++)
    //    {
    //        sessionObject.MovieIds.RemoveAt(0);
    //    }
    //    HttpContext.Session.Set<CartVM>(SessionKeyCart, sessionObject);

    //    return RedirectToAction("Index", "Home");
    //}

}
}

