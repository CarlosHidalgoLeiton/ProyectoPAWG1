using System;
using System.Collections.Generic;

namespace PAWG1.Models.EFModels;

public partial class Status
{
    public int UserId { get; set; }

    public int ComponentId { get; set; }

    public string Type { get; set; } = null!;

    public virtual Component? Component { get; set; } = null!;

    public virtual User? User { get; set; } = null!;
}
