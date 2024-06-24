using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class Mechanic
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public DateOnly CreatedDate { get; set; }

    public virtual ICollection<MechanicsOrder> MechanicsOrders { get; set; } = new List<MechanicsOrder>();
}
