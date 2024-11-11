using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class BusOrder
{
    public int Id { get; set; }

    public int Fid { get; set; }

    public DateOnly DateField { get; set; }

    public int CustomerId { get; set; }

    public string? Description { get; set; }

    public bool Iswarranty { get; set; }

    public int Kilometers { get; set; }

    public bool Isarchived { get; set; }

    public int Storedvolume { get; set; }

    public int? VehicleId { get; set; }

    public string? VehicleType { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<MaintenanceRegistry> MaintenanceRegistries { get; set; } = new List<MaintenanceRegistry>();

    public virtual ICollection<SpareOrder> SpareOrders { get; set; } = new List<SpareOrder>();

    public virtual Vehicle? Vehicle { get; set; }
}
