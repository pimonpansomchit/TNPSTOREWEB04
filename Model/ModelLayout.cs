using TNPSTOREWEB.Models;


namespace TNPSTOREWEB.Model
{
    public class ModelLayout
    {

        public int? menukey { get; set; }
        public decimal? Id { get; set; }
        public int Saveflg { get; set; }
        public string? Message { get; set; }
        public string? wlcode { get; set; }
        public string? wlname { get; set; }
        public string? actions { get; set; }
        public int? display { get; set; }
        public string disable { get; set; }
        public string[]? Options { get; set; }
        public string? Clear { get; set; }
        public int selectLoctype { get; set; }
        public string? Export { get; set; }
        public string? groupId { get; set; }
        public string? LotNo { get; set; }
        public string? ExprieDate { get; set; }
        public int Qty { get; set; }
        public int? Qtyrpl { get; set; }
        public string? Loccat { get; set; }
        public string? Locidfm { get; set; }
        public string? Locidto { get; set; }
        public int Totalconfirm { get; set; }
        public int maxcount { get; set; }
        public List<DataStatus> Transtauts { get; set; }

        //Serach History

        public string? barcode { get; set; }
        public string? SelectdocNo { get; set; }
        public string? SelectGroupNo { get; set; }
        public string? SelectStatus { get; set; }
        public string? SelectedOption { get; set; }

        public string? Selectedadd { get; set; }
        public string? DateFm { get; set; }
        public string? DateTo { get; set; }

        public int loctype { get; set; }
        public string? loctypename { get; set; }
       

        //Serach History
        public ProdInfo? pi { get; set; }
        public RepdataList? rp { get; set; }
        public ClassMenuWeb? ModelClass { get; set; }
        public RepdataHistory? ModelHis { get; set; }
        public StUserlogin addusr { get; set; }
        public List<StUserlogin>? usr { get; set; }
        public List<ExpRpt>? dataExpRpt { get; set; }
        public List<OutofSRpt>? dataOutRpt { get; set; }
        public List<ReplRpt>? dataRpt { get; set; }
        public List<StLoctype>? loc {get; set;}
        public List<TmpreplenishD> trp { get; set; }
        public List<MstWl> wls{ get; set; }
        public List<StClassinfo> cinfo { get; set; }
    }
}
