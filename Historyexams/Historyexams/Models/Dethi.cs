using System;
using System.Collections.Generic;

namespace Historyexams.Models;

public partial class Dethi
{
    public int Id { get; set; }

    public string Madethi { get; set; }

    public string Tendethi { get; set; }

    public string Mota { get; set; }

    public int Idmucdo { get; set; }

    public int Idbaitest { get; set; }

    public int? Idchuong { get; set; }

    public int? Thoigian { get; set; }

    public int? Socauhoi { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string Nguoitao { get; set; }

    public bool Isactive { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Dtch> Dtches { get; set; } = new List<Dtch>();

    public virtual Baitest IdbaitestNavigation { get; set; }

    public virtual Chuong IdchuongNavigation { get; set; }

    public virtual Mucdo IdmucdoNavigation { get; set; }

    public virtual ICollection<Tkdt> Tkdts { get; set; } = new List<Tkdt>();
}
