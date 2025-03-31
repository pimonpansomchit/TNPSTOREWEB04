using System;
using System.Collections.Generic;

namespace TNPSTOREWEB.Models;

public partial class SysDatalog
{
    public string Whid { get; set; } = null!;

    public string WlCode { get; set; } = null!;

    public string ServerIp { get; set; } = null!;

    public DateTime LoginDtime { get; set; }

    public DateTime? LogoutDtime { get; set; }

    public string? UserLogin { get; set; }

    public string IpAddress { get; set; } = null!;

    public string? HostName { get; set; }

    public string? Identify { get; set; }

    public string? BrowserType { get; set; }

    public short? MobileDevice { get; set; }

    public string? SessionType { get; set; }

    public string? TranLog { get; set; }

    public decimal LogId { get; set; }
}
