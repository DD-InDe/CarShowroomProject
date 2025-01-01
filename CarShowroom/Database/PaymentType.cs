using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class PaymentType
{
    public int PaymentTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
