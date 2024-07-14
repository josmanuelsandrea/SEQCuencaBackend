using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class MaintenanceRegistry
{
    public int Id { get; set; }

    public int VehicleFkId { get; set; }

    public int OrderFkId { get; set; }

    public DateOnly MaintenanceDate { get; set; }

    public string MaintenanceType { get; set; } = null!;

    public virtual BusOrder OrderFk { get; set; } = null!;

    public virtual Vehicle VehicleFk { get; set; } = null!;
}
