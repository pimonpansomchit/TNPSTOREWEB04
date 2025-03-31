using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class Mstlocation
{
    public string Wlid { get; set; } = null!;

    public string? Ailse { get; set; }

    public string? Rack { get; set; }

    public string? Level { get; set; }

    public string? Position { get; set; }

    public string Locid { get; set; } = null!;

    public int? Locseq { get; set; }

    public string? Side { get; set; }

    public short? StackPos { get; set; }

    public short? StackPosHgt { get; set; }

    public short? StackPosDep { get; set; }

    public short? StackPosWid { get; set; }

    public int? Ti { get; set; }

    public int? Hi { get; set; }

    public string? Handing { get; set; }

    public string? Loccat { get; set; }

    public int? Loctype { get; set; }

    public int? LocStatus { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? CreateDtime { get; set; }

    public string? ChangeUser { get; set; }

    public DateTime? ChangeDtime { get; set; }

    public string? Updatedocno { get; set; }
}
