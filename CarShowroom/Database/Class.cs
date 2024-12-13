using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class Class
{
    public int ClassId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
