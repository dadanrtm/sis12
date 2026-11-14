using System;
using System.Collections.Generic;

namespace ApiSIS.Entities;

public partial class SettingAbsensi
{
    public int IdSetting { get; set; }

    public string SettingKey { get; set; } = null!;

    public string? SettingValue { get; set; }

    public string? Deskripsi { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
