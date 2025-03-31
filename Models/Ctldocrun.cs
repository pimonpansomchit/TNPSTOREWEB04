using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class Ctldocrun
{
    public string Dcid { get; set; } = null!;

    public string Whid { get; set; } = null!;

    public string DocType { get; set; } = null!;

    public string YearNum { get; set; } = null!;

    public string MonthNum { get; set; } = null!;

    public int? CurrNum { get; set; }

    public string? NumDigit { get; set; }

    public string? HeadCharacter { get; set; }

    public string? SeparatorChar { get; set; }

    public string? FormatDoc { get; set; }

    public string? DocNo { get; set; }

    public string? FormName { get; set; }

    public string? Createuser { get; set; }

    public DateTime? Createdtime { get; set; }

    public string? Changeuser { get; set; }

    public DateTime? Changedtime { get; set; }

    public int? DocId { get; set; }

    public string Wlid { get; set; } = null!;
}
