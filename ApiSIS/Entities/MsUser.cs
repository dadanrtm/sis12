using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class MsUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AbsensiSiswa> AbsensiSiswas { get; set; } = new List<AbsensiSiswa>();

    public virtual ICollection<LogAbsensi> LogAbsensis { get; set; } = new List<LogAbsensi>();

    public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
}
