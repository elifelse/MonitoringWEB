using System;
using System.Collections.Generic;

namespace MonitoringApp.Persistence.Entities;

public partial class Applications
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Url { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Errors> Errors { get; set; } = new List<Errors>();

    public virtual ICollection<Jobs> Jobs { get; set; } = new List<Jobs>();

    public virtual ICollection<Servicehealthlogs> Servicehealthlogs { get; set; } = new List<Servicehealthlogs>();
}
