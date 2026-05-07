using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Data;
using EventEase.Models;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace EventEase.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public VenuesController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Section C: Search Feature
        public async Task<IActionResult> Index(string searchString)
        {
            // Changed 'VenueName' to 'Name' to match your Venue.cs model
            var venues = from v in _context.Venues select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                venues = venues.Where(s => s.Name.Contains(searchString));
            }

            return View(await venues.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        // Section A: Azurite Image Upload (25 Marks)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string connectionString = _configuration.GetValue<string>("AzureStorage");
                    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("venue-images");

                    await containerClient.CreateIfNotExistsAsync();

                    BlobClient blobClient = containerClient.GetBlobClient(imageFile.FileName);
                    await blobClient.UploadAsync(imageFile.OpenReadStream(), true);

                    venue.ImageUrl = blobClient.Uri.ToString();
                }

                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // Section B: Restriction Logic (25 Marks)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues
                .Include(v => v.Bookings)
                .FirstOrDefaultAsync(m => m.VenueId == id);

            if (venue != null)
            {
                if (venue.Bookings != null && venue.Bookings.Any())
                {
                    TempData["Error"] = "RESTRICTION: This venue has active bookings and cannot be deleted.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}