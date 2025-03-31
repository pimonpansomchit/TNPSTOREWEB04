using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class Locprdd
{
    public string Wlid { get; set; } = null!;

    public string LocId { get; set; } = null!;

    public string? GroupId { get; set; }

    public decimal ProdKey { get; set; }

    public string ProdId { get; set; } = null!;

    public string? ProdName { get; set; }

    public decimal BarcodeKey { get; set; }

    public string Barcode { get; set; } = null!;

    public int Unit { get; set; }

    public string Unittype { get; set; } = null!;

    public string? Unitname { get; set; }

    public string LocCode { get; set; } = null!;

    public string Loccat { get; set; } = null!;

    public int LocType { get; set; }

    public string? LocTypeName { get; set; }

    public string? Loczone { get; set; }

    public string? Locrack { get; set; }

    public string? Loclev { get; set; }

    public string? Locpos { get; set; }

    public int? StackPos { get; set; }

    public int? StackDep { get; set; }

    public int? StackHgt { get; set; }

    public int? Casecap { get; set; }

    public int? Minsel { get; set; }

    public int? Rplnamt { get; set; }

    public int? BohQty { get; set; }

    public int? Locstatus { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? CreateDtime { get; set; }

    public string? ChangeUser { get; set; }

    public DateTime? ChangeDtime { get; set; }

    public decimal? Logid { get; set; }
}
