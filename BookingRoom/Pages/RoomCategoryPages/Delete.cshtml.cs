using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcessObject.Models;

namespace BookingRoom.Pages.RoomCategoryPages
{
    public class DeleteModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public DeleteModel(DataAcessObject.Models.BookingRoomContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RoomCategory RoomCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomcategory = await _context.RoomCategories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (roomcategory == null)
            {
                return NotFound();
            }
            else
            {
                RoomCategory = roomcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomcategory = await _context.RoomCategories.FindAsync(id);
            if (roomcategory != null)
            {
                RoomCategory = roomcategory;
                _context.RoomCategories.Remove(RoomCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
