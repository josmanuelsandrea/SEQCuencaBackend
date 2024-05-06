using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BusOrder> BusOrders { get; set; } = new List<BusOrder>();

    public virtual ICollection<TruckOrder> TruckOrders { get; set; } = new List<TruckOrder>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
