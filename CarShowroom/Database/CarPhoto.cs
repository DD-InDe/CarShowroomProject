using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class CarPhoto
{
    public int CarPhotoId { get; set; }

    public byte[] Photo { get; set; } = null!;

    public string CarVin { get; set; } = null!;

    public virtual Car CarVinNavigation { get; set; } = null!;
}
