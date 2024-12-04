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
    public class DetailsModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public DetailsModel(DataAcessObject.Models.BookingRoomContext context)
        {
            _context = context;
        }

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
    }
}
