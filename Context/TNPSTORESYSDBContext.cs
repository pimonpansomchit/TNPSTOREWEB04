using Microsoft.EntityFrameworkCore;
using TNPSTOREWEB.Models;

namespace TNPSTOREWEB.Context;

public partial class TNPSTORESYSDBContext : DbContext
{
    public string ConnectDb = Startup.databaseUrlst;
    public TNPSTORESYSDBContext()
    {
    }

    public TNPSTORESYSDBContext(DbContextOptions<TNPSTORESYSDBContext> options)
        : base(options)
    {
    }
    public virtual DbSet<StMessage> StMessages { get; set; }
    public virtual DbSet<StUserlogin> StUserlogins { get; set; }
    public virtual DbSet<StWlprice> StWlprices { get; set; }  
    public virtual DbSet<Ctldocrun> Ctldocruns { get; set; }
    public virtual DbSet<SysDatalog> SysDatalogs { get; set; }
    public virtual DbSet<Trnreplenish> Trnreplenishes { get; set; }
    public virtual DbSet<TmpreplenishD> TmpreplenishDs { get; set; }
    public virtual DbSet<TrnreplenishD> TrnreplenishDs { get; set; }
    public virtual DbSet<TrnoutofStockD> TrnoutofStockDs { get; set; }
    public virtual DbSet<TrnexpiredofGoodD> TrnexpiredofGoodDs { get; set; }
    public virtual DbSet<Mstlocation> Mstlocations { get; set; }
    public virtual DbSet<StClass> StClasses { get; set; }
    public virtual DbSet<StMenu> StMenus { get; set; }
    public virtual DbSet<Locprdd> Locprdds { get; set; }
    public virtual DbSet<Ctlconfig> Ctlconfigs { get; set; }
    public virtual DbSet<StLoctype> StLoctypes { get; set; }
    public virtual DbSet<StClassweb> StClasswebs { get; set; }
    public virtual DbSet<StMenuweb> StMenuwebs { get; set; }
    public virtual DbSet<StStatus> StStatuses { get; set; }
    public virtual DbSet<StClassinfo> StClassinfos { get; set; }

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
        modelBuilder.Entity<StClassinfo>(entity =>
        {
            entity.HasKey(e => e.ClassCode);

            entity.ToTable("st_classinfo");

            entity.Property(e => e.ClassCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.ClassName).HasMaxLength(50);
        });
        modelBuilder.Entity<StMessage>(entity =>
        {
            entity.HasKey(e => e.MsgCode);

            entity.ToTable("st_message");

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
        modelBuilder.Entity<StUserlogin>(entity =>
        {
            entity.HasKey(e => new { e.UserName, e.WlKey }).HasName("PK_ctl_userlogin");

            entity.ToTable("st_userlogin");

            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .HasColumnName("user_name");
            entity.Property(e => e.WlKey)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("wl_key");
            entity.Property(e => e.ArprbCode)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ARPRB_CODE");
            entity.Property(e => e.ChangeDtime)
                .HasColumnType("datetime")
                .HasColumnName("change_dtime");
            entity.Property(e => e.ChangeUser)
                .HasMaxLength(10)
                .HasColumnName("change_user");
            entity.Property(e => e.ClassId)
                .HasMaxLength(15)
                .HasColumnName("class_id");
            entity.Property(e => e.CreateDtime)
                .HasColumnType("datetime")
                .HasColumnName("create_dtime");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(10)
                .HasColumnName("create_user");
            entity.Property(e => e.CurrPasswd)
                .HasMaxLength(10)
                .HasColumnName("curr_passwd");
            entity.Property(e => e.DbName)
                .HasMaxLength(50)
                .HasColumnName("db_name");
            entity.Property(e => e.Firstname)
                .HasMaxLength(150)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(150)
                .HasColumnName("lastname");
            entity.Property(e => e.OldPasswd)
                .HasMaxLength(10)
                .HasColumnName("old_passwd");
            entity.Property(e => e.SessionType)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("session_type");
            entity.Property(e => e.TbNewlabel)
                .HasMaxLength(50)
                .HasColumnName("tb_newlabel");
            entity.Property(e => e.Whid)
                .HasMaxLength(50)
                .HasColumnName("whid");
            entity.Property(e => e.WlCode)
                .HasMaxLength(50)
                .HasColumnName("wl_code");
            entity.Property(e => e.WlName)
                .HasMaxLength(200)
                .HasColumnName("wl_name");
            entity.Property(e => e.Language)
               .HasMaxLength(1)
               .HasColumnName("language");
        });
        modelBuilder.Entity<StWlprice>(entity =>
        {
            entity.HasKey(e => new { e.WlCode, e.ArprbCode });

            entity.ToTable("st_wlprice");

            entity.Property(e => e.WlCode)
                .HasMaxLength(50)
                .HasColumnName("wl_code");
            entity.Property(e => e.ArprbCode)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ARPRB_CODE");
            entity.Property(e => e.DbNameString)
                .HasMaxLength(300)
                .HasColumnName("db_name_string");
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
        modelBuilder.Entity<StLoctype>(entity =>
        {
            entity.HasKey(e => new { e.LocCat, e.LocType });

            entity.ToTable("st_loctype");

            entity.Property(e => e.LocCat)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("locCat");
            entity.Property(e => e.LocType).HasColumnName("locType");
            entity.Property(e => e.LocTypeName)
                .HasMaxLength(250)
                .HasColumnName("locTypeName");
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
        modelBuilder.Entity<Trnreplenish>(entity =>
        {
            entity.HasKey(e => new { e.Wlid, e.RecNo, e.DocNo }).HasName("PK_trnReplenish");

            entity.ToTable("TRNreplenish");

            entity.Property(e => e.Wlid)
                .HasMaxLength(10)
                .HasColumnName("wlid");
            entity.Property(e => e.RecNo).HasMaxLength(25);
            entity.Property(e => e.DocNo).HasMaxLength(20);
            entity.Property(e => e.ChangeDtime).HasColumnType("datetime");
            entity.Property(e => e.ChangeUser).HasMaxLength(10);
            entity.Property(e => e.CreateDtime).HasColumnType("datetime");
            entity.Property(e => e.CreateUser).HasMaxLength(10);
            entity.Property(e => e.Logid).HasMaxLength(50);
            entity.Property(e => e.TotalItem).HasColumnType("decimal(25, 0)");
            entity.Property(e => e.TotalItemRep).HasColumnType("decimal(25, 0)");
            entity.Property(e => e.TranCode)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.TranStatus)
                .HasMaxLength(3)
                .IsFixedLength();
        });
        modelBuilder.Entity<TrnreplenishD>(entity =>
        {
            entity.HasKey(e => new { e.Wlid, e.RecNo, e.DocNo, e.RecdNo }).HasName("PK_trnReplenishD");

            entity.ToTable("TRNreplenishD");

            entity.Property(e => e.Wlid)
                .HasMaxLength(10)
                .HasColumnName("wlid");
            entity.Property(e => e.RecNo).HasMaxLength(25);
            entity.Property(e => e.DocNo).HasMaxLength(20);
            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.ChangeDtime).HasColumnType("datetime");
            entity.Property(e => e.ChangeUser).HasMaxLength(10);
            entity.Property(e => e.CreateDtime).HasColumnType("datetime");
            entity.Property(e => e.CreateUser).HasMaxLength(10);
            entity.Property(e => e.GroupId).HasMaxLength(10);
            entity.Property(e => e.ItemCode).HasMaxLength(100);
            entity.Property(e => e.ItemName).HasMaxLength(500);
            entity.Property(e => e.LocIdfm)
                .HasMaxLength(50)
                .HasColumnName("LocIDFm");
            entity.Property(e => e.LocIdto)
                .HasMaxLength(50)
                .HasColumnName("LocIDTo");
            entity.Property(e => e.TranStatus)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.UnitName).HasMaxLength(30);
            entity.Property(e => e.UnitType)
                .HasMaxLength(1)
                .IsFixedLength();
        });
        modelBuilder.Entity<TmpreplenishD>(entity =>
        {
            entity.HasKey(e => new { e.TranDate, e.Wlid, e.RecNo, e.RecdNo, e.Logid }).HasName("PK_TMPReplenishD");

            entity.ToTable("TMPreplenishD");

            entity.Property(e => e.Wlid)
                .HasMaxLength(10)
                .HasColumnName("wlid");
            entity.Property(e => e.RecNo).HasMaxLength(25);
            entity.Property(e => e.Logid).HasColumnType("decimal(25, 0)");
            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.CreateUser).HasMaxLength(10);
            entity.Property(e => e.ItemCode).HasMaxLength(100);
            entity.Property(e => e.ItemName).HasMaxLength(500);
            entity.Property(e => e.LocIdfm)
                .HasMaxLength(50)
                .HasColumnName("LocIDFm");
            entity.Property(e => e.LocIdto)
                .HasMaxLength(50)
                .HasColumnName("LocIDTo");
            entity.Property(e => e.TranStatus)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.UnitName).HasMaxLength(100);
            entity.Property(e => e.UnitType)
                .HasMaxLength(1)
                .IsFixedLength();
        });
        modelBuilder.Entity<StClass>(entity =>
        {
            entity.HasKey(e => new { e.Classid, e.Menukey, e.ParentKey });

            entity.ToTable("st_class");

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
        modelBuilder.Entity<StMenu>(entity =>
        {
            entity.HasKey(e => new { e.Menukey, e.ParentKey });

            entity.ToTable("st_menu");

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
        modelBuilder.Entity<Mstlocation>(entity =>
        {
            entity.HasKey(e => new { e.Wlid, e.Locid }).HasName("PK_MstlocationB");

            entity.ToTable("MSTlocation");

            entity.Property(e => e.Wlid)
                .HasMaxLength(10)
                .HasColumnName("wlid");
            entity.Property(e => e.Locid)
                .HasMaxLength(20)
                .HasColumnName("locid");
            entity.Property(e => e.Ailse)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ailse");
            entity.Property(e => e.ChangeDtime)
                .HasColumnType("datetime")
                .HasColumnName("change_dtime");
            entity.Property(e => e.ChangeUser)
                .HasMaxLength(20)
                .HasColumnName("change_user");
            entity.Property(e => e.CreateDtime)
                .HasColumnType("datetime")
                .HasColumnName("create_dtime");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(20)
                .HasColumnName("create_user");
            entity.Property(e => e.Handing)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("handing");
            entity.Property(e => e.Hi).HasColumnName("hi");
            entity.Property(e => e.Level)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("level");
            entity.Property(e => e.LocStatus).HasColumnName("loc_status");
            entity.Property(e => e.Loccat)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("loccat");
            entity.Property(e => e.Locseq).HasColumnName("locseq");
            entity.Property(e => e.Loctype).HasColumnName("loctype");
            entity.Property(e => e.Position)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("position");
            entity.Property(e => e.Rack)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("rack");
            entity.Property(e => e.Side)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("side");
            entity.Property(e => e.StackPos).HasColumnName("stack_pos");
            entity.Property(e => e.StackPosDep).HasColumnName("stack_pos_dep");
            entity.Property(e => e.StackPosHgt).HasColumnName("stack_pos_hgt");
            entity.Property(e => e.StackPosWid).HasColumnName("stack_pos_wid");
            entity.Property(e => e.Ti).HasColumnName("ti");
            entity.Property(e => e.Updatedocno)
                .HasMaxLength(20)
                .HasColumnName("updatedocno");
        });
        modelBuilder.Entity<Locprdd>(entity =>
        {
            entity.HasKey(e => new { e.Wlid, e.ProdId, e.Barcode, e.Unittype, e.LocType, e.BarcodeKey, e.Loccat }).HasName("PK_Locprdd");

            entity.ToTable("LOCprdd");

            entity.Property(e => e.Wlid)
                .HasMaxLength(10)
                .HasColumnName("wlid");
            entity.Property(e => e.ProdId)
                .HasMaxLength(20)
                .HasColumnName("prodID");
            entity.Property(e => e.Barcode)
                .HasMaxLength(20)
                .HasColumnName("barcode");
            entity.Property(e => e.Unittype)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("unittype");
            entity.Property(e => e.LocType).HasColumnName("locType");
            entity.Property(e => e.BarcodeKey)
                .HasColumnType("decimal(25, 0)")
                .HasColumnName("barcodeKey");
            entity.Property(e => e.Loccat)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("loccat");
            entity.Property(e => e.BohQty).HasColumnName("bohQty");
            entity.Property(e => e.Casecap).HasColumnName("casecap");
            entity.Property(e => e.ChangeDtime)
                .HasColumnType("datetime")
                .HasColumnName("change_dtime");
            entity.Property(e => e.ChangeUser)
                .HasMaxLength(10)
                .HasColumnName("change_user");
            entity.Property(e => e.CreateDtime)
                .HasColumnType("datetime")
                .HasColumnName("create_dtime");
            entity.Property(e => e.CreateUser)
                .HasMaxLength(10)
                .HasColumnName("create_user");
            entity.Property(e => e.GroupId)
                .HasMaxLength(10)
                .HasColumnName("groupId");
            entity.Property(e => e.LocCode)
                .HasMaxLength(20)
                .HasColumnName("locCode");
            entity.Property(e => e.LocId)
                .HasMaxLength(20)
                .HasColumnName("locID");
            entity.Property(e => e.LocTypeName)
                .HasMaxLength(100)
                .HasColumnName("locTypeName");
            entity.Property(e => e.Loclev)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("loclev");
            entity.Property(e => e.Locpos)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("locpos");
            entity.Property(e => e.Locrack)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("locrack");
            entity.Property(e => e.Locstatus).HasColumnName("locstatus");
            entity.Property(e => e.Loczone)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("loczone");
            entity.Property(e => e.Logid)
                .HasColumnType("decimal(25, 0)")
                .HasColumnName("logid");
            entity.Property(e => e.Minsel).HasColumnName("minsel");
            entity.Property(e => e.ProdKey)
                .HasColumnType("decimal(25, 0)")
                .HasColumnName("prodKey");
            entity.Property(e => e.ProdName)
                .HasMaxLength(500)
                .HasColumnName("prodName");
            entity.Property(e => e.Rplnamt).HasColumnName("rplnamt");
            entity.Property(e => e.StackDep).HasColumnName("stackDep");
            entity.Property(e => e.StackHgt).HasColumnName("stackHgt");
            entity.Property(e => e.StackPos).HasColumnName("stackPos");
            entity.Property(e => e.Unit).HasColumnName("unit");
            entity.Property(e => e.Unitname)
                .HasMaxLength(10)
                .HasColumnName("unitname");
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
            entity.Property(e => e.ServerIp)
                .HasMaxLength(50)
                .HasColumnName("ServerIP");
            entity.Property(e => e.StoreDb)
                .HasMaxLength(50)
                .HasColumnName("StoreDB");
            entity.Property(e => e.Whid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("whid");
        });
        modelBuilder.Entity<TrnoutofStockD>(entity =>
        {
            entity.HasKey(e => new { e.Wlid, e.RecNo, e.RecdNo, e.Logid, e.TranDate });

            entity.ToTable("TRNOutofStockD");

            entity.Property(e => e.Wlid)
                .HasMaxLength(10)
                .HasColumnName("wlid");
            entity.Property(e => e.RecNo).HasMaxLength(25);
            entity.Property(e => e.Logid).HasColumnType("decimal(25, 0)");
            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.CreateDtime).HasColumnType("datetime");
            entity.Property(e => e.CreateUser).HasMaxLength(10);
            entity.Property(e => e.GroupId).HasMaxLength(10);
            entity.Property(e => e.ItemCode).HasMaxLength(100);
            entity.Property(e => e.ItemName).HasMaxLength(500);
            entity.Property(e => e.LocIdfm)
                .HasMaxLength(50)
                .HasColumnName("LocIDFm");
            entity.Property(e => e.LocIdto)
                .HasMaxLength(50)
                .HasColumnName("LocIDTo");
            entity.Property(e => e.TranStatus)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.UnitName).HasMaxLength(100);
            entity.Property(e => e.UnitType)
                .HasMaxLength(1)
                .IsFixedLength();
        });
        modelBuilder.Entity<TrnexpiredofGoodD>(entity =>
        {
            entity.HasKey(e => new { e.Wlid, e.RecNo, e.RecdNo, e.Logid, e.TranDate });

            entity.ToTable("TRNExpiredofGoodD");

            entity.Property(e => e.Wlid)
                .HasMaxLength(10)
                .HasColumnName("wlid");
            entity.Property(e => e.RecNo).HasMaxLength(25);
            entity.Property(e => e.Logid).HasColumnType("decimal(25, 0)");
            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.CreateDtime).HasColumnType("datetime");
            entity.Property(e => e.CreateUser).HasMaxLength(10);
            entity.Property(e => e.GroupId).HasMaxLength(10);
            entity.Property(e => e.ItemCode).HasMaxLength(100);
            entity.Property(e => e.ItemName).HasMaxLength(500);
            entity.Property(e => e.LocIdfm)
                .HasMaxLength(50)
                .HasColumnName("LocIDFm");
            entity.Property(e => e.LocIdto)
                .HasMaxLength(50)
                .HasColumnName("LocIDTo");
            entity.Property(e => e.LotNo).HasMaxLength(10);
            entity.Property(e => e.TranStatus)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.UnitName).HasMaxLength(100);
            entity.Property(e => e.UnitType)
                .HasMaxLength(1)
                .IsFixedLength();
        });
        modelBuilder.Entity<StClassweb>(entity =>
        {
            entity.HasKey(e => new { e.Classid, e.Menukey, e.ParentKey });

            entity.ToTable("st_classweb");

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
        modelBuilder.Entity<StMenuweb>(entity =>
        {
            entity.HasKey(e => new { e.Menukey, e.ParentKey });

            entity.ToTable("st_menuweb");

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
        modelBuilder.Entity<StStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("st_status");

            entity.Property(e => e.StatusCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.StatusId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.StatusName).HasMaxLength(100);
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
