#nullable disable
using ClosedXML.Excel;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Core;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models.Request;
namespace TNPSTOREWEB.Controllers
{
   
    public class RepReportController : Controller
    {

        private GetProdInfo info;
        private GetdataReport inforpt;
        private GenerateFile intofile;
        private readonly IWebHostEnvironment _env;
        
        public RepReportController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult RepHistory(ClassModel model, decimal? Id, ModelLayout menu)
        {

            try
            {
                menu.ModelClass = new();
                if (model.logid != 0)
                {

                    menu.ModelClass.Users = GetDBConnect.GetClassModel(model.logid);


                    ViewData["Logid"] = model.logid;
                }
                else
                {
                    if (Id == 0)
                    {
                        return RedirectToAction("Login", "Authen");
                    }
                    else
                    {
                        menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                        ViewData["Logid"] = Id;

                    }
                }

                menu = Getreptdata(menu,"R");


            }

            catch (Exception) { }

            return View(menu);

        }

        public IActionResult ExportExcel(ModelLayout menu)
        {

            try
            {
                menu.ModelClass = new();
                if (menu.Id != 0)
                {

                    menu.ModelClass.Users = GetDBConnect.GetClassModel(menu.Id);

                }
                else
                {

                    return RedirectToAction("Login", "Authen");

                }

                menu = Getreptdata(menu, "R");

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Report");
                    worksheet.Cell(1, 1).InsertTable(menu.dataRpt);
                    worksheet.Cell(1, 1).Value = "เลขที่เอกสาร";
                    worksheet.Cell(1, 2).Value = "วันที่เอกสาร";
                    worksheet.Cell(1, 3).Value = "รหัสสินค้า";
                    worksheet.Cell(1, 4).Value = "ชื่อ";
                    worksheet.Cell(1, 5).Value = "หมวด";
                    worksheet.Cell(1, 6).Value = "หน่วย";
                    worksheet.Cell(1, 7).Value = "ตำแหน่งจัด";
                    worksheet.Cell(1, 8).Value = "ตำแหน่งเติม";
                    worksheet.Cell(1, 9).Value = "จำนวนสั่งเติม";
                    worksheet.Cell(1, 10).Value = "จำนวนเติม";
                    worksheet.Cell(1, 11).Value = "สถานะ";
                    worksheet.Cell(1, 12).Value = "เวลาสร้าง";
                    worksheet.Cell(1, 13).Value = "ผู้สร้าง";
                    worksheet.Cell(1, 14).Value = "เวลาปรับปรุง";
                    worksheet.Cell(1, 15).Value = "ผู้แก้ไข";
                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        stream.Position = 0;
                        string filename = $"RplReport{DateTime.Now.ToString("yyyyMMdd") + "_" + menu.wlcode.Trim()}.xlsx";
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{filename.Trim()}");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("RepHistory", "RepReport", menu);

            }

        }

        public IActionResult PreviewPDF(decimal Id, string barcode, string datefm, string dateto, string groupid, string statusid)
        {

            ModelLayout rdata = new();
            try
            {
                TNPSYSCTLDBContext db = new();
                intofile = new();
                rdata.ModelClass = new();
                if (Id != 0)
                {
                    rdata.Id = Id;
                    rdata.barcode = barcode;
                    rdata.DateFm = datefm;
                    rdata.DateTo = dateto;
                    rdata.SelectGroupNo = groupid;
                    rdata.SelectStatus = statusid;
                    rdata.ModelClass.Users = GetDBConnect.GetClassModel(rdata.Id);

                }
                else
                {

                    return RedirectToAction("Login", "Authen");

                }



                rdata = Getreptdata(rdata,"R");
                
               
                //string filepath = "Reports/ReplRpt.xml";

                string filepath = Path.Combine(_env.ContentRootPath, "Reports", "ReplRpt.xml");
                intofile.SaveToXml(rdata.dataRpt, filepath.Trim());

               
                WebReport webReport = new WebReport();
                string reportpath = Path.Combine(_env.ContentRootPath, "Reports", "Relreport.frx");
                webReport.Report.Load(reportpath);
                webReport.Report.SetParameterValue("DateFm", datefm);
                webReport.Report.SetParameterValue("DateTo", dateto);
                webReport.Report.SetParameterValue("WLName", rdata.ModelClass.Users.WLName.Trim());
               

                webReport.Report.Prepare();

                using (MemoryStream ms = new MemoryStream())
                {
                    PDFSimpleExport pDFSimpleExport = new PDFSimpleExport();

                    webReport.Report.Export(pDFSimpleExport, ms);
                    ms.Position = 0;
                    return File(ms.ToArray(), "application/pdf");

                }
            }
            catch (Exception ex)
            {
                rdata.Message = rdata.Message+ex.Message;
                return RedirectToAction("RepHistory", "RepReport", rdata);
            }

        }



