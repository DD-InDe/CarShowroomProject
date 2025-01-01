using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class ModelGeneration
{
    public int ModelGenerationId { get; set; }

    public int Number { get; set; }

    public DateTime ReleaseDate { get; set; }

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
}
