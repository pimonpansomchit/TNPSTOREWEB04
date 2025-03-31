using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class TrnoutofStockD
{
    public string Wlid { get; set; } = null!;

    public DateOnly TranDate { get; set; }

    public string RecNo { get; set; } = null!;

    public int RecdNo { get; set; }

    public string? GroupId { get; set; }

    public string Barcode { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public string? ItemName { get; set; }

    public int? ItemQty { get; set; }

    public int? Unit { get; set; }

    public string? UnitName { get; set; }

    public string? UnitType { get; set; }

    public string? LocIdto { get; set; }

    public string? LocIdfm { get; set; }

    public int? TranType { get; set; }

    public string? TranStatus { get; set; }

    public int? TranFlag { get; set; }

    public string? CreateUser { get; set; }

    public decimal Logid { get; set; }

    public DateTime? CreateDtime { get; set; }
}
