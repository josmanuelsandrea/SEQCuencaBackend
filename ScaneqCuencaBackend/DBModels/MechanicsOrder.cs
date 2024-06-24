using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class MechanicsOrder
{
    public int Id { get; set; }

    public int WorkOrderId { get; set; }

    public string WorkOrderType { get; set; } = null!;

    public int MechanicId { get; set; }

    public virtual Mechanic Mechanic { get; set; } = null!;
}
