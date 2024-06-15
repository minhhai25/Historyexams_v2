using System;
using System.Collections.Generic;

namespace Historyexams.Models;

public partial class Traloi
{
    public long Id { get; set; }

    public long? Iddtch { get; set; }

    public string PaChon { get; set; }

    public int? Lan { get; set; }

    public DateTime? Ngaythi { get; set; }

    public int? Tkdtid { get; set; }

    public string PaDung { get; set; }

    public virtual Dtch IddtchNavigation { get; set; }

    public virtual Tkdt Tkdt { get; set; }
}
