
#nullable disable
using System.ComponentModel.DataAnnotations;
namespace TNPSTOREWEB.Models.Request
{
    public class ClassModel
    {
        [Key]
        public string DCcode { get; set; }
        public string WHcode { get; set; }
        public string WLCode { get; set; }
        public string WLName{ get; set; }
        public string UserName { get; set; } = null!;
        public string ClassId { get; set; }
        public string DBKey { get; set; }

        public string MainDB { get; set; }
        public decimal logid { get; set; }
        public string language { get; set; }
        public string ConnectString { get; set; }
        public int menukey { get; set; }
        public int submenukey { get; set; }

    }
}
