#nullable disable
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;

namespace TNPSTOREWEB.Model
{
    public class ClassMenuWeb
    {
        public ClassModel Users {  get; set; }
        public  List<StClassweb> stClasses { get; set; }
      
    }
}