        public IActionResult OutstkHistory(ClassModel model, decimal? Id, ModelLayout menu)
        {

            try
            {
                menu.ModelClass = new();
                if (model.logid != 0)
                {

                    menu.ModelClass.Users = GetDBConnect.GetClassModel(model.logid);


                    ViewData["Logid"] = model.logid;
                }
                else
                {
                    if (Id == 0)
                    {
                        return RedirectToAction("Login", "Authen");
                    }
                    else
                    {
                        menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                        ViewData["Logid"] = Id;

                    }
                }

                menu = Getreptdata(menu,"O");


            }

            catch (Exception) { }

            return View(menu);

        }


        public IActionResult OutExportExcel(ModelLayout menu)
        {

            try
            {
                menu.ModelClass = new();
                if (menu.Id != 0)
                {

                    menu.ModelClass.Users = GetDBConnect.GetClassModel(menu.Id);

                }
                else
                {

                    return RedirectToAction("Login", "Authen");

                }

                menu = Getreptdata(menu,"O");

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Report");
                    worksheet.Cell(1, 1).InsertTable(menu.dataOutRpt);
                    worksheet.Cell(1, 1).Value = "วันที่เอกสาร";
                    worksheet.Cell(1, 2).Value = "รหัสสินค้า";
                    worksheet.Cell(1, 3).Value = "ชื่อ";
                    worksheet.Cell(1, 4).Value = "หมวด";
                    worksheet.Cell(1, 5).Value = "หน่วย";
                    worksheet.Cell(1, 6).Value = "ตำแหน่ง";
                    worksheet.Cell(1, 7).Value = "จำนวน";
                    worksheet.Cell(1, 8).Value = "เวลาสร้าง";
                    worksheet.Cell(1, 9).Value = "ผู้สร้าง";
                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        stream.Position = 0;
                        string filename = $"OutofStockReport{DateTime.Now.ToString("yyyyMMdd") + "_" + menu.wlcode.Trim()}.xlsx";
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{filename.Trim()}");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("OutofSHistory", "RepReport", menu);

            }

        }

        public IActionResult OutPreviewPDF(decimal Id, string barcode, string datefm, string dateto, string groupid, string statusid)
        {

            ModelLayout rdata = new();
            try
            {
                TNPSYSCTLDBContext db = new();
                intofile = new();
                rdata.ModelClass = new();
                if (Id != 0)
                {
                    rdata.Id = Id;
                    rdata.barcode = barcode;
                    rdata.DateFm = datefm;
                    rdata.DateTo = dateto;
                    rdata.SelectGroupNo = groupid;
                    rdata.SelectStatus = statusid;
                    rdata.ModelClass.Users = GetDBConnect.GetClassModel(rdata.Id);

                }
                else
                {

                    return RedirectToAction("Login", "Authen");

                }



                rdata = Getreptdata(rdata, "O");

               
                string filepath = Path.Combine(_env.ContentRootPath, "Reports", "OutRpt.xml");

                intofile.SaveToXml(rdata.dataOutRpt, filepath.Trim());

                WebReport webReport = new WebReport();
                string reportpath = Path.Combine(_env.ContentRootPath, "Reports", "Outreport.frx");
                webReport.Report.Load(reportpath);
                webReport.Report.SetParameterValue("DateFm", datefm);
                webReport.Report.SetParameterValue("DateTo", dateto);
                webReport.Report.SetParameterValue("WLName", rdata.ModelClass.Users.WLName.Trim());
                
                webReport.Report.Prepare();

                using (MemoryStream ms = new MemoryStream())
                {
                    PDFSimpleExport pDFSimpleExport = new PDFSimpleExport();

                    webReport.Report.Export(pDFSimpleExport, ms);
                    ms.Position = 0;
                    
                    return File(ms.ToArray(), "application/pdf");

                }
            }
            catch (Exception)
            {
                return RedirectToAction("RepHistory", "RepReport", rdata);
            }

        }


        public IActionResult ExprieHistory(ClassModel model, decimal? Id, ModelLayout menu)
        {

            try
            {
                menu.ModelClass = new();
                if (model.logid != 0)
                {

                    menu.ModelClass.Users = GetDBConnect.GetClassModel(model.logid);


                    ViewData["Logid"] = model.logid;
                }
                else
                {
                    if (Id == 0)
                    {
                        return RedirectToAction("Login", "Authen");
                    }
                    else
                    {
                        menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                        ViewData["Logid"] = Id;

                    }
                }

                menu = Getreptdata(menu, "E");


            }

            catch (Exception) { }

            return View(menu);

        }

