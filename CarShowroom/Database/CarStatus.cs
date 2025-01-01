using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class CarStatus
{
    public int CarStatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
