using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class Notice
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public DateOnly NoticeDate { get; set; }

    public string? Description { get; set; }

    public string? Severity { get; set; }

    public bool Resolved { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
