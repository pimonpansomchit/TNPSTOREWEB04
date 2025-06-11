#nullable disable
using MBSASSET.Core;
using Microsoft.AspNetCore.Mvc;
using TNPWMSWEB.Context;
using TNPWMSWEB.Core;
using TNPWMSWEB.Model;

namespace TNPWMSWEB.Controllers
{
    public class ProductController : Controller
    {
        private  TNPWMSSYSDBContext _dbs;
        private GetProdInfo info =new();
        Systemcontrol msginfo = new();
        GetClassMenu getClassMenu = new();
        private GetDataList dl = new();

        private readonly IWebHostEnvironment _env;
        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index(ModelLayout data)
        {
            if (data.Id != 0)
            {

                data.prdinfo = new();
                data = GetViewData(data);
                if (data.codeid != null && data.codeid != string.Empty)
                {
                    data.msgpop = msginfo.GetMessage(data.codeid, data.msg);
                }

            }
            else
            {
                return RedirectToAction("Login", "Authen");
            }
            data.ModelClass = getClassMenu.GetStClassesweb(data);
            return View(data);
        }


        private ModelLayout GetViewData(ModelLayout rdata)
        {
           

            if (rdata != null) 
                {
                    rdata.prdgrp = dl.GetGroupList();
                rdata.prdinfo = info.SearchProduct(rdata);
                }
            return rdata;
        }
    }
}
