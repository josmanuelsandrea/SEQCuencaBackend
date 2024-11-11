using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class SparePart
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<SpareRegister> SpareRegisters { get; set; } = new List<SpareRegister>();
}
