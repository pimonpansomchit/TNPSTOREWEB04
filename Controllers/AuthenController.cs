#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TNPWMSWEB.Context;
using TNPWMSWEB.Model;
using TNPWMSWEB.Models;
using TNPWMSWEB.Models.Request;
using Wangkanai.Detection.Services;


namespace TNPWMSWEB.Controllers
{
    public class AuthenController : Controller
    {


        private readonly TNPWMSSYSDBContext _db;
        private readonly IHttpContextAccessor _accessor;
        private readonly IDetectionService _detection;
  

        public AuthenController(IHttpContextAccessor accessor, TNPWMSSYSDBContext db, IDetectionService detection)
        {
            _db = db;
            _accessor = accessor;
            _detection = detection;
        }

        public IActionResult Index()
        {
            return View();
        }
        //GET METHOD Origenal (for Display)
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel obj)
        {
            ModelLayout layout = new ModelLayout();
            Ctluserlogin _Userobj;
            _Userobj = ISFoundInUserLogin(obj);


            layout.ModelClass = new() { Users = new() };
            if (ModelState.IsValid) { 
            if (_Userobj.UserName != null)
            {
                try
                {
                        layout.Id = GetIpLogin(_Userobj, Get_accessor());
                        layout.wlcode = _Userobj.Whid;
                        layout.ModelClass.Users.logid= (decimal)layout.Id;
                        layout.ModelClass.Users.WLCode = _Userobj.Whid; //same wlcode
                        layout.ModelClass.Users.UserName = _Userobj.UserName;
                        layout.ModelClass.Users.ClassId = _Userobj.ClassId;
                        layout.ModelClass.Users.language = _Userobj.LangId;
                        return RedirectToAction("Indexweb", "Home", layout);
                }
                catch (Exception)
                {
                }

            }
            }
            return View();

        }

      
        private Ctluserlogin ISFoundInUserLogin(LoginModel user)
        {

            Ctluserlogin data = new Ctluserlogin();

                var sysUser = _db.Ctluserlogins.Where(t =>
                    t.UserName == user.USERNAME
                    && t.CurrPasswd == user.PASSWORD
                    ).FirstOrDefault();
                if (sysUser != null)
                {
                    data = sysUser;
                    return data;

                }
                else { return data; }
        }

        private IHttpContextAccessor Get_accessor()
        {
            return _accessor;
        }

        private decimal GetIpLogin(Ctluserlogin st, IHttpContextAccessor _accessor)
        {
              TNPWMSSYSDBContext _dbs = new TNPWMSSYSDBContext();
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                SysDatalog data = new();
                data.Whid = st.Whid;
                data.WlCode = st.Whid;
                data.ServerIp = _db.Ctlconfigs.Select(t => t.ServerIp).FirstOrDefault();
                //data.ServerIp = _accessor.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
                //data.IpAddress = _accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                data.IpAddress = "1.0.0.0";
                data.LoginDtime = DateTime.Now;
                data.UserLogin = st.UserName;
                data.MobileDevice = ((short)_detection.Device.Type);
                data.HostName = string.Empty;
                //data.Identify = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                data.BrowserType = _detection.Browser.Name.ToString();
                data.SessionType = st.SessionType;
                data.LogId = Convert.ToDecimal(DateTime.Now.ToString("yyMMddHHmmssfff")+"2" + st.Whid.Trim());
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (data != null)
                {
                    _dbs.SysDatalogs.Add(data);
                    _dbs.SaveChanges();
                    return data.LogId;

                }
                else { 
                    
                    return 0; }
            }
            catch (Exception) { return 0; }
            
        }

    }
}
