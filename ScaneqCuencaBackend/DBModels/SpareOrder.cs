using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class SpareOrder
{
    public int Id { get; set; }

    public int? BusOrderFk { get; set; }

    public int? CustomerFk { get; set; }

    public bool Isclosed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public virtual BusOrder? BusOrder { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<SpareRegister> SpareRegisters { get; set; } = new List<SpareRegister>();
}
