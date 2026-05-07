/* <!--CODE ATTRIBUTION-->

<!--TITLE: (EVENTEASE)-->

<!--AUTHOR: ( GOUWELOOS , MICK )-->

<!--DATE: ( 15/04/2026 )-->

<!--VERSION: (FIREST EDITION) -->

<!--AVAILABLE: (https://advtechonline.sharepoint.com/:w:/r/sites/TertiaryStudents/_layouts/15/Doc.aspx?sourcedoc=%7B9C23B0F8-6BED-497E-B60C-1D56E59BEDAB%7D&file=PROG6221_MO.docx&action=default&mobileredirect=true)-->    
*/
/* */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEase.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        // --- THE BRIDGES (Foreign Keys) ---

        [Required(ErrorMessage = "Please select an event")]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public virtual Event? Event { get; set; }

        [Required(ErrorMessage = "Please select a venue")]
        [ForeignKey("Venue")]
        public int VenueId { get; set; } // Added to prevent double bookings at a specific location
        public virtual Venue? Venue { get; set; }
    }
}