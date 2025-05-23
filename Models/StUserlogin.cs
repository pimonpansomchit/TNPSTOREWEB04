﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TNPSTOREWEB.Models;

public partial class StUserlogin
{
    [Required(ErrorMessage = "ไม่สามารถใส่ค่าว่างได้ ใส่ข้อมูลให้ครบถ้วน")]
    public string UserName { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? ClassId { get; set; }

    [Required(ErrorMessage = "ไม่สามารถใส่ค่าว่างได้ ใส่ข้อมูลให้ครบถ้วน")]
    public string? CurrPasswd { get; set; }

    public string? OldPasswd { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? CreateDtime { get; set; }

    public string? ChangeUser { get; set; }

    public DateTime? ChangeDtime { get; set; }

    public string? SessionType { get; set; }

    public string Whid { get; set; } = null!;

    public decimal WlKey { get; set; }

    public string WlCode { get; set; } = null!;

    public string? WlName { get; set; }

    public string? ArprbCode { get; set; }

    public string? TbNewlabel { get; set; }

    public string? DbName { get; set; }

    public string? Language { get; set; }
}
