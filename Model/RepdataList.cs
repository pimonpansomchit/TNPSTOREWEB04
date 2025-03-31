using TNPSTOREWEB.Models;

namespace TNPSTOREWEB.Model
{
    public class RepdataList
    {
       
        public List<Mstgroup>? prodgroups { get; set; }
        public List<Trnreplenish>? replenish { get; set; }
        public List<TrnreplenishD>?  replenishD { get; set; }
        public List<AssignInfo>? assignInfos { get; set; }
        public List<RelLocList>? locprdds { get; set; }
        public CheckQty? checkQty { get; set; }  

      

    }
}
