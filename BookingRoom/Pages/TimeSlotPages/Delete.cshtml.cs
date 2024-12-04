using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcessObject.Models;

namespace BookingRoom.Pages.TimeSlotPages
{
    public class DeleteModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public DeleteModel(DataAcessObject.Models.BookingRoomContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TimeSlot TimeSlot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeslot = await _context.TimeSlots.FirstOrDefaultAsync(m => m.SlotId == id);

            if (timeslot == null)
            {
                return NotFound();
            }
            else
            {
                TimeSlot = timeslot;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeslot = await _context.TimeSlots.FindAsync(id);
            if (timeslot != null)
            {
                TimeSlot = timeslot;
                _context.TimeSlots.Remove(TimeSlot);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
