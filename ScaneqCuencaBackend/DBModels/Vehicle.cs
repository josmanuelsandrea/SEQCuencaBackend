using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class Vehicle
{
    public int Id { get; set; }

    public string? Model { get; set; }

    public string? Vin { get; set; }

    public string? Color { get; set; }

    public string? Engine { get; set; }

    public int? Year { get; set; }

    public string? Gearbox { get; set; }

    public string? AxleGear { get; set; }

    public decimal? RearAxleGearRatio { get; set; }

    public int? CustomerId { get; set; }

    public string Plate { get; set; } = null!;

    public string? Type { get; set; }

    public bool? MaintenanceAgreement { get; set; }

    public int? CooperativeId { get; set; }

    public int? FleetNumber { get; set; }

    public virtual ICollection<BusOrder> BusOrders { get; set; } = new List<BusOrder>();

    public virtual Cooperative? Cooperative { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<MaintenanceRegistry> MaintenanceRegistries { get; set; } = new List<MaintenanceRegistry>();

    public virtual ICollection<Notice> Notices { get; set; } = new List<Notice>();
}
