using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class StWlprice
{
    public string WlCode { get; set; } = null!;

    public string ArprbCode { get; set; } = null!;

    public string? DbNameString { get; set; }
}
