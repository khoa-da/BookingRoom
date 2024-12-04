﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcessObject.Models;

namespace BookingRoom.Pages.RoomPages
{
    public class IndexModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public IndexModel(DataAcessObject.Models.BookingRoomContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Room = await _context.Rooms
                .Include(r => r.Category).ToListAsync();
        }
    }
}
