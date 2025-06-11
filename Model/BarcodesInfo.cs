namespace TNPWMSWEB.Model
{
    public class BarcodesInfo
    {
        public string SkuCode { get; set; } = null!;
        public decimal SkuKey { get; set; }
        public decimal BarcodeKey { get; set; }
        public string Barcode { get; set; } = null!;
        public string SkuName { get; set; } = string.Empty;
        public string UnitName { get; set; } =string.Empty;
        public int Unit { get; set; }

        
    }
}
