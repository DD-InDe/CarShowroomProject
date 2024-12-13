using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class Passport
{
    public int PassportId { get; set; }

    public int Serial { get; set; }

    public int Number { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime IssueDate { get; set; }

    public string IssuedBy { get; set; } = null!;

    public string IssuedPlace { get; set; } = null!;

    public virtual User? User { get; set; }
}
