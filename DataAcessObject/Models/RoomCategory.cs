using System;
using System.Collections.Generic;

namespace DataAcessObject.Models;

public partial class RoomCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public int MaxCapacity { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
