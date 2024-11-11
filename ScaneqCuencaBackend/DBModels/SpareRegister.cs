using System;
using System.Collections.Generic;

namespace ScaneqCuencaBackend.DBModels;

public partial class SpareRegister
{
    public int Id { get; set; }

    public int SpareOrderFk { get; set; }

    public int SpareFk { get; set; }

    public int Quantity { get; set; }

    public DateTime? AddedAt { get; set; }

    public virtual SparePart SparePart { get; set; } = null!;

    public virtual SpareOrder SpareOrderFkNavigation { get; set; } = null!;
}
