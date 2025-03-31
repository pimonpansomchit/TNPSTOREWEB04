namespace TNPSTOREWEB.Model
{
    public class BarcodesInfo
    {
        public string BarcodeI { get; set; } = null!;

        public string BarcodeC { get; set; } = null!;

        public string BarcodeU { get; set; } = null!;

        public string ItemCode { get; set; } = null!;

        public int? ItemQty { get; set; }

        public string? UnitName { get; set; }

        public int? Unit { get; set; }

        public int? UnitI { get; set; }

        public int? UnitC { get; set; }

        public int? UnitU { get; set; }

    }
}
