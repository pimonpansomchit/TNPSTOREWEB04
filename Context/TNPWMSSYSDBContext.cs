using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TNPWMSWEB.Models;

namespace TNPWMSWEB.Context;

public partial class TNPWMSSYSDBContext : DbContext
{
    public string ConnectDb = Startup.databaseUrlst;
    public TNPWMSSYSDBContext()
    {
    }

    public TNPWMSSYSDBContext(DbContextOptions<TNPWMSSYSDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ctlclass> Ctlclasses { get; set; }

    public virtual DbSet<Ctlclassinfo> Ctlclassinfos { get; set; }

    public virtual DbSet<Ctlclassweb> Ctlclasswebs { get; set; }

    public virtual DbSet<Ctlconfig> Ctlconfigs { get; set; }

    public virtual DbSet<Ctldocrun> Ctldocruns { get; set; }

    public virtual DbSet<Ctlmenu> Ctlmenus { get; set; }

    public virtual DbSet<Ctlmenuweb> Ctlmenuwebs { get; set; }

    public virtual DbSet<Ctlmessage> Ctlmessages { get; set; }

    public virtual DbSet<Ctlstatus> Ctlstatuses { get; set; }

    public virtual DbSet<Ctluserlogin> Ctluserlogins { get; set; }

    public virtual DbSet<SysDatalog> SysDatalogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectDb);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<Ctlclass>(entity =>
        {
            entity.HasKey(e => new { e.Classid, e.Menukey, e.ParentKey }).HasName("PK_st_class");

            entity.ToTable("CTLclass");

            entity.Property(e => e.Classid)
                .HasMaxLength(15)
                .HasColumnName("classid");
            entity.Property(e => e.Menukey).HasColumnName("menukey");
            entity.Property(e => e.ParentKey).HasColumnName("parentKey");
            entity.Property(e => e.Actionflg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("actionflg");
            entity.Property(e => e.Actionid)
                .HasMaxLength(50)
                .HasColumnName("actionid");
            entity.Property(e => e.Controllerid)
                .HasMaxLength(50)
                .HasColumnName("controllerid");
            entity.Property(e => e.EngName).HasMaxLength(200);
            entity.Property(e => e.MenuId)
                .HasMaxLength(10)
                .HasColumnName("menuID");
            entity.Property(e => e.Menuseq).HasColumnName("menuseq");
            entity.Property(e => e.Menutype).HasColumnName("menutype");
            entity.Property(e => e.RootKey).HasColumnName("rootKey");
            entity.Property(e => e.ThaiName).HasMaxLength(200);
            entity.Property(e => e.Urlname)
                .HasMaxLength(200)
                .HasColumnName("urlname");
            entity.Property(e => e.Viewid)
                .HasMaxLength(50)
                .HasColumnName("viewid");
        });

        modelBuilder.Entity<Ctlclassinfo>(entity =>
        {
            entity.HasKey(e => e.ClassCode).HasName("PK_st_classinfo");

            entity.ToTable("CTLclassinfo");

            entity.Property(e => e.ClassCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.ClassName).HasMaxLength(50);
        });

        modelBuilder.Entity<Ctlclassweb>(entity =>
        {
            entity.HasKey(e => new { e.Classid, e.Menukey, e.ParentKey }).HasName("PK_st_classweb");

            entity.ToTable("CTLclassweb");

            entity.Property(e => e.Classid)
                .HasMaxLength(15)
                .HasColumnName("classid");
            entity.Property(e => e.Menukey).HasColumnName("menukey");
            entity.Property(e => e.ParentKey).HasColumnName("parentKey");
            entity.Property(e => e.Actionflg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("actionflg");
            entity.Property(e => e.Actionid)
                .HasMaxLength(50)
                .HasColumnName("actionid");
            entity.Property(e => e.Controllerid)
                .HasMaxLength(50)
                .HasColumnName("controllerid");
            entity.Property(e => e.EngName).HasMaxLength(200);
            entity.Property(e => e.Icons)
                .HasMaxLength(100)
                .HasColumnName("icons");
            entity.Property(e => e.MenuId)
                .HasMaxLength(10)
                .HasColumnName("menuID");
            entity.Property(e => e.Menuseq).HasColumnName("menuseq");
            entity.Property(e => e.Menutype).HasColumnName("menutype");
            entity.Property(e => e.RootKey).HasColumnName("rootKey");
            entity.Property(e => e.ThaiName).HasMaxLength(200);
            entity.Property(e => e.Urlname)
                .HasMaxLength(200)
                .HasColumnName("urlname");
            entity.Property(e => e.Viewid)
                .HasMaxLength(50)
                .HasColumnName("viewid");
        });

        modelBuilder.Entity<Ctlconfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CTLConfig");

            entity.Property(e => e.Dcid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("dcid");
            entity.Property(e => e.MainMstDb)
                .HasMaxLength(50)
                .HasColumnName("MainMstDB");
            entity.Property(e => e.Mainconnect)
                .HasMaxLength(1000)
                .HasColumnName("mainconnect");
            entity.Property(e => e.Maindcid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("maindcid");
            entity.Property(e => e.Mainwhid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("mainwhid");
            entity.Property(e => e.ServerIp)
                .HasMaxLength(50)
                .HasColumnName("ServerIP");
            entity.Property(e => e.Whconnect)
                .HasMaxLength(1000)
                .HasColumnName("whconnect");
            entity.Property(e => e.Whdb)
                .HasMaxLength(50)
                .HasColumnName("WHDB");
            entity.Property(e => e.Whid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("whid");
        });

        modelBuilder.Entity<Ctldocrun>(entity =>
        {
            entity.HasKey(e => new { e.Wlid, e.Dcid, e.Whid, e.DocType, e.YearNum, e.MonthNum }).HasName("PK_docrun");

            entity.ToTable("CTLdocrun");

            entity.Property(e => e.Wlid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("wlid");
            entity.Property(e => e.Dcid)
                .HasMaxLength(10)
                .HasColumnName("dcid");
            entity.Property(e => e.Whid)
                .HasMaxLength(10)
                .HasColumnName("whid");
            entity.Property(e => e.DocType)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("doc_type");
            entity.Property(e => e.YearNum)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("year_num");
            entity.Property(e => e.MonthNum)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("month_num");
            entity.Property(e => e.Changedtime)
                .HasColumnType("datetime")
                .HasColumnName("changedtime");
            entity.Property(e => e.Changeuser)
                .HasMaxLength(15)
                .HasColumnName("changeuser");
            entity.Property(e => e.Createdtime)
                .HasColumnType("datetime")
                .HasColumnName("createdtime");
            entity.Property(e => e.Createuser)
                .HasMaxLength(15)
                .HasColumnName("createuser");
            entity.Property(e => e.CurrNum).HasColumnName("curr_num");
            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.DocNo)
                .HasMaxLength(20)
                .HasColumnName("doc_no");
            entity.Property(e => e.FormName)
                .HasMaxLength(100)
                .HasColumnName("form_name");
            entity.Property(e => e.FormatDoc)
                .HasMaxLength(100)
                .HasColumnName("format_doc");
            entity.Property(e => e.HeadCharacter)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("head_character");
            entity.Property(e => e.NumDigit)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("num_digit");
            entity.Property(e => e.SeparatorChar)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("separator_char");
        });

        modelBuilder.Entity<Ctlmenu>(entity =>
        {
            entity.HasKey(e => new { e.Menukey, e.ParentKey }).HasName("PK_st_menu");

            entity.ToTable("CTLmenu");

            entity.Property(e => e.Menukey)
                .ValueGeneratedOnAdd()
                .HasColumnName("menukey");
            entity.Property(e => e.ParentKey).HasColumnName("parentKey");
            entity.Property(e => e.Actionflg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("actionflg");
            entity.Property(e => e.Actionid)
                .HasMaxLength(50)
                .HasColumnName("actionid");
            entity.Property(e => e.Controllerid)
                .HasMaxLength(50)
                .HasColumnName("controllerid");
            entity.Property(e => e.EngName).HasMaxLength(200);
            entity.Property(e => e.MenuId)
                .HasMaxLength(10)
                .HasColumnName("menuID");
            entity.Property(e => e.Menuseq).HasColumnName("menuseq");
            entity.Property(e => e.Menutype).HasColumnName("menutype");
            entity.Property(e => e.RootKey).HasColumnName("rootKey");
            entity.Property(e => e.ThaiName).HasMaxLength(200);
            entity.Property(e => e.Urlname)
                .HasMaxLength(200)
                .HasColumnName("urlname");
            entity.Property(e => e.Viewid)
                .HasMaxLength(50)
                .HasColumnName("viewid");
        });

        modelBuilder.Entity<Ctlmenuweb>(entity =>
        {
            entity.HasKey(e => new { e.Menukey, e.ParentKey }).HasName("PK_st_menuweb");

            entity.ToTable("CTLmenuweb");

            entity.Property(e => e.Menukey)
                .ValueGeneratedOnAdd()
                .HasColumnName("menukey");
            entity.Property(e => e.ParentKey).HasColumnName("parentKey");
            entity.Property(e => e.Actionflg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("actionflg");
            entity.Property(e => e.Actionid)
                .HasMaxLength(50)
                .HasColumnName("actionid");
            entity.Property(e => e.Controllerid)
                .HasMaxLength(50)
                .HasColumnName("controllerid");
            entity.Property(e => e.EngName).HasMaxLength(200);
            entity.Property(e => e.Icons)
                .HasMaxLength(100)
                .HasColumnName("icons");
            entity.Property(e => e.MenuId)
                .HasMaxLength(10)
                .HasColumnName("menuID");
            entity.Property(e => e.Menuseq).HasColumnName("menuseq");
            entity.Property(e => e.Menutype).HasColumnName("menutype");
            entity.Property(e => e.RootKey).HasColumnName("rootKey");
            entity.Property(e => e.ThaiName).HasMaxLength(200);
            entity.Property(e => e.Urlname)
                .HasMaxLength(200)
                .HasColumnName("urlname");
            entity.Property(e => e.Viewid)
                .HasMaxLength(50)
                .HasColumnName("viewid");
        });

        modelBuilder.Entity<Ctlmessage>(entity =>
        {
            entity.HasKey(e => e.MsgCode).HasName("PK_st_message");

            entity.ToTable("CTLmessage");

            entity.Property(e => e.MsgCode)
                .HasMaxLength(50)
                .HasColumnName("msg_code");
            entity.Property(e => e.DeviceType)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("device_type");
            entity.Property(e => e.MsgLangEn)
                .HasMaxLength(500)
                .HasColumnName("msg_lang_EN");
            entity.Property(e => e.MsgLangTh)
                .HasMaxLength(500)
                .HasColumnName("msg_lang_TH");
            entity.Property(e => e.MsgShotEn)
                .HasMaxLength(100)
                .HasColumnName("msg_shot_EN");
            entity.Property(e => e.MsgShotTh)
                .HasMaxLength(100)
                .HasColumnName("msg_shot_TH");
            entity.Property(e => e.MsgType)
                .HasMaxLength(20)
                .HasColumnName("msg_type");
        });

        modelBuilder.Entity<Ctlstatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CTLstatus");

            entity.Property(e => e.StatusCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.StatusId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.StatusName).HasMaxLength(100);
        });

        modelBuilder.Entity<Ctluserlogin>(entity =>
        {
            entity.HasKey(e => new { e.Dcid, e.Whid, e.UserKey, e.UserName });

            entity.ToTable("CTLuserlogin");

            entity.Property(e => e.Dcid)
                .HasMaxLength(10)
                .HasColumnName("dcid");
            entity.Property(e => e.Whid)
                .HasMaxLength(10)
                .HasColumnName("whid");
            entity.Property(e => e.UserKey)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("user_key");
            entity.Property(e => e.UserName)
                .HasMaxLength(15)
                .HasColumnName("user_name");
            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .HasColumnName("action");
            entity.Property(e => e.AssgNo)
                .HasMaxLength(15)
                .HasColumnName("assg_no");
            entity.Property(e => e.Changedtime)
                .HasColumnType("datetime")
                .HasColumnName("changedtime");
            entity.Property(e => e.Changeuser)
                .HasMaxLength(15)
                .HasColumnName("changeuser");
            entity.Property(e => e.ClassId)
                .HasMaxLength(100)
                .HasColumnName("class_id");
            entity.Property(e => e.Controller)
                .HasMaxLength(100)
                .HasColumnName("controller");
            entity.Property(e => e.Createdtime)
                .HasColumnType("datetime")
                .HasColumnName("createdtime");
            entity.Property(e => e.Createuser)
                .HasMaxLength(15)
                .HasColumnName("createuser");
            entity.Property(e => e.CurrPasswd)
                .HasMaxLength(15)
                .HasColumnName("curr_passwd");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .HasColumnName("first_name");
            entity.Property(e => e.LangId)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("lang_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .HasColumnName("last_name");
            entity.Property(e => e.LoginType)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("login_type");
            entity.Property(e => e.Logintimeout).HasColumnName("logintimeout");
            entity.Property(e => e.OldPasswd)
                .HasMaxLength(15)
                .HasColumnName("old_passwd");
            entity.Property(e => e.ResetPasswdCtl)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("reset_passwd_ctl");
            entity.Property(e => e.SessionType)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("session_type");
            entity.Property(e => e.WhName)
                .HasMaxLength(300)
                .HasColumnName("whName");
        });

        modelBuilder.Entity<SysDatalog>(entity =>
        {
            entity.HasKey(e => new { e.Whid, e.WlCode, e.ServerIp, e.LoginDtime, e.IpAddress }).HasName("PK_ctl_datalog");

            entity.ToTable("SysDatalog");

            entity.HasIndex(e => e.LogId, "IX_ctl_datalog").IsUnique();

            entity.Property(e => e.Whid)
                .HasMaxLength(10)
                .HasColumnName("whid");
            entity.Property(e => e.WlCode)
                .HasMaxLength(10)
                .HasColumnName("wlCode");
            entity.Property(e => e.ServerIp)
                .HasMaxLength(50)
                .HasColumnName("server_ip");
            entity.Property(e => e.LoginDtime)
                .HasColumnType("datetime")
                .HasColumnName("login_dtime");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(150)
                .HasColumnName("ip_address");
            entity.Property(e => e.BrowserType)
                .HasMaxLength(150)
                .HasColumnName("browser_type");
            entity.Property(e => e.HostName)
                .HasMaxLength(150)
                .HasColumnName("host_name");
            entity.Property(e => e.Identify)
                .HasMaxLength(150)
                .HasColumnName("identify");
            entity.Property(e => e.LogId)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("log_id");
            entity.Property(e => e.LogoutDtime)
                .HasColumnType("datetime")
                .HasColumnName("logout_dtime");
            entity.Property(e => e.MobileDevice).HasColumnName("mobile_device");
            entity.Property(e => e.SessionType)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("session_type");
            entity.Property(e => e.TranLog)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("tran_log");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(15)
                .HasColumnName("user_login");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
