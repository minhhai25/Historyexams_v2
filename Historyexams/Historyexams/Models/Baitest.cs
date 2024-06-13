using System;
using System.Collections.Generic;

namespace Historyexams.Models;

public partial class Baitest
{
    public int Id { get; set; }

    public string Mabaitest { get; set; }

    public string Tenbaitest { get; set; }

    public string Mota { get; set; }

    public int? Iduser { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string Nguoitao { get; set; }

    public bool Isactive { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Dethi> Dethis { get; set; } = new List<Dethi>();
}
