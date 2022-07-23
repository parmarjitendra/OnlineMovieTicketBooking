using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMovieBooking.Models
{
    public class BookingTable
    {
        public int Id { get; set; }
        public string seatno { get; set; }

        public string UserId { get; set; }

        public DateTime Datetopresent { get; set; }

        public int MovieDetailsId { get; set; }
        public int Amount { get; set; }
        [ForeignKey("MovieDetailsId") ]
        public virtual MovieDetails MovieDetails { get; set; }

     


    }
}
