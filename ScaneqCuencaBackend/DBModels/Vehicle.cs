using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class Vehicle
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Vin { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Engine { get; set; } = null!;

    public int Year { get; set; }

    public string? Gearbox { get; set; }

    public string? AxleGear { get; set; }

    public decimal? RearAxleGearRatio { get; set; }

    public int CustomerId { get; set; }

    public string Plate { get; set; } = null!;

    public virtual ICollection<BusOrder> BusOrders { get; set; } = new List<BusOrder>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<TruckOrder> TruckOrders { get; set; } = new List<TruckOrder>();
}
