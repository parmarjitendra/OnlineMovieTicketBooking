using FileUploadControls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMovieBooking.Data;
using OnlineMovieBooking.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieBooking.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private UploadInterface _upload;
        public AdminController(ApplicationDbContext context, UploadInterface upload)
        {
            _upload = upload;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles ="")]
        [HttpPost]
        public IActionResult Create(IList<IFormFile> files, MovieDetails vmodel, MovieDetails movie)
        {
            movie.Movie_Name = vmodel.Movie_Name;
            movie.Movie_Description = vmodel.Movie_Description;
            movie.DateAndTime = vmodel.DateofMovie;
            foreach (var item in files)
            {
                movie.MoviePicture = "~/uploads/" + item.FileName.Trim();
            }
            //_upload.uploadfilemultiple(files);
            _context.MovieDetails.Add(movie);
            _context.SaveChanges();
            TempData["Sucess"] = "save your Movie";
            return RedirectToAction("Create", "Admin");
        }
        [HttpGet]
        public IActionResult checkBookSeat()
        {
            var getBookingTable = _context.BookingTable.ToList().OrderByDescending(a => a.Datetopresent);
            return View(getBookingTable);
        }
        [HttpGet]
        public IActionResult GetUserDetails()
        {
            var getUserTable = _context.Users.ToList();
            return View(getUserTable);
        }
    }
}
