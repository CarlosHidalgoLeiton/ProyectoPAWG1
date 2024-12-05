using System;
using System.Collections.Generic;

namespace PAWG1.Models.EFModels;

public partial class Component
{
    public int IdComponent { get; set; }

    public string TypeComponent { get; set; } = null!;

    public int Size { get; set; }

    public string ApiUrl { get; set; } = null!;

    public string? ApiKey { get; set; }

    public string? ApiKeyId { get; set; }

    public DateOnly CreateDate { get; set; }

    public DateOnly UpdateDate { get; set; }

    public string Descrip { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Color { get; set; } = null!;

    public byte[] Simbol { get; set; } = null!;

    public string? Data { get; set; }

    public bool State { get; set; }

    public int IdOwner { get; set; }

    public string AllowedRole { get; set; } = null!;

    public virtual User? IdOwnerNavigation { get; set; } = null!;

    public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
}
