
/* <!--CODE ATTRIBUTION-->

<!--TITLE: (EVENTEASE)-->

<!--AUTHOR: ( GOUWELOOS , MICK )-->

<!--DATE: ( 15/04/2026 )-->

<!--VERSION: (FIREST EDITION) -->

<!--AVAILABLE: (https://advtechonline.sharepoint.com/:w:/r/sites/TertiaryStudents/_layouts/15/Doc.aspx?sourcedoc=%7B9C23B0F8-6BED-497E-B60C-1D56E59BEDAB%7D&file=PROG6221_MO.docx&action=default&mobileredirect=true)-->    
*/
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required(ErrorMessage = "Please enter a venue name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Range(1, 5000, ErrorMessage = "Capacity must be between 1 and 5000")]
        public int Capacity { get; set; }

        // Stores the URL from Azurite (Section A & D Requirement)
        public string? ImageUrl { get; set; }

        // Navigation property for the Deletion Restriction logic (Section B)
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}