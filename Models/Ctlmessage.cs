using System;
using System.Collections.Generic;

namespace TNPWMSWEB.Models;

public partial class Ctlmessage
{
    public string MsgCode { get; set; } = null!;

    public string? MsgShotTh { get; set; }

    public string? MsgShotEn { get; set; }

    public string? MsgLangTh { get; set; }

    public string? MsgLangEn { get; set; }

    public string? MsgType { get; set; }

    public string? DeviceType { get; set; }
}
