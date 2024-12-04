using System;
using System.Collections.Generic;

namespace DataAcessObject.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int? CategoryId { get; set; }

    public string RoomName { get; set; } = null!;

    public bool? IsVip { get; set; }

    public decimal BasePrice { get; set; }

    public decimal? VipPrice { get; set; }

    public int TotalCapacity { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual RoomCategory? Category { get; set; }
}
