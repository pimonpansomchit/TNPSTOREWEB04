using System;
using System.Collections.Generic;

namespace TNPWMSWEB.Models;

public partial class MstBranch
{
    public string Dcid { get; set; } = null!;

    public decimal BrKey { get; set; }

    public string BrId { get; set; } = null!;

    public string BrType { get; set; } = null!;

    public string? BrName { get; set; }

    public string? BrAddrL1 { get; set; }

    public string? BrAddrL2 { get; set; }

    public string? BrAddrL3 { get; set; }

    public string? BrState { get; set; }

    public string? BrZipCode { get; set; }

    public string? BrCounty { get; set; }

    public string? BrTelno { get; set; }

    public string? BrTexId { get; set; }

    public string? ContName { get; set; }

    public string? ContTelno { get; set; }

    public string? ContEmail { get; set; }

    public int? BrVat { get; set; }

    public string? Createuser { get; set; }

    public DateTime? Createdtime { get; set; }

    public string? Changeuser { get; set; }

    public DateTime? Changedtime { get; set; }

    public decimal? Logid { get; set; }

    public string? EnableFlg { get; set; }
}
