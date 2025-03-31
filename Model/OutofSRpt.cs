using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
namespace TNPSTOREWEB.Model
{
    [XmlRoot("OutofSRpt")]
    public class OutofSRpt
    {
       
        [XmlElement("DocDate")]
        public string DocDate { get; set; }
        [XmlElement("Barcode")]
        public string? barcode { get; set; }
        [XmlElement("ProductName")]
        public string? productName { get; set; }
       
        [XmlElement("GroupId")]
        public string? GroupId { get; set; }
        [XmlElement("UnitName")]
        public string? UnitName { get; set; }

        [XmlElement("LocIdtoName")]
        public string? LocIdtoName { get; set; }
       
        [XmlElement("ItemQty")]
        public int? ItemQty { get; set; }
       
        [XmlElement("CreateDtime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDtime { get; set; }

        [XmlElement("CreateUser")]
        public string? CreateUser { get; set; }
        


    }
}
