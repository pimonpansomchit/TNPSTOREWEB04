using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class Trnreplenish
{
    public string Wlid { get; set; } = null!;

    public string RecNo { get; set; } = null!;

    public string DocNo { get; set; } = null!;

    public int DocId { get; set; }

    public DateOnly TranDate { get; set; }

    public string? TranCode { get; set; }

    public decimal? TotalItem { get; set; }

    public decimal? TotalItemRep { get; set; }

    public string? TranStatus { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? CreateDtime { get; set; }

    public string? ChangeUser { get; set; }

    public DateTime? ChangeDtime { get; set; }

    public string? Logid { get; set; }
}
