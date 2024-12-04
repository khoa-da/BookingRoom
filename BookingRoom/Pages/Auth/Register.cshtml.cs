using DataAcessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingRoom.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public RegisterModel(DataAcessObject.Models.BookingRoomContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Auth/Login");
        }
    }
}
