using TNPSTOREWEB.Models;

namespace TNPSTOREWEB.Model
{
    public class LocdataList
    {
        public decimal? Id { get; set; }
        public int? menukey { get; set; }
        public string? barcode { get; set; }
        public int? loctype { get; set; }
        public int? SaveFlg { get; set; }
        public string? Message { get; set; }
        public string? groupid { get; set; }
        public MstProd? MstProd { get; set; }
        public MstBarcode? MstBarcode { get; set; }
        public List<LocList>? locinfo { get; set; }
      
    }
}
