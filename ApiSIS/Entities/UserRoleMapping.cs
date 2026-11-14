using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class UserRoleMapping
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual MsUser User { get; set; } = null!;
}
