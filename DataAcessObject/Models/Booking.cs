using System;
using System.Collections.Generic;

namespace DataAcessObject.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int RoomId { get; set; }

    public int SlotId { get; set; }

    public DateOnly BookingDate { get; set; }

    public int UserId { get; set; }

    public string? Status { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? BookingTime { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentTransactionId { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual TimeSlot Slot { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
