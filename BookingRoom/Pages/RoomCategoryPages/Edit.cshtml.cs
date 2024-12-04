using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAcessObject.Models;

namespace BookingRoom.Pages.RoomCategoryPages
{
    public class EditModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public EditModel(DataAcessObject.Models.BookingRoomContext context)
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

            var roomcategory =  await _context.RoomCategories.FirstOrDefaultAsync(m => m.CategoryId == id);
            if (roomcategory == null)
            {
                return NotFound();
            }
            RoomCategory = roomcategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RoomCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomCategoryExists(RoomCategory.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoomCategoryExists(int id)
        {
            return _context.RoomCategories.Any(e => e.CategoryId == id);
        }
    }
}
