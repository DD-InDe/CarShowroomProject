using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class EngineType
{
    public int EngineTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
