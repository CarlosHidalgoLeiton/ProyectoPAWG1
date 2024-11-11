﻿using System;
using System.Collections.Generic;

namespace PAWG1.Models.EFModels;

public partial class Role
{
    public int IdRole { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> IdUsers { get; set; } = new List<User>();
}