using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMovieBooking.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMovieBooking.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<BookingTable> BookingTable { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<MovieDetails> MovieDetails { get; set; }
        public DbSet<OnlineMovieBooking.Models.BookNowViewModel> BookNowViewModel { get; set; }
    }
}
