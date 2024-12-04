using System;
using System.Collections.Generic;

namespace DataAcessObject.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? FullName { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime? RegistrationDate { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
