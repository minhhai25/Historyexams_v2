using System;
using System.Collections.Generic;

namespace Historyexams.Models;

public partial class Cauhoi
{
    public int Id { get; set; }

    public string Macauhoi { get; set; }

    public int Idmucdo { get; set; }

    public int Idchuong { get; set; }

    public string Noidung { get; set; }

    public string PaA { get; set; }

    public string PaB { get; set; }

    public string PaC { get; set; }

    public string PaD { get; set; }

    public string PaDung { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string Nguoitao { get; set; }

    public bool Isactive { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Dtch> Dtches { get; set; } = new List<Dtch>();

    public virtual Chuong IdchuongNavigation { get; set; }

    public virtual Mucdo IdmucdoNavigation { get; set; }
}
