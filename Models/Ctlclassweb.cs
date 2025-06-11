using System;
using System.Collections.Generic;

namespace TNPWMSWEB.Models;

public partial class Ctlclassweb
{
    public string Classid { get; set; } = null!;

    public int Menukey { get; set; }

    public short Menutype { get; set; }

    public int Menuseq { get; set; }

    public string? MenuId { get; set; }

    public string? ThaiName { get; set; }

    public string? EngName { get; set; }

    public string? Urlname { get; set; }

    public string Actionflg { get; set; } = null!;

    public int ParentKey { get; set; }

    public int RootKey { get; set; }

    public string? Viewid { get; set; }

    public string? Controllerid { get; set; }

    public string? Actionid { get; set; }

    public string? Icons { get; set; }
}
