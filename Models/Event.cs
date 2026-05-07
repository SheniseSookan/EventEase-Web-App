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
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Event Name is required")]
        [StringLength(100)]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Please provide a short description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        // Stores the URL from Azurite (Required for Section A)
        public string? ImageUrl { get; set; }

        // Navigation property for Deletion Restriction (Section B Requirement)
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}