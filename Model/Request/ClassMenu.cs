#nullable disable
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;

namespace TNPSTOREWEB.Model
{
    public class ClassMenu
    {
        public ClassModel Users {  get; set; }
        public  List<StClass> stClasses { get; set; }
      
    }
}
