#nullable disable
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Core;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;


namespace TNPSTOREWEB.Controllers
{

    public class ReplenishController : Controller
    {
        private TNPSTORESYSDBContext _dbs;
        private GetProdInfo info;

        // GET: ReplenishController
        public ActionResult Index(ClassModel model, ModelLayout rdata)
        {
            info = new();
            rdata.pi = new();
            Messageinfo msginfo = new();
            if (rdata.Id != 0) 
            {
                rdata.Id = rdata.Id;
                rdata.menukey = rdata.menukey;
                

                rdata.Id = rdata.Id;
                rdata.menukey = rdata.menukey;
            }
            
            rdata.pi.stLoctypes = info.GetLoctype("S");
            rdata.maxcount = info.MaxTrackcount("R")+1;
            if (rdata.barcode != null) //after input barcode and display
            {
                    rdata.barcode = rdata.barcode.ToString();
                    rdata.Loccat = "S";
                    rdata = info.SearchProduct(rdata);   
                    msginfo = info.TrackDuplication("R", rdata.barcode, rdata.maxcount);
                    rdata.Message=msginfo.messsage;
                    rdata.Saveflg = msginfo.code;

            }
            
            return View(rdata);

        }

        public ActionResult RepSummary(decimal? Id, int error, string msg, int datakey,string docno)
        {
            ViewData["Menukey"] = datakey;
            ViewData["Logid"] = Id;
            ViewData["ErrorID"] = error;
            ViewData["Message"] = msg;
            ModelLayout data = new();
            ClassModel model = new();
            _dbs = new();
            data.trp = new();
            try
            {
               
                model = GetDBConnect.GetClassModel(Id);
                DateOnly datenow = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) ; //ติด Dateonly
                var qdata = _dbs.TmpreplenishDs.Where(t => t.TranDate== datenow && t.CreateUser==model.UserName && t.Wlid==model.WLCode ).OrderBy(t => t.RecdNo).ToList();
                data.trp = qdata;

                if(docno!=null)
                {
                    data.Totalconfirm = _dbs.TrnreplenishDs.Where(t => t.DocNo == docno.Trim() && t.Wlid == model.WLCode).Count();
                    data.SelectdocNo = docno.Trim();
                }
                else
                {
                    data.Totalconfirm = 0;
                }


            }
            catch(Exception e) 
            {
                ViewData["ErrorID"] = 1;
                ViewData["Message"] =e.Message;
            }

            return View(data);
        }

        public ActionResult RepList(ClassModel model, decimal? Id, int datakey, int type)
        {
            ViewData["ErrorID"] = 0;
            ViewData["Message"] = string.Empty;
           
            if (type == 0)
            { type = 1; }

            ModelLayout data = new()
            {
                Options = new[] { "1", "2", "3" },
                SelectedOption = type.ToString(),
                rp = new()
            };
            data.rp.replenish = new();


            if (model.logid != 0)
            {

                ViewData["Menukey"] = model.menukey;
                ViewData["Logid"] = model.logid;
            }
            else
            {
                model = GetDBConnect.GetClassModel(Id);
                ViewData["Menukey"] = datakey;
                ViewData["Logid"] = Id;
            }
            try
            {
                if (Id != 0)
                {

                    data.display = type;
                    if (type == 3)
                    {
                        data.rp.replenish = new();
                        using (_dbs = new TNPSTORESYSDBContext())
                        {
                            data.rp.replenish = _dbs.Trnreplenishes.Where(t => t.TranStatus == "OPN" && t.Wlid == model.WLCode).OrderBy(t => t.DocNo).ToList();

                        }
                    }
                    else
                    {
                        if (type == 0)
                        {
                            data.display = 1;

                        }
                        else
                        {
                            data.display = type;
                        }
                        data.rp.replenishD = new();
                        using (_dbs = new TNPSTORESYSDBContext())
                        {
                            data.rp.replenishD = _dbs.TrnreplenishDs.Where(t => t.TranStatus == "OPN"
                                                                        && t.Wlid == model.WLCode).ToList();

                        }
                    }

                    if (data.rp.replenishD == null && data.rp.replenish == null)
                    {
                        ViewData["Message"] = "ไม่พบข้อมูลการสั่งเติม";
                    }
                    else
                    {
                        ViewData["Message"] = "";
                    }
                }
                else { return RedirectToAction("Login", "Authen"); }

            }
            catch (Exception ex)
            {
                ViewData["ErrorID"] = 1;
                ViewData["Message"] = ex.Message;

            }

            return View(data);
        }


