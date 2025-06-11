#nullable disable
using TNPWMSWEB.Models;
using TNPWMSWEB.Model.Request;

namespace TNPWMSWEB.Model
{
    public class ClassMenuWeb
    {
        public ClassUserLogin Users {  get; set; }
        public  List<Ctlclassweb> wmsClasses { get; set; }
      
    }
}
