using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class Cooperative
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
