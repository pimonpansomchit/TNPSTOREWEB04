using MBSASSET.Model.Request;
using TNPWMSWEB.Models;


namespace TNPWMSWEB.Model
{
    public class ModelLayout
    {
        public decimal Id { get; set; } = 0;
        public string classid { get; set; } = string.Empty;
        public string barcode { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public int? menukey { get; set; }
        public int Saveflg { get; set; }
        public string? Message { get; set; }
        public string? wlcode { get; set; }
        public string? wlname { get; set; }
        public string? actions { get; set; }
        public int? display { get; set; }
        public string disable { get; set; } =string.Empty;
        public string[]? Options { get; set; }
        public string? Clear { get; set; }
        public string? Export { get; set; }
        public string? groupId { get; set; }
       
        public List<DataStatus> Transtauts { get; set; } = new();

        //Serach History

       
        public string? SelectdocNo { get; set; }
        public string? SelectGroupNo { get; set; }
        public string? SelectStatus { get; set; }
        public string? SelectedOption { get; set; }
        public string? Selectedadd { get; set; }
        public string? DateFm { get; set; }
        public string? DateTo { get; set; }

        public string urls { get; set; } = string.Empty;
        public string xmlname { get; set; } = string.Empty;


        public string msg { get; set; } = string.Empty;
        public string codeid { get; set; } = string.Empty;

        //Serach History
        public ProdInfo prdinfo { get; set; } = new();
        public ClassMenuWeb ModelClass { get; set; } = new();
        public Ctluserlogin addusr { get; set; } = new();
        public List<Mstgroup> prdgrp { get; set; } = new();
        public List<MstWl> wls { get; set; } = new();
        public List<Ctluserlogin> usr { get; set; } = new();
        public List<Ctlclassinfo> cinfo { get; set; } = new();
        public MessageModel msgpop { get; set; } = new();
    }
}
