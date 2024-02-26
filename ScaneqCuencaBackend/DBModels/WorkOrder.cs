using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class WorkOrder
{
    public int Uid { get; set; }

    public int Fid { get; set; }

    public DateOnly DateField { get; set; }

    public int CustomerId { get; set; }

    public string? Description { get; set; }

    public decimal Billquantity { get; set; }

    public decimal Labourcost { get; set; }

    public bool Iswarranty { get; set; }

    public int Kilometers { get; set; }

    public bool Isarchived { get; set; }

    public int Storedvolume { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
