//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineMovieBooking.Data;
using OnlineMovieBooking.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieBooking.Controllers
{

    public class HomeController : Controller
    {
        int count = 1;
        bool flag = true;
        private UserManager<ApplicationUser> _usermanager;
        private ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            //_usermanager = usermanager;
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            var getMovieList = _context.MovieDetails.ToList();
            return View(getMovieList);
        }
        [HttpGet]
        public IActionResult BookNow(int Id)
        {
            BookNowViewModel vm = new BookNowViewModel();
            var item = _context.MovieDetails.Where(a => a.Id == Id).FirstOrDefault();
            vm.Movie_Name = item.Movie_Name;
            vm.Movie_Date = item.DateofMovie;
            vm.MovieId = Id;
            return View(vm);
        }
        [HttpPost]
        public IActionResult BookNow(BookNowViewModel vm)
        {
            List<BookingTable> booking = new List<BookingTable>();
            List<Cart> carts = new List<Cart>();
            string seatno = vm.SeatNo.ToString();
            int MovieId = vm.MovieId;

            string[] seatnoArray = seatno.Split(',');
            count = seatnoArray.Length;
            if (checkseat(seatno, MovieId) == false)
            {
                foreach(var item in seatnoArray)
                {
                    carts.Add(new Cart { Amount = 150, MovieId = vm.MovieId, UserId = _usermanager.GetUserId(HttpContext.User), date = vm.Movie_Date, seatno = item });
                }
                foreach(var item in carts)
                {
                    _context.Cart.Add(item);
                    _context.SaveChanges();
                }
                TempData["Sucess"] = "Seat no booked,check your cart";
            }
            else
            {
                TempData["Sucess"] = "Please change your seat no";
            }
            return RedirectToAction("BookNow");
        }

        private bool checkseat(string seatno, int movieId)
        {
            string seats = seatno;
            string[] seatreserve = seatno.Split(",");
            var seatnolist = _context.BookingTable.Where(a=>a.MovieDetailsId==movieId).ToList();
            foreach(var item in seatnolist)
            {
                string alreadybook = item.seatno;
                foreach(var item1 in seatreserve)
                {
                    if (item1 == alreadybook)
                    {
                        flag = false;
                        break;
                    }
                }

            }
            if (flag == false)
                return true;
            else
                return false;

        }

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
