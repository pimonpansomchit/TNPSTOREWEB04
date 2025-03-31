using TNPSTOREWEB.Models;

namespace TNPSTOREWEB.Model
{
    public class RepdataHistory
    {
   
        public List<Mstgroup>? prodgroups { get; set; }
        public List<Trnreplenish>? replenish { get; set; }
        public List<TrnreplenishD>?  replenishD { get; set; }
        public List<RelLocList>? locprdds { get; set; }

        public List<TrnoutofStockD>? outofstock { get; set; }
        public List<TrnexpiredofGoodD>? expriegood { get; set; }

    }
}
