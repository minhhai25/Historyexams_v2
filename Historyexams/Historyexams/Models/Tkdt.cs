using System;
using System.Collections.Generic;

namespace Historyexams.Models;

public partial class Tkdt
{
    public int Id { get; set; }

    public int Idtaikhoan { get; set; }

    public int Iddethi { get; set; }

    public int? Lan { get; set; }

    public string Tyle { get; set; }

    public DateTime? Ngaythi { get; set; }

    public virtual Dethi IddethiNavigation { get; set; }

    public virtual Taikhoan IdtaikhoanNavigation { get; set; }

    public virtual ICollection<Traloi> Tralois { get; set; } = new List<Traloi>();
}
