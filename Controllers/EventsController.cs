/* <!--CODE ATTRIBUTION-->

<!--TITLE: (EVENTEASE)-->

<!--AUTHOR: ( GOUWELOOS , MICK )-->

<!--DATE: ( 15/04/2026 )-->

<!--VERSION: (FIREST EDITION) -->

<!--AVAILABLE: (https://advtechonline.sharepoint.com/:w:/r/sites/TertiaryStudents/_layouts/15/Doc.aspx?sourcedoc=%7B9C23B0F8-6BED-497E-B60C-1D56E59BEDAB%7D&file=PROG6221_MO.docx&action=default&mobileredirect=true)-->    
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Data;
using EventEase.Models;

namespace EventEase.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }
    }
}