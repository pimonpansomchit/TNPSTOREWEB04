#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;
using Wangkanai.Detection.Services;


namespace TNPSTOREWEB.Controllers
{
    public class AuthenController : Controller
    {


        private readonly TNPSTORESYSDBContext _db;
        private readonly IHttpContextAccessor _accessor;
        private readonly IDetectionService _detection;
  

        public AuthenController(IHttpContextAccessor accessor, TNPSTORESYSDBContext db, IDetectionService detection)
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
            StUserlogin _Userobj;
            _Userobj = ISFoundInUserLogin(obj);


            layout.ModelClass = new() { Users = new() };
            if (ModelState.IsValid) { 
            if (_Userobj.UserName != null)
            {
                try
                {
                        layout.Id = GetIpLogin(_Userobj, Get_accessor());
                        layout.wlcode = _Userobj.WlCode;
                        layout.ModelClass.Users.logid= (decimal)layout.Id;
                        layout.ModelClass.Users.WLCode = _Userobj.WlCode;
                        layout.ModelClass.Users.UserName = _Userobj.UserName;
                        layout.ModelClass.Users.ClassId = _Userobj.ClassId;
                        layout.ModelClass.Users.language = _Userobj.Language;
                        return RedirectToAction("Indexweb", "Home", layout);
                }
                catch (Exception)
                {
                }

            }
            }
            return View();

        }

      
        private StUserlogin ISFoundInUserLogin(LoginModel user)
        {

            StUserlogin data = new StUserlogin();

                var sysUser = _db.StUserlogins.Where(t =>
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

        private decimal GetIpLogin(StUserlogin st, IHttpContextAccessor _accessor)
        {
              TNPSTORESYSDBContext _dbs = new TNPSTORESYSDBContext();
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                SysDatalog data = new();
                data.Whid = st.Whid;
                data.WlCode = st.WlCode;
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
                data.LogId = Convert.ToDecimal(DateTime.Now.ToString("yyMMddHHmmssfff")+"2" + st.WlCode.Trim());
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
