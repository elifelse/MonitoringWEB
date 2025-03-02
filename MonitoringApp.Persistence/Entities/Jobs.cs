using System;
using System.Collections.Generic;

namespace MonitoringApp.Persistence.Entities;

public partial class Jobs
{
    public int Id { get; set; }

    public int? Applicationid { get; set; }

    public string? Jobname { get; set; }

    public DateTime? Lastrun { get; set; }

    public string? Status { get; set; }

    public string? Errormessage { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Applications? Application { get; set; }
}