        public IActionResult ExpPreviewPDF(decimal Id, string barcode, string datefm, string dateto, string groupid, string statusid)
        {

            ModelLayout rdata = new();
            try
            {
                TNPSYSCTLDBContext db = new();
                intofile = new();
                rdata.ModelClass = new();
                if (Id != 0)
                {
                    rdata.Id = Id;
                    rdata.barcode = barcode;
                    rdata.DateFm = datefm;
                    rdata.DateTo = dateto;
                    rdata.SelectGroupNo = groupid;
                    rdata.SelectStatus = statusid;
                    rdata.ModelClass.Users = GetDBConnect.GetClassModel(rdata.Id);

                }
                else
                {

                    return RedirectToAction("Login", "Authen");

                }



                rdata = Getreptdata(rdata, "E");
               
                string filepath = Path.Combine(_env.ContentRootPath, "Reports", "ExpRpt.xml");
                intofile.SaveToXml(rdata.dataExpRpt, filepath.Trim());

                WebReport webReport = new WebReport();
                string reportpath = Path.Combine(_env.ContentRootPath, "Reports", "Expreport.frx");
                webReport.Report.Load(reportpath);
                webReport.Report.SetParameterValue("DateFm", datefm);
                webReport.Report.SetParameterValue("DateTo", dateto);
                webReport.Report.SetParameterValue("WLName", rdata.ModelClass.Users.WLName.Trim());
               

                webReport.Report.Prepare();

                using (MemoryStream ms = new MemoryStream())
                {
                    PDFSimpleExport pDFSimpleExport = new PDFSimpleExport();

                    webReport.Report.Export(pDFSimpleExport, ms);
                    ms.Position = 0;
                    //string htmlContent =System.Text.Encoding.UTF8.GetString(ms.ToArray());
                    //return Content(htmlContent, "text/html");
                    return File(ms.ToArray(), "application/pdf");

                }
            }
            catch (Exception)
            {
                return RedirectToAction("ExprieHistory", "RepReport", rdata);
            }

        }

