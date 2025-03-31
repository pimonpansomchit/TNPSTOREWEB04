#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Core;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;
namespace TNPSTOREWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly TNPSTORESYSDBContext _db;
        public HomeController(TNPSTORESYSDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(ClassModel model, decimal? Id)
        {
            ClassMenu menu = new();
            GetClassMenu getClassMenu = new();


          

            if (model.logid != 0) {

                menu.Users = GetDBConnect.GetClassModel(model.logid);
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
                    menu.Users = GetDBConnect.GetClassModel(Id);
                    ViewData["Logid"] = Id;

                }
            }

            menu.stClasses = getClassMenu.GetStClasses(menu.Users.ClassId, 0, 0, 0, 0);
            return View(menu);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserInfo(decimal? Id)
        {
            if (Id == null)
            { return NotFound(); }

            using (var dts = new TNPSTORESYSDBContext())
            {
                var username = dts.SysDatalogs.Where(t =>
                t.LogId == Id).FirstOrDefault().UserLogin;

                var data = _db.StUserlogins.Where(t =>
                t.UserName == username).FirstOrDefault();
                return View(data);
            }
        }

        //index into web
        public IActionResult Indexweb(ModelLayout menu, decimal? Id)
        {
           
            GetClassMenu getClassMenu = new();
           

            if (menu.ModelClass != null)
            {
               
                menu.ModelClass.Users = GetDBConnect.GetClassModel(menu.ModelClass.Users.logid);
                ViewData["Logid"] = menu.ModelClass.Users.logid;
                menu.Id = menu.ModelClass.Users.logid;
            }
            else
            {
                if (Id == 0)
                {
                    return RedirectToAction("Login", "Authen");
                }
                else
                {
                    menu.ModelClass = new();
                    menu.ModelClass.Users = GetDBConnect.GetClassModel(Id);
                    menu.Id = Id;
                    ViewData["Logid"] = Id;

                }
            }

            menu.ModelClass.stClasses = getClassMenu.GetStClassesweb(menu.ModelClass.Users.ClassId, 0, 0, 0, 0);
            return View(menu);

        }

        public IActionResult LogOut(decimal? Id)
        {
            TNPSTORESYSDBContext _dbs = new TNPSTORESYSDBContext();

            if (Id == 0)
            {
                return RedirectToAction("Login", "Authen");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        SysDatalog data = new();
                        var sysUserLog = _dbs.SysDatalogs.Where(t =>
                        t.LogId == Id
                        ).FirstOrDefault();

                        if (sysUserLog != null)
                        {
                            data = sysUserLog;
                            data.LogoutDtime = DateTime.Now;
                            _dbs.SysDatalogs.Update(data);
                            _dbs.SaveChanges();
                        }

                        return RedirectToAction("Login", "Authen");
                    }
                    catch (Exception) { }
                }
            }


            ViewData["Logid"] = Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Indexweb(decimal? Id)
        {
            ModelLayout menu = new();
            GetClassMenu getClassMenu = new();
            ClassMenuWeb model = new();

            model.Users = new();

                if (Id == 0 && Id ==null)
                {
                    return RedirectToAction("Login", "Authen");
                }
                else
                {
                     model.Users.logid = (decimal)Id;
                     model.Users= GetDBConnect.GetClassModel(Id);
                     model.stClasses = getClassMenu.GetStClassesweb(model.Users.ClassId, 0, 0, 0, 0);
                    menu.Id = Id;
                    menu.ModelClass = model;
                     
                     return View(menu);
                
                }
            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PageRequest(decimal Id,int datakey,string actkey ,string controlkey,int submenukey)
        {
            
            ClassModel model = new();
           

            if (Id == 0)
            {
                return RedirectToAction("Login", "Authen");
            }
            else
            {
                model = GetDBConnect.GetClassModel(Id);
                model.menukey = datakey;
                model.submenukey = submenukey;
                model.logid = Id;
                ViewData["Logid"] = Id;
                ViewData["Menukey"] = datakey;
                return RedirectToAction(actkey, controlkey, model);
                   
            }


            

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
