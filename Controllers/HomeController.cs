#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TNPWMSWEB.Context;
using TNPWMSWEB.Core;
using TNPWMSWEB.Model;
using TNPWMSWEB.Model.Request;
using TNPWMSWEB.Models;
namespace TNPWMSWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly TNPWMSSYSDBContext _db;
        GetClassMenu getClassMenu = new();

        public HomeController(TNPWMSSYSDBContext db)
        {
            _db = db;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserInfo(decimal? Id)
        {
            if (Id == null)
            { return NotFound(); }

            using (var dts = new TNPWMSSYSDBContext())
            {
                var username = dts.SysDatalogs.Where(t =>
                t.LogId == Id).FirstOrDefault().UserLogin;

                var data = _db.Ctluserlogins.Where(t =>
                t.UserName == username).FirstOrDefault();
                return View(data);
            }
        }

        //index into web
        public IActionResult Indexweb(ModelLayout menu, decimal? Id)
        {

            GetClassMenu getClassMenu = new();


            if (menu != null)
            {

                menu.ModelClass = getClassMenu.GetStClassesweb(menu);

            }
            else
            {

                return RedirectToAction("Login", "Authen");

            }

            return View(menu);

        }

        public IActionResult LogOut(decimal? Id)
        {
            TNPWMSSYSDBContext _dbs = new TNPWMSSYSDBContext();

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
        public IActionResult Indexweb(decimal Id)
        {
            ModelLayout menu = new();
            GetClassMenu getClassMenu = new();
            TNPWMSSYSDBContext db = new();

            if (Id == 0)
            {
                return RedirectToAction("Login", "Authen");
            }
            else
            {

                var userinfo = (from t in db.SysDatalogs
                                join t2 in db.Ctluserlogins
                                on t.UserLogin equals t2.UserName
                                where t.LogId == Id
                                select t2).First();

                menu.classid = userinfo.ClassId;
                menu.username = userinfo.UserName;

                return RedirectToAction("Indexweb", "Home", menu);

            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
