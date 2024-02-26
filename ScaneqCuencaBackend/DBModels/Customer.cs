using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
