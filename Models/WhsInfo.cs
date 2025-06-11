using System;
using System.Collections.Generic;

namespace TNPWMSWEB.Models;

public partial class WhsInfo
{
    public string Dcid { get; set; } = null!;

    public string Whid { get; set; } = null!;

    public string? WhsName { get; set; }

    public string WhsActive { get; set; } = null!;

    public string? Createuser { get; set; }

    public DateTime? Createdtime { get; set; }

    public string? Changeuser { get; set; }

    public DateTime? Changedtime { get; set; }
}
