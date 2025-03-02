using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MonitoringApp.Models;

public partial class Applications
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Url { get; set; }

    public DateTime? Createdat { get; set; }

    [JsonIgnore]
    public virtual ICollection<Servicehealthlogs> Servicehealthlogs { get; set; } = new List<Servicehealthlogs>();
}
