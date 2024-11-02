using System;
using System.Collections.Generic;

namespace PAWG1.Models.EFModels;

public partial class Favorite
{
    public int UserId { get; set; }

    public int WidgetId { get; set; }
}
