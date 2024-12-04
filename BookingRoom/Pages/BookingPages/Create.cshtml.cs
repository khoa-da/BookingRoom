using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAcessObject.Models;

namespace BookingRoom.Pages.BookingPages
{
    public class CreateModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public CreateModel(DataAcessObject.Models.BookingRoomContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomName");
        ViewData["SlotId"] = new SelectList(_context.TimeSlots, "SlotId", "SlotId");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
