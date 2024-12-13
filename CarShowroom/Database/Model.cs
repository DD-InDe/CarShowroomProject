using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class Model
{
    public int ModelId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Length { get; set; }

    public decimal Width { get; set; }

    public decimal Height { get; set; }

    public int DoorsCount { get; set; }

    public decimal EngineVolume { get; set; }

    public int BrandId { get; set; }

    public int GenerationId { get; set; }

    public int BodyTypeId { get; set; }

    public int EngineTypeId { get; set; }

    public int ClassId { get; set; }

    public virtual BodyType BodyType { get; set; } = null!;

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual Class Class { get; set; } = null!;

    public virtual EngineType EngineType { get; set; } = null!;

    public virtual ModelGeneration Generation { get; set; } = null!;
}
