using Microsoft.EntityFrameworkCore;
using TNPSTOREWEB.Models;

namespace TNPSTOREWEB.Context;

public partial class TNPSYSCTLDBContext : DbContext
{
    public string ConnectDb = Startup.databaseUrl;
    public TNPSYSCTLDBContext()
    {
    }

    public TNPSYSCTLDBContext(DbContextOptions<TNPSYSCTLDBContext> options)
        : base(options)
    {
    }


    
    public virtual DbSet<MstWl> MstWls { get; set; }

    public virtual DbSet<MstProd> MstProds { get; set; }
    public virtual DbSet<MstProdgroup> MstProdgroups { get; set; }
    public virtual DbSet<MstBarcode> MstBarcodes { get; set; }
    public virtual DbSet<MstBranch> MstBranches { get; set; }
    public virtual DbSet<WhsInfo> WhsInfos { get; set; }
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

        
        modelBuilder.Entity<MstWl>(entity =>
        {
            entity.HasKey(e => new { e.Dcid, e.WlKey, e.WlId }).HasName("PK_mst_whloc");

            entity.ToTable("mst_wl");

            entity.Property(e => e.Dcid)
                .HasMaxLength(10)
                .HasColumnName("dcid");
            entity.Property(e => e.WlKey)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("wl_key");
            entity.Property(e => e.WlId)
                .HasMaxLength(15)
                .HasColumnName("wl_id");
            entity.Property(e => e.Changedtime)
                .HasColumnType("datetime")
                .HasColumnName("changedtime");
            entity.Property(e => e.Changeuser)
                .HasMaxLength(10)
                .HasColumnName("changeuser");
            entity.Property(e => e.ContEmail)
                .HasMaxLength(100)
                .HasColumnName("cont_email");
            entity.Property(e => e.ContName)
                .HasMaxLength(50)
                .HasColumnName("cont_name");
            entity.Property(e => e.ContTelno)
                .HasMaxLength(20)
                .HasColumnName("cont_telno");
            entity.Property(e => e.Createdtime)
                .HasColumnType("datetime")
                .HasColumnName("createdtime");
            entity.Property(e => e.Createuser)
                .HasMaxLength(10)
                .HasColumnName("createuser");
            entity.Property(e => e.CustKey)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("cust_key");
            entity.Property(e => e.EnableFlg)
                .HasMaxLength(1)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("enable_flg");
            entity.Property(e => e.Logid)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("logid");
            entity.Property(e => e.WlAddrL1)
                .HasMaxLength(60)
                .HasColumnName("wl_addr_L1");
            entity.Property(e => e.WlAddrL2)
                .HasMaxLength(60)
                .HasColumnName("wl_addr_L2");
            entity.Property(e => e.WlAddrL3)
                .HasMaxLength(60)
                .HasColumnName("wl_addr_L3");
            entity.Property(e => e.WlCounty)
                .HasMaxLength(50)
                .HasColumnName("wl_county");
            entity.Property(e => e.WlName)
                .HasMaxLength(100)
                .HasColumnName("wl_name");
            entity.Property(e => e.WlState)
                .HasMaxLength(50)
                .HasColumnName("wl_state");
            entity.Property(e => e.WlTelno)
                .HasMaxLength(20)
                .HasColumnName("wl_telno");
            entity.Property(e => e.WlTexId)
                .HasMaxLength(13)
                .HasColumnName("wl_TexID");
            entity.Property(e => e.WlType)
                .HasMaxLength(2)
                .HasColumnName("wl_type");
            entity.Property(e => e.WlVat).HasColumnName("wl_vat");
            entity.Property(e => e.WlZipCode)
                .HasMaxLength(20)
                .HasColumnName("wl_zip_code");
        });
        modelBuilder.Entity<MstProd>(entity =>
        {
            entity.HasKey(e => new { e.Dcid, e.Whid, e.ProdId, e.ProdKey });

            entity.ToTable("mst_prod");

            entity.Property(e => e.Dcid)
                .HasMaxLength(10)
                .HasColumnName("dcid");
            entity.Property(e => e.Whid)
                .HasMaxLength(10)
                .HasColumnName("whid");
            entity.Property(e => e.ProdId)
                .HasMaxLength(20)
                .HasColumnName("prod_id");
            entity.Property(e => e.ProdKey)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("prod_key");
            entity.Property(e => e.Changedtime)
                .HasColumnType("datetime")
                .HasColumnName("changedtime");
            entity.Property(e => e.Changeuser)
                .HasMaxLength(10)
                .HasColumnName("changeuser");
            entity.Property(e => e.CodeDateExp).HasColumnName("code_date_exp");
            entity.Property(e => e.CodeDateFlg)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("code_date_flg");
            entity.Property(e => e.CodeDateIncr).HasColumnName("code_date_incr");
            entity.Property(e => e.CodeDateMax).HasColumnName("code_date_max");
            entity.Property(e => e.Createdtime)
                .HasColumnType("datetime")
                .HasColumnName("createdtime");
            entity.Property(e => e.Createuser)
                .HasMaxLength(10)
                .HasColumnName("createuser");
            entity.Property(e => e.Logid)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("logid");
            entity.Property(e => e.LongDescription)
                .HasMaxLength(100)
                .HasColumnName("long_description");
            entity.Property(e => e.ProdStatus)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("prod_status");
            entity.Property(e => e.ProdVat).HasColumnName("prod_vat");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(25)
                .HasColumnName("short_description");
            entity.Property(e => e.StoreHi).HasColumnName("store_hi");
            entity.Property(e => e.StoreTi).HasColumnName("store_ti");
            entity.Property(e => e.VenKey)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("ven_key");
            entity.Property(e => e.VenProdId)
                .HasMaxLength(20)
                .HasColumnName("ven_prod_id");
        });
        modelBuilder.Entity<MstBarcode>(entity =>
        {
            entity.HasKey(e => new { e.Dcid, e.Whid, e.ProdKey, e.PrddId, e.BarcodeKey }).HasName("PK_mst_barcode0.2");

            entity.ToTable("mst_barcode");

            entity.Property(e => e.Dcid)
                .HasMaxLength(10)
                .HasColumnName("dcid");
            entity.Property(e => e.Whid)
                .HasMaxLength(10)
                .HasColumnName("whid");
            entity.Property(e => e.ProdKey)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("prod_key");
            entity.Property(e => e.PrddId).HasColumnName("prdd_id");
            entity.Property(e => e.BarcodeKey)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("barcode_key");
            entity.Property(e => e.BarcodeStatus)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("barcode_status");
            entity.Property(e => e.Changedtime)
                .HasColumnType("datetime")
                .HasColumnName("changedtime");
            entity.Property(e => e.Changeuser)
                .HasMaxLength(10)
                .HasColumnName("changeuser");
            entity.Property(e => e.Createdtime)
                .HasColumnType("datetime")
                .HasColumnName("createdtime");
            entity.Property(e => e.Createuser)
                .HasMaxLength(10)
                .HasColumnName("createuser");
            entity.Property(e => e.Logid)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("logid");
            entity.Property(e => e.PrddCube)
                .HasColumnType("decimal(9, 3)")
                .HasColumnName("prdd_cube");
            entity.Property(e => e.PrddHgt)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("prdd_hgt");
            entity.Property(e => e.PrddLen)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("prdd_len");
            entity.Property(e => e.PrddWgt)
                .HasColumnType("decimal(9, 3)")
                .HasColumnName("prdd_wgt");
            entity.Property(e => e.PrddWid)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("prdd_wid");
            entity.Property(e => e.ProdBarType)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("prod_bar_type");
            entity.Property(e => e.ProdBarUnit)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("prod_bar_unit");
            entity.Property(e => e.ProdBarcode)
                .HasMaxLength(50)
                .HasColumnName("prod_barcode");
            entity.Property(e => e.ProdId)
                .HasMaxLength(20)
                .HasColumnName("prod_id");
            entity.Property(e => e.UnitName)
                .HasMaxLength(20)
                .HasColumnName("unit_name");
        });
        modelBuilder.Entity<MstBranch>(entity =>
        {
            entity.HasKey(e => new { e.Dcid, e.BrKey, e.BrId, e.BrType });

            entity.ToTable("mst_branch");

            entity.Property(e => e.Dcid)
                .HasMaxLength(10)
                .HasColumnName("dcid");
            entity.Property(e => e.BrKey)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("br_key");
            entity.Property(e => e.BrId)
                .HasMaxLength(12)
                .HasColumnName("br_id");
            entity.Property(e => e.BrType)
                .HasMaxLength(2)
                .HasColumnName("br_type");
            entity.Property(e => e.BrAddrL1)
                .HasMaxLength(60)
                .HasColumnName("br_addr_L1");
            entity.Property(e => e.BrAddrL2)
                .HasMaxLength(60)
                .HasColumnName("br_addr_L2");
            entity.Property(e => e.BrAddrL3)
                .HasMaxLength(60)
                .HasColumnName("br_addr_L3");
            entity.Property(e => e.BrCounty)
                .HasMaxLength(50)
                .HasColumnName("br_county");
            entity.Property(e => e.BrName)
                .HasMaxLength(100)
                .HasColumnName("br_name");
            entity.Property(e => e.BrState)
                .HasMaxLength(50)
                .HasColumnName("br_state");
            entity.Property(e => e.BrTelno)
                .HasMaxLength(100)
                .HasColumnName("br_telno");
            entity.Property(e => e.BrTexId)
                .HasMaxLength(13)
                .HasColumnName("br_TexID");
            entity.Property(e => e.BrVat).HasColumnName("br_vat");
            entity.Property(e => e.BrZipCode)
                .HasMaxLength(20)
                .HasColumnName("br_zip_code");
            entity.Property(e => e.Changedtime)
                .HasColumnType("datetime")
                .HasColumnName("changedtime");
            entity.Property(e => e.Changeuser)
                .HasMaxLength(10)
                .HasColumnName("changeuser");
            entity.Property(e => e.ContEmail)
                .HasMaxLength(100)
                .HasColumnName("cont_email");
            entity.Property(e => e.ContName)
                .HasMaxLength(50)
                .HasColumnName("cont_name");
            entity.Property(e => e.ContTelno)
                .HasMaxLength(100)
                .HasColumnName("cont_telno");
            entity.Property(e => e.Createdtime)
                .HasColumnType("datetime")
                .HasColumnName("createdtime");
            entity.Property(e => e.Createuser)
                .HasMaxLength(10)
                .HasColumnName("createuser");
            entity.Property(e => e.EnableFlg)
                .HasMaxLength(1)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("enable_flg");
            entity.Property(e => e.Logid)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("logid");
        });
        modelBuilder.Entity<WhsInfo>(entity =>
        {
            entity.HasKey(e => new { e.Dcid, e.Whid });

            entity.ToTable("whs_info");

            entity.Property(e => e.Dcid)
                .HasMaxLength(10)
                .HasColumnName("dcid");
            entity.Property(e => e.Whid)
                .HasMaxLength(10)
                .HasColumnName("whid");
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
            entity.Property(e => e.WhsActive)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("whs_active");
            entity.Property(e => e.WhsName)
                .HasMaxLength(200)
                .HasColumnName("whs_name");
        });
        modelBuilder.Entity<MstProdgroup>(entity =>
        {
            entity.HasKey(e => new { e.ProdId, e.ProdKey, e.SubgroupKey, e.GroupKey }).HasName("PK_mst_prodgrop");

            entity.ToTable("mst_prodgroup");

            entity.Property(e => e.ProdId)
                .HasMaxLength(25)
                .HasColumnName("prod_id");
            entity.Property(e => e.ProdKey).HasColumnName("prod_key");
            entity.Property(e => e.SubgroupKey).HasColumnName("subgroup_key");
            entity.Property(e => e.GroupKey).HasColumnName("group_key");
            entity.Property(e => e.GroupId)
                .HasMaxLength(15)
                .HasColumnName("group_id");
            entity.Property(e => e.GroupName)
                .HasMaxLength(150)
                .HasColumnName("group_name");
            entity.Property(e => e.SubgroupId)
                .HasMaxLength(15)
                .HasColumnName("subgroup_id");
            entity.Property(e => e.SubgroupName)
                .HasMaxLength(150)
                .HasColumnName("subgroup_name");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
