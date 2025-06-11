using Microsoft.Extensions.Primitives;
using TNPWMSWEB.Models;

namespace TNPWMSWEB.Model
{
    public class ProdInfo
    {
        public string GropId { get; set; } = string.Empty;
        public MstProd MstProd { get; set; } = new();
        public MstBarcode MstBarcode { get; set; } = new();
        public List<BarcodesInfo> Listsku { get; set; } = new();
        
    }
}
