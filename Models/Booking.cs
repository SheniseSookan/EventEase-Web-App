using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public DateTime BookingDate { get; set; }
    }
}