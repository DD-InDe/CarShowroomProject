using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class Request
{
    public int RequestId { get; set; }

    public DateTime? DateCreate { get; set; }

    public int? StatusId { get; set; }

    public string? CarId { get; set; }

    public int? CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Car? Car { get; set; }

    public virtual Contract? Contract { get; set; }

    public virtual User? Customer { get; set; }

    public virtual User? Employee { get; set; }

    public virtual RequestStatus? Status { get; set; }
}
