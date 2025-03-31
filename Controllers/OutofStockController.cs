
#nullable disable
using Microsoft.AspNetCore.Mvc;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Core;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;
using Wangkanai.Extensions;
namespace TNPSTOREWEB.Controllers
{
    public class OutofStockController : Controller
    {
        private TNPSTORESYSDBContext _dbs;
        private GetProdInfo info;
        public ActionResult Index(ClassModel model, ModelLayout rdata)
        {
            info = new();
            rdata.pi = new();
            Messageinfo msginfo = new();
            if (model.logid != 0 && rdata.Id == null)
            {
                rdata.Id = model.logid;
                rdata.menukey = model.menukey;
                rdata.Message = "";
                rdata.Saveflg = 0;
            }
            rdata.maxcount = info.MaxTrackcount("O")+1;
            if (rdata.barcode != null) //after input barcode and display
            {
                
                rdata = info.SearchProduct(rdata);
                msginfo = info.TrackDuplication("O", rdata.barcode, rdata.maxcount);
                rdata.Message = msginfo.messsage;
                rdata.Saveflg = msginfo.code;
            }
            
            return View(rdata);

        }

        // POST: ReplenishController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ModelLayout rdata, decimal Id, int datakey,string save)
        {
            try
            {
                //assgin tmp
               
                if (Id != 0)
                {
                    if (rdata.barcode != null)
                    {
                        info = new();
                        rdata.pi = new();
                        rdata = info.SearchProduct(rdata);
                        ClassModel model = new();
                        _dbs = new();
                        model = GetDBConnect.GetClassModel(Id);
                        DateOnly datenow = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day); //ติด Dateonly
                        if (save != null)
                        {
                            TrnoutofStockD idata = new()
                            {
                                Wlid = model.WLCode,
                                RecNo = Id.ToString(),
                                RecdNo = SerachMaxitemNo(Id.ToString(), model.DBKey, model.ConnectString, model.WLCode) + 1,
                                GroupId = rdata.groupId,
                                TranDate = datenow,
                                Barcode = rdata.barcode,
                                ItemCode = rdata.pi.MstProd.ProdId,
                                ItemName = rdata.pi.MstProd.LongDescription,
                                ItemQty = 0,
                                UnitName = rdata.pi.MstBarcode.UnitName,
                                Unit = (int)rdata.pi.MstBarcode.ProdBarUnit,
                                UnitType = rdata.pi.MstBarcode.ProdBarType,
                                LocIdfm = rdata.Locidfm, //sheft
                                LocIdto = rdata.Locidto, //reserve
                                TranType = 0, //need replenish
                                TranFlag = 1,
                                TranStatus = "CRE",
                                Logid = Id,
                                CreateUser = model.UserName,
                                CreateDtime=DateTime.Now,

                            };
                            if (idata != null)
                            {
                                _dbs.TrnoutofStockDs.Add(idata);
                                _dbs.SaveChanges();
                                rdata = new();

                                rdata.Saveflg = 0;
                                rdata.Message = "บันทึกข้อมูลเรียบร้อย";
                            }
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
            rdata.menukey = datakey;
            return RedirectToAction("Index", "OutofStock", rdata);
        }

        public int SerachMaxitemNo(string Id, string DBname, string DbConnext, string wlcode)
        {
            int itemno = 0;
            GetDBConnect dB = new();
            string SQL = $"use {DBname}" +
                         $" select isnull(max(RecdNo),0) " +
                         $" from TRNOutofStockD" +
                         $" where wlid={wlcode}" +
                         $" and TranDate =convert(date,Getdate()) ";
            if (dB.ExecuteReadData(SQL, DbConnext))
            {
                if (dB.myReader.HasRows)
                {
                    while (dB.myReader.Read())
                    {
                        itemno = (int)dB.myReader[0];
                    }
                }

            }

            return itemno;
        }


    }
}
