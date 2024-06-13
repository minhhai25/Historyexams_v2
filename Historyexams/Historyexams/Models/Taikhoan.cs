using System;
using System.Collections.Generic;

namespace Historyexams.Models;

public partial class Taikhoan
{
    public int Id { get; set; }

    public string Mataikhoan { get; set; }

    public string Hoten { get; set; }

    public string Matkhau { get; set; }

    public string Dienthoai { get; set; }

    public string Email { get; set; }

    public string Gioitinh { get; set; }

    public string Diachi { get; set; }

    public DateTime? Ngaytao { get; set; }

    public string Nguoitao { get; set; }

    public DateTime? Ngaysua { get; set; }

    public bool Isactive { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Tkdt> Tkdts { get; set; } = new List<Tkdt>();
}
