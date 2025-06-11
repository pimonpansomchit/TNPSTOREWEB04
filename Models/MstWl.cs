using System;
using System.Collections.Generic;

namespace TNPWMSWEB.Models;

public partial class MstWl
{
    public string Dcid { get; set; } = null!;

    public decimal WlKey { get; set; }

    public string WlId { get; set; } = null!;

    public string? WlType { get; set; }

    public string? WlName { get; set; }

    public string? WlAddrL1 { get; set; }

    public string? WlAddrL2 { get; set; }

    public string? WlAddrL3 { get; set; }

    public string? WlState { get; set; }

    public string? WlZipCode { get; set; }

    public string? WlCounty { get; set; }

    public string? WlTelno { get; set; }

    public string? WlTexId { get; set; }

    public string? ContName { get; set; }

    public string? ContTelno { get; set; }

    public string? ContEmail { get; set; }

    public int? WlVat { get; set; }

    public decimal? CustKey { get; set; }

    public string? Createuser { get; set; }

    public DateTime? Createdtime { get; set; }

    public string? Changeuser { get; set; }

    public DateTime? Changedtime { get; set; }

    public decimal? Logid { get; set; }

    public string? EnableFlg { get; set; }
}
