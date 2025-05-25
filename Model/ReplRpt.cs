using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
namespace TNPSTOREWEB.Model
{
    [XmlRoot("ReplRpt")]
    public class ReplRpt
    {
        [XmlElement("WlName")]
        public string? WlName { get; set; }
        [XmlElement("DocNo")]
        public string? DocNo { get; set; }
        [XmlElement("DocDate")]
        public string? DocDate { get; set; }
        [XmlElement("Barcode")]
        public string? barcode { get; set; }
        [XmlElement("ProductName")]
        public string? productName { get; set; }
        [XmlElement("GroupId")]
        public string? GroupId { get; set; }

        [XmlElement("UnitName")]
        public string? UnitName { get; set; }
        
        [XmlElement("LocIdfmName")]
        public string? LocIdfmName { get; set; }
        [XmlElement("LocIdtoName")]
        public string? LocIdtoName { get; set; }
        [XmlElement("ItemQty")]
        public int? ItemQty { get; set; }
        [XmlElement("ItemQtyRep")]
        public int? ItemQtyRep { get; set; }
        [XmlElement("TranStatusname")]
        public string? TranStatusname { get; set; }

        [XmlElement("CreateDtime")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string? CreateDtime { get; set; }
        [XmlElement("CreateUser")]
        public string? CreateUser { get; set; }
        [XmlElement("ChangeDtime")]

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string? ChangeDtime { get; set; }
        [XmlElement("ChangeUser")]
        public string? ChangeUser { get; set; }

        [XmlElement("Totalitem")]
        public int? Totalitem { get; set; }

        [XmlElement("WlCode")]
        public string? WlCode { get; set; }



    }
}
