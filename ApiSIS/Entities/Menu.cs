using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class Menu
{
    public int Id { get; set; }

    public int ParentId { get; set; }

    public string MenuName { get; set; } = null!;

    public string? Path { get; set; }

    public string? Component { get; set; }

    public string? Perms { get; set; }

    public string MenuType { get; set; } = null!;

    public bool Visible { get; set; }

    public int OrderNum { get; set; }

    public string? Icon { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? MenuTitle { get; set; }

    public bool? IsCache { get; set; }

    public bool? IsFrame { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RoleMenuMapping> RoleMenuMappings { get; set; } = new List<RoleMenuMapping>();
}
