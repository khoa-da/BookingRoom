using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAcessObject.Models;
using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Pages.BookingRoomUser
{
    public class IndexModel : PageModel
    {
        private readonly BookingRoomContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(BookingRoomContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public RoomSearchModel SearchCriteria { get; set; }

        public List<Room> AvailableRooms { get; set; } = new List<Room>();

        public void OnGet()
        {
            // Initial page load, no search performed
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Search for available rooms based on criteria
                AvailableRooms = await SearchRoomsAsync();

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching for rooms");
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi tìm kiếm phòng.");
                return Page();
            }
        }

        private async Task<List<Room>> SearchRoomsAsync()
        {
            // Base query for rooms
            var query = _context.Rooms.AsQueryable();

            // Filter by category if specified
            if (SearchCriteria.CategoryId.HasValue)
            {
                query = query.Where(r => r.CategoryId == SearchCriteria.CategoryId);
            }

            // Filter by capacity if specified
            if (SearchCriteria.Capacity.HasValue)
            {
                query = query.Where(r => r.TotalCapacity >= SearchCriteria.Capacity);
            }

            // Check room availability
            if (SearchCriteria.BookingDate.HasValue && SearchCriteria.TimeSlotId.HasValue)
            {
                // Find rooms that are NOT booked in the specified date and time slot
                query = query.Where(room =>
                    !_context.Bookings.Any(booking =>
                        booking.RoomId == room.RoomId &&
                        booking.BookingDate == SearchCriteria.BookingDate &&
                        booking.Slot.SlotId == SearchCriteria.TimeSlotId
                    )
                );
            }

            // Additional filtering for room status if needed
            query = query.Where(r => r.Status == "Trống");

            // Return the list of available rooms
            return await query
                .Select(r => new Room
                {
                    RoomId = r.RoomId,
                    RoomName = r.RoomName,
                    CategoryId = (int)r.CategoryId,
                    IsVip = (bool)r.IsVip,
                    BasePrice = r.BasePrice,
                    VipPrice = (decimal)r.VipPrice,
                    TotalCapacity = r.TotalCapacity,
                    Status = r.Status
                })
                .ToListAsync();
        }
    }

    public class RoomSearchModel
    {
        [Range(1, 2, ErrorMessage = "Vui lòng chọn loại phòng")]
        public int? CategoryId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng chọn ngày")]
        public DateOnly? BookingDate { get; set; }

        [Range(1, 5, ErrorMessage = "Vui lòng chọn khung giờ")]
        public int? TimeSlotId { get; set; }

        [Range(1, 10, ErrorMessage = "Số lượng người không hợp lệ")]
        public int? Capacity { get; set; }
    }

    // DTO for room display
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int CategoryId { get; set; }
        public bool IsVip { get; set; }
        public decimal BasePrice { get; set; }
        public decimal VipPrice { get; set; }
        public int TotalCapacity { get; set; }
        public string Status { get; set; }
    }
}