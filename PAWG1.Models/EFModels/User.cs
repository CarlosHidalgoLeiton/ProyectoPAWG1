﻿using System;
using System.Collections.Generic;

namespace PAWG1.Models.EFModels;

public partial class User
{
    public int IdUser { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool State { get; set; }

    public int IdRole { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Component> Widgets { get; set; } = new List<Component>();
}
