using TNPSTOREWEB.Models;

namespace TNPSTOREWEB.Model
{
    public class ItemUnit
    {
        public int QtyC { get; set; }
        public int QtyI { get; set; }
        public int QtyU { get; set; }
        public TrnreplenishD? itemrelD { get; set; }
        
    }
}
