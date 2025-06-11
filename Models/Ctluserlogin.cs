using System;
using System.Collections.Generic;

namespace TNPWMSWEB.Models;

public partial class Ctluserlogin
{
    public string Dcid { get; set; } = null!;

    public string Whid { get; set; } = null!;

    public decimal UserKey { get; set; }

    public string UserName { get; set; } = null!;

    public string? AssgNo { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string ClassId { get; set; } = null!;

    public string LoginType { get; set; } = null!;

    public string SessionType { get; set; } = null!;

    public string? CurrPasswd { get; set; }

    public string? OldPasswd { get; set; }

    public string ResetPasswdCtl { get; set; } = null!;

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public int? Logintimeout { get; set; }

    public string? LangId { get; set; }

    public string Createuser { get; set; } = null!;

    public DateTime Createdtime { get; set; }

    public string Changeuser { get; set; } = null!;

    public DateTime Changedtime { get; set; }

    public string? WhName { get; set; }
}
