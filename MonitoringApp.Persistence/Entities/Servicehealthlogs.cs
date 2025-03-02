using System;
using System.Collections.Generic;

namespace MonitoringApp.Persistence.Entities;

public partial class Servicehealthlogs
{
    public int Id { get; set; }

    public int? Applicationid { get; set; }

    public string Status { get; set; } = null!;

    public int? Responsetimems { get; set; }

    public string? Errormessage { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Statuscode { get; set; }

    public string? Errortype { get; set; }

    public DateTime? Checkedat { get; set; }

    public string? Source { get; set; }

    public virtual Applications? Application { get; set; }
}
