/* <!--CODE ATTRIBUTION-->

<!--TITLE: (EVENTEASE)-->

<!--AUTHOR: ( GOUWELOOS , MICK )-->

<!--DATE: ( 15/04/2026 )-->

<!--VERSION: (FIREST EDITION) -->

<!--AVAILABLE: (https://advtechonline.sharepoint.com/:w:/r/sites/TertiaryStudents/_layouts/15/Doc.aspx?sourcedoc=%7B9C23B0F8-6BED-497E-B60C-1D56E59BEDAB%7D&file=PROG6221_MO.docx&action=default&mobileredirect=true)-->    
*/
// Section C: Enhanced Display and Search
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEase.Data;
using EventEase.Models;

namespace EventEase.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Section C: Updated Index with Search Functionality
        public async Task<IActionResult> Index(string searchString)
        {
            // Keep the search text in the box so it doesn't disappear
            ViewData["CurrentFilter"] = searchString;

            // Start with the 'Consolidated' query (joining Events and Venues)
            var bookingsQuery = _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .AsQueryable();

            // If the user typed something in the search box, filter the results
            if (!String.IsNullOrEmpty(searchString))
            {
                bookingsQuery = bookingsQuery.Where(b =>
                    b.CustomerName.Contains(searchString) ||
                    b.Event.EventName.Contains(searchString));
            }

            return View(await bookingsQuery.ToListAsync());
        }

        public IActionResult Create()
        {
            // Populates the dropdown lists with Names for the View
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
            return View();
        }

        // Section B: Double Booking Validation Logic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Logic to check if the Venue is already booked on that specific date
                bool isDoubleBooked = await _context.Bookings.AnyAsync(b =>
                    b.VenueId == booking.VenueId &&
                    b.BookingDate.Date == booking.BookingDate.Date);

                if (isDoubleBooked)
                {
                    ModelState.AddModelError("BookingDate", "This venue is already booked for the selected date.");

                    ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
                    ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);

                    return View(booking);
                }

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            return View(booking);
        }
    }
}