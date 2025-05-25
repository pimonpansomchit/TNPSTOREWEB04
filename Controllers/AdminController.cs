#nullable disable
using Microsoft.AspNetCore.Mvc;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Core;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models.Request;

namespace TNPSTOREWEB.Controllers
{
    public class AdminController : Controller
    {
        private GetClassMenu infousr =new();
        public IActionResult Userlogin(ClassModel model, decimal? Id, ModelLayout menu)
        {
            try
            {
                menu.ModelClass = new();
                infousr = new();

                if (model.logid != 0)
                {

                    menu.ModelClass.Users = GetDBConnect.GetClassModel(model.logid);
                    menu.Id = model.logid;
                }
                else
                {
                    if (Id == 0 || Id == null)
                    {
                        return RedirectToAction("Login", "Authen");
                    }
                    else
                    {
                        menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                        menu.Id = Id;

                    }
                }
                if (menu.SelectedOption == null)
                {
                    menu.SelectedOption = "0";
                }
                if (menu.SelectGroupNo == null)
                {
                    menu.SelectGroupNo = "0";
                }

                menu = infousr.Getuserlogin(menu);


            }

            catch (Exception) { 
                
            }

            return View(menu);
        }

        public IActionResult UserloginAdd(ModelLayout menu, decimal? Id)
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
                    if (Id == 0 || Id == null)
                    {
                        return RedirectToAction("Login", "Authen");
                    }
                    else
                    {
                        menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                        menu.Id = Id;
                    }


                    
                }
                menu = infousr.Getuserlogin(menu);
                if(menu.actions=="M")
                {
                    if (menu.addusr == null)
                    {
                        menu.Saveflg = 1;
                        menu.Message = "ไม่พบข้อมูลกรุณาตรวจสอบ";
                    }
                }

               
            }

            catch (Exception) { }

            return View(menu);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Userlogin(ModelLayout rdata, decimal? Id, string clear)
        {
            try
            {

                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";

                    if (rdata.SelectedOption == null)
                    {
                        rdata.SelectedOption = "0";
                    }
                    if (rdata.SelectGroupNo == null)
                    {
                        rdata.SelectGroupNo = "0";
                    }
                    if (clear == null)
                    {
                        rdata.Clear = "N";
                    }
                    else
                    {
                       
                        rdata.Clear = clear;
                        if (clear == "Y")
                        {
                            rdata.SelectedOption = "0";
                            rdata.SelectGroupNo = "0";

                        }

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
            return RedirectToAction("Userlogin", "Admin", rdata);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserloginAdd(ModelLayout menu, decimal? Id, string actionx,string classid , string usernames , string wl,string passwd)
        {

            menu.ModelClass = new();
            menu.actions = actionx;
            menu.Clear = "N";
            menu.Id = Id;
            TNPSTORESYSDBContext db = new();
            try
            {
                if (menu.actions == "M")
                {
                    menu.addusr = new();
                    menu.SelectGroupNo = db.StClassinfos.Where(t => t.ClassName == classid.Trim()).Select(t=>t.ClassCode).First();
                    menu.Selectedadd = usernames.Trim();
                    menu.SelectedOption = wl.ToString();
                   
                }
                if (Id != 0)
                {
                    menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                }
                else
                {

                    return RedirectToAction("Login", "Authen");

                }
            }

            catch (Exception) {
                return RedirectToAction("Userlogin", "Admin", menu);
            }
           
           
            return RedirectToAction("UserloginAdd", "Admin", menu);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ModelLayout rdata, string actionx, decimal Id,string actions)
        {
            rdata.ModelClass = new();
            rdata.actions = actions;
            if (actionx == "S")
            {
                rdata.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                TNPSYSCTLDBContext _db = new();

                using (TNPSTORESYSDBContext _dbs = new())
                {
                    var dataname = _dbs.StUserlogins.Where(t => t.UserName == rdata.addusr.UserName).FirstOrDefault();
                    var data = _dbs.StUserlogins.Where(t => t.WlCode == (rdata.SelectedOption.Trim()) && t.UserName == rdata.addusr.UserName).FirstOrDefault();
                   
                    if(dataname==null)
                    {
                        if (data == null)
                        {
                            rdata.addusr.WlKey = _db.MstWls.Where(t => t.WlId == (rdata.SelectedOption.Trim())).Select(t => t.WlKey).First();
                            rdata.addusr.WlCode = (rdata.SelectedOption.Trim());
                            rdata.addusr.WlName = _db.MstWls.Where(t => t.WlId == (rdata.SelectedOption.Trim())).Select(t => t.WlName).First();
                            rdata.addusr.ClassId = _dbs.StClassinfos.Where(t => t.ClassCode == rdata.SelectGroupNo.Trim()).Select(t => t.ClassName).First();
                            rdata.addusr.CreateDtime = DateTime.Now;
                            rdata.addusr.CreateUser = rdata.ModelClass.Users.UserName;
                            rdata.addusr.ChangeDtime = DateTime.Now;
                            rdata.addusr.ChangeUser = rdata.ModelClass.Users.UserName;
                            rdata.addusr.SessionType = "M";
                            rdata.addusr.Whid = rdata.ModelClass.Users.WHcode.Trim();
                            rdata.addusr.TbNewlabel = "tb_new" + rdata.addusr.WlCode.Trim();
                            rdata.addusr.DbName = rdata.ModelClass.Users.DBKey;
                            rdata.addusr.Language = "T";
                            rdata.addusr.ArprbCode = _dbs.StWlprices.Where(t => t.WlCode == rdata.addusr.WlCode).Select(t => t.ArprbCode).First();
                            _dbs.Add(rdata.addusr);
                            _dbs.SaveChanges();
                            rdata.actions = "A";
                            rdata.addusr = new();
                            rdata.SelectGroupNo = "0";
                            rdata.SelectedOption = "0";
                            rdata.Saveflg = 0;
                            rdata.Message = "บันทึกข้อมูลสำเร็จ";
                        }
                        else
                        {
                            if (rdata.actions == "M")
                            {
                                rdata.addusr.ChangeDtime = DateTime.Now;
                                rdata.addusr.ChangeUser = rdata.ModelClass.Users.UserName;
                                _dbs.SaveChanges();
                                rdata.actions = "A";
                                rdata.addusr = new();
                                rdata.Saveflg = 0;
                                rdata.Message = "บันทึกข้อมูลสำเร็จ";

                            }
                            else
                            {
                                rdata.Saveflg = 1;
                                rdata.Message = "ไม่สามารถ เพิ่มข้อมุลได้";
                            }
                        }
                    }
                    else
                    {
                        rdata.Saveflg = 1;
                        rdata.Message = "ไม่สามารถ เพิ่มข้อมุลได้ เนื่องจากมี username นี้ในระบบแล้ว";
                    }



                        rdata.Id = Id;
                }

                //Add
            }

            return RedirectToAction("UserloginAdd", "Admin", rdata);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ModelLayout rdata, string actionx, decimal Id,string wl,string usernames)
        {

            if (actionx == "D")
            {
                

                using (TNPSTORESYSDBContext _dbs = new())
                {
                    var data = _dbs.StUserlogins.Where(t => t.UserName == usernames.Trim() && t.WlCode==wl.Trim()).FirstOrDefault();

                    var tdata = _dbs.SysDatalogs.Where(t => t.UserLogin == usernames.Trim() && t.WlCode == wl.Trim()).FirstOrDefault();

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


                    
                }

                //Add
            }
            rdata.Id = Id;
            return RedirectToAction("Userlogin", "Admin", rdata);

        }

       
        
    }
}
