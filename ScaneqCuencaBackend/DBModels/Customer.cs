using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? IdRucNumber { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<BusOrder> BusOrders { get; set; } = new List<BusOrder>();

    public virtual ICollection<SpareOrder> SpareOrders { get; set; } = new List<SpareOrder>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
