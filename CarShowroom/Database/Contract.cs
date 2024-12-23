using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class Contract
{
    public int ContractId { get; set; }

    public DateTime? DateCreate { get; set; }

    public DateTime? DateOfTransaction { get; set; }

    public int? PaymentTypeId { get; set; }

    public virtual Request ContractNavigation { get; set; } = null!;

    public virtual PaymentType? PaymentType { get; set; }
}