        public IActionResult ExpExportExcel(ModelLayout menu)
        {

            try
            {
                menu.ModelClass = new();
                if (menu.Id != 0)
                {

                    menu.ModelClass.Users = GetDBConnect.GetClassModel(menu.Id);

                }
                else
                {

                    return RedirectToAction("Login", "Authen");

                }

                menu = Getreptdata(menu, "E");

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Report");
                    worksheet.Cell(1, 1).InsertTable(menu.dataExpRpt);
                    worksheet.Cell(1, 1).Value = "วันที่เอกสาร";
                    worksheet.Cell(1, 2).Value = "รหัสสินค้า";
                    worksheet.Cell(1, 3).Value = "ชื่อ";
                    worksheet.Cell(1, 4).Value = "หมวด";
                    worksheet.Cell(1, 5).Value = "หน่วย";
                    worksheet.Cell(1, 6).Value = "ตำแหน่ง";
                    worksheet.Cell(1, 7).Value = "จำนวน";
                    worksheet.Cell(1, 8).Value = "วันที่หมดอายุ";
                    worksheet.Cell(1, 9).Value = "LotNo";
                    worksheet.Cell(1, 10).Value = "เวลาสร้าง";
                    worksheet.Cell(1, 11).Value = "ผู้สร้าง";
                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        stream.Position = 0;
                        string filename = $"ExprieReport{DateTime.Now.ToString("yyyyMMdd") + "_" + menu.wlcode.Trim()}.xlsx";
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{filename.Trim()}");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("ExprieHistory", "RepReport", menu);

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RepHistory(ModelLayout rdata,  decimal? Id,string clear,string export)
        {
            try
            {

                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    info = new();
                    if (rdata.Options != null)
                    {
                        rdata.SelectedOption = rdata.Options[0];
                    }
                    else
                    {
                        rdata.SelectedOption = "1";
                    }
                    if (clear == null)
                    {
                        rdata.Clear = "N";
                    }
                    if (export == null) {
                        export = "N";
                    }
                    else
                    {
                        rdata.Export = export;
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Authen");
                }
            }
            catch (Exception ex) {
                rdata.Saveflg = 1;
                rdata.Message = ex.Message;

            }
                rdata.Id = Id;
                return RedirectToAction("RepHistory", "RepReport",rdata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OutstkHistory(ModelLayout rdata, decimal? Id, string clear, string export)
        {
            try
            {

                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    info = new();
                    if (rdata.Options != null)
                    {
                        rdata.SelectedOption = rdata.Options[0];
                    }
                    else
                    {
                        rdata.SelectedOption = "1";
                    }
                    if (clear == null)
                    {
                        rdata.Clear = "N";
                    }
                    if (export == null)
                    {
                        export = "N";
                    }
                    else
                    {
                        rdata.Export = export;
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Authen");
                }
            }
            catch (Exception ex)
            {
                rdata.Saveflg = 1;
                rdata.Message = ex.Message;

            }
            rdata.Id = Id;
            return RedirectToAction("OutstkHistory", "RepReport", rdata);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExprieHistory(ModelLayout rdata, decimal? Id, string clear, string export)
        {
            try
            {

                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    info = new();
                    if (rdata.Options != null)
                    {
                        rdata.SelectedOption = rdata.Options[0];
                    }
                    else
                    {
                        rdata.SelectedOption = "1";
                    }
                    if (clear == null)
                    {
                        rdata.Clear = "N";
                    }
                    if (export == null)
                    {
                        export = "N";
                    }
                    else
                    {
                        rdata.Export = export;
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Authen");
                }
            }
            catch (Exception ex)
            {
                rdata.Saveflg = 1;
                rdata.Message = ex.Message;

            }
            rdata.Id = Id;
            return RedirectToAction("ExprieHistory", "RepReport", rdata);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExportFile(ModelLayout rdata, decimal? Id, string export)
        {
            try
            {

                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    info = new();
                    if (rdata.Options != null)
                    {
                        rdata.SelectedOption = rdata.Options[0];
                    }
                    else
                    {
                        rdata.SelectedOption = "1";
                    }
                   
                    if (export == null)
                    {
                        export = "N";
                    }
                    else
                    {
                        rdata.Export = export;
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Authen");
                }
            }
            catch (Exception ex)
            {
                rdata.Saveflg = 1;
                rdata.Message = ex.Message;

            }
            rdata.Id = Id;
           
            return RedirectToAction("ExportExcel", "RepReport", rdata);
            
           
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OutExportFile(ModelLayout rdata, decimal? Id, string export)
        {
            try
            {

                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    info = new();
                    if (rdata.Options != null)
                    {
                        rdata.SelectedOption = rdata.Options[0];
                    }
                    else
                    {
                        rdata.SelectedOption = "1";
                    }

                    if (export == null)
                    {
                        export = "N";
                    }
                    else
                    {
                        rdata.Export = export;
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Authen");
                }
            }
            catch (Exception ex)
            {
                rdata.Saveflg = 1;
                rdata.Message = ex.Message;

            }
            rdata.Id = Id;

            return RedirectToAction("OutExportExcel", "RepReport", rdata);



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExpExportFile(ModelLayout rdata, decimal? Id, string export)
        {
            try
            {

                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    info = new();
                    if (rdata.Options != null)
                    {
                        rdata.SelectedOption = rdata.Options[0];
                    }
                    else
                    {
                        rdata.SelectedOption = "1";
                    }

                    if (export == null)
                    {
                        export = "N";
                    }
                    else
                    {
                        rdata.Export = export;
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Authen");
                }
            }
            catch (Exception ex)
            {
                rdata.Saveflg = 1;
                rdata.Message = ex.Message;

            }
            rdata.Id = Id;

            return RedirectToAction("ExpExportExcel", "RepReport", rdata);



        }

        private ModelLayout Getreptdata(ModelLayout data,string typereport)
        {
            GetClassMenu getClassMenu = new();

            data.ModelHis = new();
            data.Transtauts = new();

            info = new();
            inforpt = new();
            //Getdate Query
            try
            {


                data.Options = new[] { "1", "2" };
                data.ModelHis.prodgroups = info.GetGroupList();
                data.Transtauts = info.GetrepStatus();
                if (data.Clear == null)
                {
                    data.Clear = "N";
                }


                if (data.SelectedOption == null)
                {
                    data.SelectedOption = "1";

                }
                if (data.SelectStatus == null)
                {
                    data.SelectStatus = "0";
                }
                if (data.SelectGroupNo == null)
                {
                    data.SelectGroupNo = "0";
                }
                if (data.DateFm == null && data.DateTo == null)
                {
                    data.DateFm = DateTime.Now.ToString("yyyy-MM-dd");
                    data.DateTo = DateTime.Now.ToString("yyyy-MM-dd");
                }

                if (data.Clear == "Y")
                {
                    data.barcode = null;
                    data.SelectedOption = "0";
                    data.SelectGroupNo = "0";
                    data.SelectStatus = "0";
                    data.DateFm = DateTime.Now.ToString("yyyy-MM-dd");
                    data.DateTo = DateTime.Now.ToString("yyyy-MM-dd");

                }


                data.wlcode = data.ModelClass.Users.WLCode.Trim();
                data.ModelClass.stClasses = getClassMenu.GetStClassesweb(data.ModelClass.Users.ClassId, 0, 0, 0, 0);
                switch(typereport)
                {
                    case "R":
                    data = inforpt.RelListHistory(data);
                       break;
                    case "O":
                        data = inforpt.OutListHistory(data);
                        break;
                    case "E":
                        data = inforpt.ExpListHistory(data);
                        break;
                }
               

            }
            catch (Exception) { }


            return data;
        }

    }
}
