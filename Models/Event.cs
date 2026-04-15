using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        public string EventName { get; set; }

        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        // Added for Section B of the assignment
        public string? ImageUrl { get; set; }
    }
}