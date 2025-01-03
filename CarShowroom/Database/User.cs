﻿using System;
using System.Collections.Generic;

namespace CarShowroom.Database;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public int RoleId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Request> RequestCustomers { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestEmployees { get; set; } = new List<Request>();

    public virtual Role Role { get; set; } = null!;

    public virtual Passport Passport { get; set; } = null!;
}
