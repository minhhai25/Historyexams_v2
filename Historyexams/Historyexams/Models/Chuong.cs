using System;
using System.Collections.Generic;

namespace Historyexams.Models;

public partial class Chuong
{
    public int Id { get; set; }

    public string Machuong { get; set; }

    public string Tenchuong { get; set; }

    public string Mota { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string Nguoitao { get; set; }

    public bool Isactive { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Cauhoi> Cauhois { get; set; } = new List<Cauhoi>();

    public virtual ICollection<Dethi> Dethis { get; set; } = new List<Dethi>();
}