        //POS: ReplenishController

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ModelLayout rdata, decimal Id,int datakey)
        {
            try
            {
                //assgin tmp
               if(Id!=0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    info = new();
                    rdata.pi = new();
                    

                    if (rdata.barcode != null)
                    {
                        rdata.menukey = datakey;
                        rdata.Id = Id;
                        rdata.Loccat = "S";
                        rdata.selectLoctype = rdata.selectLoctype;
                        rdata = info.SearchProduct(rdata);

                        if (rdata.Qty != 0 )
                        {
                            ClassModel model = new();
                            _dbs = new();
                            model = GetDBConnect.GetClassModel(Id);
                            DateOnly datenow = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day); //ติด Dateonly

                            TmpreplenishD idata = new()
                            {
                                TranDate = datenow,
                                Wlid = model.WLCode,
                                RecNo = Id.ToString(),
                                RecdNo = SerachMaxitemNo(Id.ToString(), model.DBKey, model.ConnectString, model.WLCode) + 1,
                                Barcode = rdata.barcode,
                                ItemCode = rdata.pi.MstProd.ProdId,
                                ItemName = rdata.pi.MstProd.LongDescription,
                                ItemQty = rdata.Qty,
                                UnitName = rdata.pi.MstBarcode.UnitName,
                                Unit = (int)rdata.pi.MstBarcode.ProdBarUnit,
                                UnitType = rdata.pi.MstBarcode.ProdBarType,
                                LocIdfm = rdata.Locidfm, //sheft
                                LocIdto = rdata.Locidto, //reserve
                                TranType = 0,
                                TranFlag = 1,
                                TranStatus = "CRE",
                                Logid = Id,
                                CreateUser = model.UserName,

                            };
                            if (idata != null)
                            {
                                _dbs.TmpreplenishDs.Add(idata);
                                _dbs.SaveChanges();
                                rdata = new();
                                rdata.Message = "บันทึกข้อมูลเรียบร้อย";
                                if (rdata.Locidto == string.Empty)
                                {
                                    rdata.Message = "บันทึกข้อมูลเรียบร้อย : ยังไม่มีการกำหนดตำแหน่งเก็บ";
                                }
                                rdata.Saveflg = 0;
                               
                            }
                        }
                    }
                   
                }
                else
                {
                    return RedirectToAction("Login", "Authen");
                }

            }
            catch(Exception ex) 
            {
                rdata.Saveflg = 1;
                rdata.Message = ex.Message;

            }

            rdata.menukey = datakey;
            rdata.Id = Id;
            return RedirectToAction("Index", "Replenish", rdata);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(ModelLayout rdata, decimal Id,int datakey)
        {
            int errorid = 0;
            string message = string.Empty;
            string docno = string.Empty;
            try
            {
                //assgin replce production
               
                if (Id != 0)
                {
                    ClassModel model = new();
                    model = GetDBConnect.GetClassModel(Id);
                    GetDocRunning getDocRunning = new();
                    docno=getDocRunning.GenDoc(200, DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy"), "RPL", model.DCcode, model.WHcode, model.ConnectString,model.WLCode);
                    if (docno != string.Empty)
                    {
                        if(InsertTRNreplenish(Id, model.MainDB, model.ConnectString, docno,model.WLCode,model.UserName,model.Connectmain.Trim()))
                        {
                            if(InsertTRNreplenishH(Id, model.MainDB, model.ConnectString, docno, model.WLCode))
                            {
                               
                                message = "สั่งเติมสินค้าเรียบร้อย >> เอกสารสั่งเติม :"+docno;
                            }


                            //Mssage
                        }
                        else
                        {
                            errorid = 1;
                            message = "สั่งเติมสืนค้าผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
                            //message 
                        }
                    }
                  
                }
              
            }
            catch(Exception ex) 
            {
                errorid = 1;
                message=ex.Message;
               
            }
            return RedirectToAction("RepSummary", new { Id = Id, error = errorid, msg = message ,datakey=datakey, docno =docno});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(decimal Id, int datakey, string barcode)
        {
            int errorid = 0;
            string message = string.Empty;
            try
            {
                //assgin replce production

                if (Id != 0 && barcode != string.Empty)
                {
                    ClassModel model = new();
                    model = GetDBConnect.GetClassModel(Id);

                    if (DeleteTmpreplenish(Id, model.MainDB, model.ConnectString, barcode))
                    {
                        message = "ลบข้อมูลเรียบร้อย";

                        //Mssage
                    }
                    else
                    {
                        errorid = 1;
                        message = "ลบข้อมูลไม่สำเร็จ กรุณาติดต่อผู้ดูแลระบบ";
                        //message 
                    }



                }

                
            }
            catch( Exception ex) { errorid = 1; message = ex.Message; }
           
            return RedirectToAction("RepSummary", new { Id = Id, error = errorid, msg = message, datakey = datakey });
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectType(ModelLayout data,decimal Id,int datakey)
        {
           
            return RedirectToAction("RepList", new { Id = Id,  datakey = datakey, type = data.Options[0] });
        }
        //GET Fuction: ReplenishController
        private bool InsertTRNreplenish(decimal Id, string DBname, string DbConnext, string docno, string wlcode,string username,string MainConnect)
        {
            info = new();
            GetDBConnect dB = new();
            
            bool flg = true;
            string recno = DateTime.Now.ToString("yyMMddHHmmssfff");
            recno = wlcode.Trim() + recno;
            try
            {
                string SQL = $"	SELECT [RecNo]     	" +
                                $"	        ,[Barcode]	" +
                                $"	        ,[ItemCode]	" +
                                $"	        ,[ItemName]	" +
                                $"	        ,sum([ItemQty])	" +
                                $"	        ,[UnitName]	" +
                                $"	        ,[Unit]	" +
                                $"	        ,[UnitType]	" +
                                $"	        ,isnull([LocIDTo],'')	" +
                                $"	        ,isnull([LocIDFm],'')	" +
                                $"	        ,[CreateUser]	" +
                                $"	        ,[Logid],[wlid],[TranDate],max(recdno)" +
                                $"	  FROM [TMPreplenishD]	" +
                                $"	  where [TranDate] =convert(date,GETDATE())" +
                                $"	  and [wlid] ='{wlcode}'	" +
                                $"    and [CreateUser] ='{username.Trim()}'"+
                                $"	  and [TranType] =0	" +
                                $"	  and [TranStatus] ='CRE'" +
                                $"	  group by [RecNo]	" +
                                $"	  ,[Barcode]	" +
                                $"	  ,[ItemCode]	" +
                                $"	  ,[ItemName]	" +
                                $"	  ,[Unit]	" +
                                $"	  ,[UnitName]	" +
                                $"	  ,[UnitType]	" +
                                $"	  ,[LocIDTo]	" +
                                $"	  ,[LocIDFm]	" +
                                $"	  ,[CreateUser]	" +
                                $"	  ,[Logid],[wlid],[TranDate]	" +
                                $"	  order by [ItemCode]	";
                if (dB.ExecuteReadData(SQL, DbConnext))
                {
                    if (dB.myReader.HasRows)
                    {
                        while (dB.myReader.Read())
                        {
                            UnitBarcode unitBarcode = info.GetUnitBarcode(dB.myReader[2].ToString(), DBname, MainConnect);
                            

                            using (var _dbs = new TNPSTORESYSDBContext())
                            {

                                TrnreplenishD tranrpld = new()
                                {
                                    Wlid = dB.myReader[12].ToString(),
                                    RecNo = recno,
                                    RecdNo = (int)dB.myReader[14],
                                    DocNo = docno,
                                    Barcode = dB.myReader[1].ToString(),
                                    ItemCode = dB.myReader[2].ToString(),
                                    ItemName = dB.myReader[3].ToString(),
                                    ItemQty = (int)dB.myReader[4],
                                    ItemQtyRep = 0,
                                    GroupId = unitBarcode.groupid,
                                    UnitName = dB.myReader[5].ToString(),
                                    Unit = (int)dB.myReader[6],
                                    UnitI = unitBarcode.UnitI,
                                    UnitC = unitBarcode.UnitC,
                                    UnitU = unitBarcode.UnitU,
                                    QtyUnit = (unitBarcode.UnitU) * (int)dB.myReader[4],
                                    UnitType = dB.myReader[7].ToString(),
                                    TranType = 0,
                                    LocIdto = dB.myReader[8].ToString(),
                                    LocIdfm = dB.myReader[9].ToString(),
                                    TranStatus = "OPN",
                                    TranFlag = 2, //1:CRE , 2:OPN ,3:RPL
                                    CreateUser = dB.myReader[10].ToString(),
                                    CreateDtime = DateTime.Now,
                                    ChangeUser = dB.myReader[10].ToString(),
                                    ChangeDtime = DateTime.Now,
                                };
                                _dbs.TrnreplenishDs.Add(tranrpld);
                                _dbs.SaveChanges();

                                SQL = $"  DELETE " +
                                     $"	  FROM [dbo].[TMPreplenishD]	" +
                                     $"	  where RecNo ={dB.myReader[0].ToString()} " +
                                     $"	  and wlid ='{wlcode}'	" +
                                     $"   and Barcode='{dB.myReader[1].ToString().Trim()}'";

                                if (dB.ExecuteTransData(SQL, DbConnext))
                                {
                                    dB.CloseDB();
                                   
                                }
                            }
                        }

                    }
                    else
                    {
                        flg = false;
                    }

                }
                else { flg = false; }
                dB.CloseDB();

                return flg;
            }
            catch (Exception) { return false; }

        }
        private bool InsertTRNreplenishH(decimal Id, string DBname, string DbConnext, string docno, string wlcode)
        {
            GetDBConnect dB = new();

            bool flg = true;
            try
            {
                string SQL = $" INSERT INTO [TRNreplenish]" +
                            $"	SELECT [wlid]" +
                            $"        ,[RecNo]	" +
                             $"	      ,[DocNo]	" +
                             $"	      ,200	" +
                             $"	      ,convert(date,max([CreateDtime])) as TranDate 	" +
                             $"	      ,TranFlag as Trancode	" +
                             $"	      ,count([RecdNo]),0	" +
                             $"	      ,[TranStatus]	" +
                             $"	      ,[CreateUser]	" +
                             $"	      ,getdate()	" +
                             $"	      ,[ChangeUser]	" +
                             $"	      ,getdate()	" +
                             $"	      ,{Id} as Logid 	" +
                             $"	  FROM [dbo].[TRNreplenishD]	" +
                             $"	  where DocNo ='{docno}'	" +
                             $"	  and wlid ='{wlcode}'	" +
                             $"	  and [TranStatus] ='OPN'" +
                             $"	  group by [RecNo]	" +
                             $"	      ,[DocNo]	" +
                             $"	  ,[TranFlag]	" +
                             $"	  ,[ChangeUser]	" +
                             $"	  ,[CreateUser]	" +
                             $"	  ,[TranStatus],[wlid]	";
                if (dB.ExecuteTransData(SQL, DbConnext))
                {
                    dB.CloseDB();
                    

                }

            }
            catch (Exception) { flg = false; }

            return flg;
        }
       
        private bool DeleteTmpreplenish(decimal Id, string DBname, string DbConnext, string barcode)
        {
            try
            {


                using (var dt = new TNPSTORESYSDBContext())
                {
                    var data = (from t in dt.TmpreplenishDs
                                where t.Barcode == barcode
                                && t.Logid == Id
                                select t).FirstOrDefault();

                    dt.Remove(data);
                    dt.SaveChanges();

                }
            }
            catch (Exception) { }
            return true;
        }
        public int SerachMaxitemNo(string Id, string DBname, string DbConnext, string wlcode)
        {
            int itemno = 0;
            int titemno = 0;
            GetDBConnect dB = new();
            string SQL = $"use {DBname}" +
                         $" select isnull(max(RecdNo),0) " +
                         $" from TRNreplenishD" +
                         $" where wlid={wlcode}"+
                         $" and convert(date,CreateDtime) =convert(date,Getdate()) ";
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

             SQL = $"use {DBname}" +
                         $" select isnull(max(RecdNo),0) " +
                         $" from TMPreplenishD" +
                         $" where wlid={wlcode}" +
                         $" and TranDate =convert(date,Getdate()) ";
            if (dB.ExecuteReadData(SQL, DbConnext))
            {
                if (dB.myReader.HasRows)
                {
                    while (dB.myReader.Read())
                    {

                        titemno = (int)dB.myReader[0];
                    }
                }

            }

            if(titemno > itemno)
            {
                itemno= titemno;
            }
            return itemno;
        }



    }
}

