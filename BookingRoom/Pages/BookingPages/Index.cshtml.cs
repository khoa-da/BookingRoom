using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcessObject.Models;

namespace BookingRoom.Pages.BookingPages
{
    public class IndexModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public IndexModel(DataAcessObject.Models.BookingRoomContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Slot)
                .Include(b => b.User).ToListAsync();
        }
    }
}
