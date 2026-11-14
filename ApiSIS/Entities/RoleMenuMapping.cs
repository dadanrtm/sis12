using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class RoleMenuMapping
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int MenuId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
