using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly DataAcessObject.Models.BookingRoomContext _context;

        public LoginModel(DataAcessObject.Models.BookingRoomContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            ErrorMessage = string.Empty;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == Input.Email && u.PasswordHash == Input.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("Role", user.Role);
                // Đăng nhập thành công, chuyển hướng đến trang khác
                return RedirectToPage("/Index");
            }

            // Nếu đăng nhập thất bại
            ErrorMessage = "Invalid email or password.";
            return Page();
        }
    }
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
