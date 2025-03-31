using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class MstBarcode
{
    public string Dcid { get; set; } = null!;

    public string Whid { get; set; } = null!;

    public decimal ProdKey { get; set; }

    public string ProdId { get; set; } = null!;

    public int PrddId { get; set; }

    public decimal BarcodeKey { get; set; }

    public string ProdBarcode { get; set; } = null!;

    public string UnitName { get; set; } = null!;

    public decimal ProdBarUnit { get; set; }

    public string? ProdBarType { get; set; }

    public string? Createuser { get; set; }

    public DateTime? Createdtime { get; set; }

    public string? Changeuser { get; set; }

    public DateTime? Changedtime { get; set; }

    public decimal? Logid { get; set; }

    public decimal? PrddWid { get; set; }

    public decimal? PrddWgt { get; set; }

    public decimal? PrddLen { get; set; }

    public decimal? PrddHgt { get; set; }

    public decimal? PrddCube { get; set; }

    public string? BarcodeStatus { get; set; }
}
