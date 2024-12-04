using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAcessObject.Models;

namespace BookingRoom.Pages.TimeSlotPages
{
    public class EditModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public EditModel(DataAcessObject.Models.BookingRoomContext context)
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

            var timeslot =  await _context.TimeSlots.FirstOrDefaultAsync(m => m.SlotId == id);
            if (timeslot == null)
            {
                return NotFound();
            }
            TimeSlot = timeslot;
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

            _context.Attach(TimeSlot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeSlotExists(TimeSlot.SlotId))
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

        private bool TimeSlotExists(int id)
        {
            return _context.TimeSlots.Any(e => e.SlotId == id);
        }
    }
}
