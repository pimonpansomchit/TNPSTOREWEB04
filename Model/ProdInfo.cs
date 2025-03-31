using Microsoft.Extensions.Primitives;
using TNPSTOREWEB.Models;

namespace TNPSTOREWEB.Model
{
    public class ProdInfo
    {
        
        public MstProd? MstProd { get; set; }
        public MstBarcode? MstBarcode { get; set; }
        public List<StLoctype>? stLoctypes { get; set; }
        public Locprdd prodinv { get; set; }
        public Locprdd iprodinv { get; set; }
    }
}
