using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class Car
{
    public string CarVin { get; set; } = null!;

    public string Color { get; set; } = null!;

    public decimal Price { get; set; }

    public int YearOfManufacture { get; set; }

    public int ModelId { get; set; }

    public int StatusId { get; set; }

    public byte[]? Photo { get; set; }

    public virtual Model Model { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual CarStatus Status { get; set; } = null!;
}
