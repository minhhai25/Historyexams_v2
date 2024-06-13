using System;
using System.Collections.Generic;

namespace Historyexams.Models;

public partial class Dtch
{
    public long Id { get; set; }

    public int Iddethi { get; set; }

    public int Idcauhoi { get; set; }

    public virtual Cauhoi IdcauhoiNavigation { get; set; }

    public virtual Dethi IddethiNavigation { get; set; }

    public virtual ICollection<Traloi> Tralois { get; set; } = new List<Traloi>();
}
