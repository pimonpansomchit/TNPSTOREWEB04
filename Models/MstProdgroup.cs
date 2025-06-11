using System;
using System.Collections.Generic;

namespace TNPWMSWEB.Models;

public partial class MstProdgroup
{
    public int ProdKey { get; set; }

    public string ProdId { get; set; } = null!;

    public int GroupKey { get; set; }

    public string? GroupId { get; set; }

    public string? GroupName { get; set; }

    public string SubgroupId { get; set; } = null!;

    public string SubgroupName { get; set; } = null!;

    public int SubgroupKey { get; set; }
}
