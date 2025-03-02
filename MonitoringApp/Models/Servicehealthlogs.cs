using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MonitoringApp.Models;

public partial class Servicehealthlogs
{
    public int Id { get; set; }

    public int? Applicationid { get; set; }

    public string Status { get; set; } = null!;

    public int? Responsetimems { get; set; }

    public string? Errormessage { get; set; }

    public DateTime? Createdat { get; set; }

    [JsonIgnore]
    public virtual Applications? Application { get; set; }
}
