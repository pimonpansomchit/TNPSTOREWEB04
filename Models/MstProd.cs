using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class MstProd
{
    public string Dcid { get; set; } = null!;

    public string Whid { get; set; } = null!;

    public decimal ProdKey { get; set; }

    public string ProdId { get; set; } = null!;

    public decimal? VenKey { get; set; }

    public string? VenProdId { get; set; }

    public string? ProdStatus { get; set; }

    public string? LongDescription { get; set; }

    public string? ShortDescription { get; set; }

    public int? ProdVat { get; set; }

    public string? Createuser { get; set; }

    public DateTime? Createdtime { get; set; }

    public string? Changeuser { get; set; }

    public DateTime? Changedtime { get; set; }

    public string? CodeDateFlg { get; set; }

    public int? CodeDateExp { get; set; }

    public int? CodeDateIncr { get; set; }

    public int? CodeDateMax { get; set; }

    public int? StoreTi { get; set; }

    public int? StoreHi { get; set; }

    public decimal? Logid { get; set; }
}
