using System;
using System.Collections.Generic;

namespace MonitoringApp.Persistence.Entities;

public partial class Errors
{
    public int Id { get; set; }

    public int? Applicationid { get; set; }

    public string? Errortype { get; set; }

    public int? Statuscode { get; set; }

    public string? Description { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Applications? Application { get; set; }
}
