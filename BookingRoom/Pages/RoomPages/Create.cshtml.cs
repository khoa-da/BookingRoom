using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAcessObject.Models;

namespace BookingRoom.Pages.RoomPages
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
        ViewData["CategoryId"] = new SelectList(_context.RoomCategories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
