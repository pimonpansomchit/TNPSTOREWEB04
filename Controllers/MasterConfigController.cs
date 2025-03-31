#nullable disable
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Core;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;

namespace TNPSTOREWEB.Controllers
{
    public class MasterConfigController : Controller
    {
        private GetdataReport inforpt;
        public IActionResult TypeLocation(ClassModel model, decimal? Id, ModelLayout menu)
        {

            try
            {
                menu.ModelClass = new();
                if (model.logid != 0)
                {

                    menu.ModelClass.Users = GetDBConnect.GetClassModel(model.logid);
                    menu.Id=model.logid;
                }
                else
                {
                    if (Id == 0 || Id ==null)
                    {
                        return RedirectToAction("Login", "Authen");
                    }
                    else
                    {
                        menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                        menu.Id = Id;



                    }
                }

                menu = GetAdddata(menu, "T");
               

            }

            catch (Exception) { }
            
            return View(menu);

        }

        public IActionResult TypeLocationAdd(ModelLayout menu, decimal? Id)
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
                    if(Id == 0 || Id ==null)
                    {
                        return RedirectToAction("Login", "Authen");
                    }
                    else
                    {
                        menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                        
                        menu.Id = Id;
                    }
                    
               
                   
                }

            }

            catch (Exception) { }

            return View(menu);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TypeLocationAdd(ModelLayout menu,decimal? Id, string cat,string actions,string name, int type)
        {

            try
            {
                menu.ModelClass = new();
                if (Id != 0)
                {

                    menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                    ViewData["Logid"] = Id;

                }
                else
                {
                    
                        return RedirectToAction("Login", "Authen");
                   
                }

                if(actions=="A")
                {
                    using (TNPSTORESYSDBContext _dbs = new())
                    {
                        var data = _dbs.StLoctypes.Where(t => t.LocCat == cat.Trim()).OrderByDescending(t => t.LocType).FirstOrDefault();
                        menu.loctype = data.LocType+1;
                    }
                }
                else
                {
                    menu.loctype = type;
                }
            }

            catch (Exception) { }
            menu.Id = Id;
            menu.Clear = "N";
            menu.Loccat = cat;
            menu.loctypename = name;
            menu.actions = actions;
            return RedirectToAction("TypeLocationAdd", "MasterConfig",menu);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TypeLocation(ModelLayout rdata, decimal? Id, string clear, int type,string cat,string name,string actions)
        {
            try
            {

                if (Id != 0)
                {
                    
                    if (cat != null)
                    {
                        rdata.Loccat = cat.Trim();
                    }
                    if (name != null) { rdata.loctypename = name.Trim(); }
                    if (type != 0)
                    {
                        rdata.loctype = type;
                    }
                    if (rdata.Options != null)
                    {
                        rdata.SelectedOption = rdata.Options[0];
                        rdata.Loccat=rdata.Options[0];


                    }
                    else
                    {
                        rdata.SelectedOption = "A";
                    }
                    if (clear == null)
                    {
                        rdata.Clear = "N";
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
            rdata.Saveflg = 0;
            rdata.Message = "";
            rdata.actions = actions;
            return RedirectToAction("TypeLocation", "MasterConfig", rdata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ModelLayout rdata,string actions, int type, string cat ,decimal Id)
        {

            if (rdata.actions == "S")
            {
                rdata.Loccat = cat;
                rdata.loctype = type;
                StLoctype st = new()
                { LocCat = rdata.Loccat, LocType = rdata.loctype, LocTypeName = rdata.loctypename.Trim() };
                using (TNPSTORESYSDBContext _dbs = new())
                {
                    var data = _dbs.StLoctypes.Where(t => t.LocCat == rdata.Loccat.Trim() && t.LocType == rdata.loctype).FirstOrDefault();
                    if (data == null)
                    {
                        _dbs.StLoctypes.Add(st);
                        _dbs.SaveChanges();
                        var dataid = _dbs.StLoctypes.Where(t => t.LocCat == rdata.Loccat.Trim()).OrderByDescending(t => t.LocType).FirstOrDefault();
                        rdata.loctype = dataid.LocType + 1;
                        rdata.actions = "A";
                        rdata.loctypename = "";
                    }
                    else
                    {
                        data.LocTypeName = rdata.loctypename;
                        _dbs.SaveChanges();
                        rdata.actions = "M";
                       
                    }
                    
                    rdata.Saveflg = 0;
                    rdata.Message = "บันทึกข้อมูลสำเร็จ";
                    rdata.Id = Id;
                }

                //Add
            }
            
            return RedirectToAction("TypeLocationAdd", "MasterConfig", rdata);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ModelLayout rdata, string actions, int type, string cat, decimal Id)
        {

            if (rdata.actions == "D")
            {
                rdata.Loccat = cat;
                rdata.loctype = type;
              
                using (TNPSTORESYSDBContext _dbs = new())
                {
                    var data = _dbs.StLoctypes.Where(t => t.LocCat == rdata.Loccat.Trim() 
                    && t.LocType == rdata.loctype).FirstOrDefault();
                    
                    var tdata = _dbs.Locprdds.Where(t => t.Loccat == rdata.Loccat.Trim()
                    && t.LocType == rdata.loctype).FirstOrDefault();

                    if (data != null && tdata == null)
                    {
                       
                        _dbs.Remove(data);
                        _dbs.SaveChanges();
                        rdata.Saveflg = 0;
                        rdata.Message = "ลบข้อมูลสำเร็จ";


                    }
                    else
                    {
                        rdata.Saveflg = 1;
                        rdata.Message = "ไม่สามารถลบข้อมูลได้เนื่องจาก มีการใช้ข้อมูลนี้อยู่ !!";
                    }


                        rdata.Id = Id;
                }

                //Add
            }

            return RedirectToAction("TypeLocation", "MasterConfig", rdata);

        }
        private ModelLayout GetAdddata(ModelLayout data, string typereport)
        {
            GetClassMenu getClassMenu = new();

            data.loc = new();
            inforpt = new();
            
            //Getdate Query
            try
            {


                data.Options = new[] { "S", "R","A" };
             
                if (data.Clear == null)
                {
                    data.Clear = "N";
                }
                if (data.SelectedOption == null)
                {
                    data.SelectedOption = "A";

                }
                if (data.Clear == "Y")
                {
                    data.SelectedOption = "A";
                   
                }


                data.wlcode = data.ModelClass.Users.WLCode.Trim();
                data.ModelClass.stClasses = getClassMenu.GetStClassesweb(data.ModelClass.Users.ClassId, 0, 0, 0, 0);
                switch (typereport)
                {
                    case "T": //Loctype
                        data = inforpt.ConfigTypeLoc(data);
                        break;
                    
                }


            }
            catch (Exception) { }
            return data;
        }
    }
}
