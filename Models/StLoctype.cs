using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class StLoctype
{
    public int LocType { get; set; }

    public string? LocTypeName { get; set; }

    public string LocCat { get; set; } = null!;
}
