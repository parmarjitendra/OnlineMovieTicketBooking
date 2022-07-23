using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMovieBooking.Models
{
    public class BookNowViewModel
    {
        public string Movie_Name { get; set; }
        public DateTime Movie_Date { get; set; }
        [Key]
        public string SeatNo { get; set; }
        public int Amount { get; set; }
        
        public int MovieId { get; set; }

        

        
    }
}
