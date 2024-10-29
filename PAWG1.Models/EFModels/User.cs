using System;
using System.Collections.Generic;

namespace PAWG1.Models.EFModels;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateOnly CreateDate { get; set; }

    public DateOnly UpdateDate { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Component> Widgets { get; set; } = new List<Component>();
}
