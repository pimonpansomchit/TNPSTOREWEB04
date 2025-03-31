
#nullable disable
using Microsoft.AspNetCore.Mvc;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Core;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;
namespace TNPSTOREWEB.Controllers
{
    public class LocationController : Controller
    {
        private TNPSTORESYSDBContext _dbs;
        private GetProdInfo info;

        // GET: LocationController
        public ActionResult Blocation(ClassModel model, ModelLayout rdata)
        {

            if (model.logid != 0 && rdata.Id == null)
            {
                rdata.Id = model.logid;
                rdata.menukey = model.menukey;
                rdata.Message = "";
                rdata.Saveflg = 0;
            }
          
            if(rdata.Id!=0)
            {
                info = new();
                rdata.pi = new();
                rdata.pi.stLoctypes = info.GetLoctype("S");

                if (rdata.barcode != null) //after input barcode and display
                {
                    rdata = info.SearchProduct(rdata);
                    if (rdata.pi.iprodinv == null)
                    {
                        if (rdata.selectLoctype != 0)
                        {
                            rdata.pi.iprodinv = new();
                            rdata.pi.iprodinv.LocType = rdata.selectLoctype;
                            rdata.pi.iprodinv.LocTypeName = rdata.pi.stLoctypes.Where(p => p.LocType == rdata.selectLoctype).Select(t => t.LocTypeName).FirstOrDefault();
                        }
                    }
                    else
                    {

                        rdata.pi.iprodinv.Locrack = rdata.pi.iprodinv.Locrack.Trim();
                        rdata.pi.iprodinv.Loclev = rdata.pi.iprodinv.Loclev.Trim();
                        rdata.selectLoctype = rdata.pi.iprodinv.LocType;
                    }
                }
                
            }
            else { return RedirectToAction("Login", "Authen"); }
            
           
            
            return View(rdata);

        }
        public ActionResult Wlocation(ClassModel model, ModelLayout rdata)
        {
            if (model.logid != 0 && rdata.Id == null)
            {
                rdata.Id = model.logid;
                rdata.menukey = model.menukey;
                rdata.Message = "";
                rdata.Saveflg = 0;
            }

            if (rdata.Id != 0)
            {


                info = new();
                rdata.pi = new();
                rdata.pi.stLoctypes = info.GetLoctype("R");

                if (rdata.barcode != null) //after input barcode and display
                {
                    rdata = info.SearchProduct(rdata);
                    if (rdata.pi.iprodinv == null)
                    {
                        if (rdata.selectLoctype != 0)
                        {
                            rdata.pi.iprodinv = new();
                            rdata.pi.iprodinv.LocType = rdata.selectLoctype;
                            rdata.pi.iprodinv.LocTypeName = rdata.pi.stLoctypes.Where(p => p.LocType == rdata.selectLoctype).Select(t => t.LocTypeName).FirstOrDefault();
                        }
                    }
                    else
                    {

                        rdata.pi.iprodinv.Locrack = rdata.pi.iprodinv.Locrack.Trim();
                        rdata.pi.iprodinv.Loclev = rdata.pi.iprodinv.Loclev.Trim();
                        rdata.selectLoctype = rdata.pi.iprodinv.LocType;
                    }
                }

            }
            else
            {
                return RedirectToAction("Login", "Authen");
            }
            return View(rdata);

        }

        public ActionResult LocationQury(ClassModel model, LocdataList rdata)
        {
            if (model.logid != 0 && rdata.Id == null)
            {
                rdata.Id = model.logid;
                rdata.menukey = model.menukey;
                rdata.Message = "";
                rdata.SaveFlg = 0;
            }
            try
            {
                if(rdata.Id!=0)
                {
                    info = new();
                    if (rdata.barcode != null)
                    {
                        rdata = info.SearchProductLoc(rdata);
                        if (rdata.locinfo == null)
                        {
                            rdata.SaveFlg = 1;
                            rdata.Message = "ไม่พบข้อมูลตำแหน่งเก็บ";

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
                rdata.SaveFlg = 1;
                rdata.Message = ex.Message;


            }
            return View(rdata);
        }

        // POST: LocationController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Blocation(ModelLayout rdata, decimal Id, int datakey, string actions, string loccat)
        {
            try
            {
                //assgin tmp
                if (Id != 0)
                {
                    string locCode = string.Empty;
                    rdata.Saveflg = 0;
                    rdata.Message = string.Empty;

                    info = new();

                    if (actions == "S" && rdata.barcode != null)
                    {
                        ClassModel model = new();
                        _dbs = new();

                        model = GetDBConnect.GetClassModel(Id);
                        rdata.pi.stLoctypes = info.GetLoctype("S");

                        if (rdata.selectLoctype != 0)
                        {
                            rdata.pi.iprodinv.LocType = rdata.selectLoctype;
                            rdata.pi.iprodinv.LocTypeName = rdata.pi.stLoctypes.Where(p => p.LocType == rdata.selectLoctype).Select(t => t.LocTypeName).FirstOrDefault();

                            //###### Assing valeu to iprodinv ######//
                            if (rdata.pi.iprodinv.Loczone == string.Empty)
                            {
                                rdata.pi.iprodinv.Loczone = "00";
                            }
                            if (rdata.pi.iprodinv.Locrack == string.Empty)
                            {
                                rdata.pi.iprodinv.Locrack = "00";
                            }
                            if (rdata.pi.iprodinv.Loclev == string.Empty)
                            {
                                rdata.pi.iprodinv.Loclev = "00";
                            }

                            rdata.pi.iprodinv.Loccat = loccat;
                            rdata.pi.iprodinv.Loczone = rdata.pi.iprodinv.Loczone.Trim();
                            rdata.pi.iprodinv.Locrack = rdata.pi.iprodinv.Locrack.Trim();
                            rdata.pi.iprodinv.Loclev = rdata.pi.iprodinv.Loclev.Trim();
                            rdata.pi.iprodinv.StackPos = rdata.pi.iprodinv.StackPos;
                            rdata.pi.iprodinv.StackDep = rdata.pi.iprodinv.StackDep;
                            rdata.pi.iprodinv.StackHgt = rdata.pi.iprodinv.StackHgt;


                            locCode = (rdata.selectLoctype + "-" + rdata.pi.iprodinv.Loczone.Trim() + rdata.pi.iprodinv.Locrack.Trim() + rdata.pi.iprodinv.Loclev.Trim());
                            LocInfo locInfo = SerachMaxitemNo(locCode.ToString(), model.DBKey, model.ConnectString, model.WLCode);
                            int casecap = (int)(rdata.pi.iprodinv.StackPos * rdata.pi.iprodinv.StackDep * rdata.pi.iprodinv.StackHgt);
                            //###### End Assing valeu to iprodinv ######//

                            rdata = info.SearchProduct(rdata);
                            Locprdd idata = new()
                            {
                                Wlid = model.WLCode,
                                LocId = locInfo.LocId,
                                GroupId = rdata.groupId,
                                ProdKey = rdata.pi.MstProd.ProdKey,
                                ProdId = rdata.pi.MstProd.ProdId,
                                ProdName = rdata.pi.MstProd.LongDescription,
                                BarcodeKey = rdata.pi.MstBarcode.BarcodeKey,
                                Barcode = rdata.barcode,
                                Unit = (int)rdata.pi.MstBarcode.ProdBarUnit,
                                Unittype = rdata.pi.MstBarcode.ProdBarType,
                                Unitname = rdata.pi.MstBarcode.UnitName,
                                LocCode = locCode.Trim(),
                                Loccat = rdata.pi.iprodinv.Loccat.Trim(),
                                LocType = rdata.pi.iprodinv.LocType,
                                LocTypeName = rdata.pi.iprodinv.LocTypeName,
                                Loczone = rdata.pi.iprodinv.Loczone,
                                Locrack = rdata.pi.iprodinv.Locrack,
                                Loclev = rdata.pi.iprodinv.Loclev,
                                Locpos = locInfo.LocPos,
                                StackPos = rdata.pi.iprodinv.StackPos,
                                StackDep = rdata.pi.iprodinv.StackDep,
                                StackHgt = rdata.pi.iprodinv.StackHgt,
                                Casecap = casecap,
                                Minsel = rdata.pi.iprodinv.StackPos,
                                Rplnamt = casecap - rdata.pi.iprodinv.StackPos,
                                BohQty = rdata.pi.iprodinv.BohQty,
                                Locstatus = 1,
                                CreateUser = model.UserName,
                                CreateDtime = DateTime.Now,
                                ChangeUser = model.UserName,
                                ChangeDtime = DateTime.Now,
                                Logid = Id,

                            };
                            GetDBConnect dB = new();
                            if (idata != null && rdata.pi.iprodinv.LocId == null)
                            {
                                

                                string SQL = $"INSERT INTO LOCprdd{model.WLCode.Trim()}" +
                                  $"([wlid]" +
                                   $",[locID]" +
                                   $",[groupId]" +
                                   $",[prodKey]" +
                                   $",[prodID]" +
                                   $",[prodName]" +
                                   $",[barcodeKey]" +
                                   $",[barcode]" +
                                   $",[unit]" +
                                   $",[unittype]" +
                                   $",[unitname]" +
                                   $",[locCode]" +
                                   $",[loccat]" +
                                   $",[locType]" +
                                   $",[locTypeName]" +
                                   $",[loczone]" +
                                   $",[locrack]" +
                                   $",[loclev]" +
                                   $",[locpos]" +
                                   $",[stackPos]" +
                                   $",[stackDep]" +
                                   $",[stackHgt]" +
                                   $",[casecap]" +
                                   $",[minsel]" +
                                   $",[rplnamt]" +
                                   $",[bohQty]" +
                                   $",[locstatus]" +
                                   $",[create_user]" +
                                   $",[create_dtime]" +
                                   $",[change_user]" +
                                   $",[change_dtime]" +
                                   $",[logid] )" +
                                   $" VALUES (" +
                                $"'{idata.Wlid.Trim()}'," +
                                $"'{idata.LocId}',"+
                                $"'{idata.GroupId}',"+
                                $"{idata.ProdKey}," +
                                $"'{idata.ProdId}',"+
                                $"'{idata.ProdName}',"+
                                $"{idata.BarcodeKey},"+
                                $"'{idata.Barcode}'," +
                                $"{idata.Unit},"+
                                $"'{idata.Unittype}',"+
                                $"'{idata.Unitname}',"+
                                $"'{idata.LocCode}',"+
                                $"'{idata.Loccat}',"+
                                $"{idata.LocType},"+
                                $"'{idata.LocTypeName}',"+
                                $"'{idata.Loczone}'," +
                                $"'{idata.Locrack}'," +
                                $"'{idata.Loclev}'," +
                                $"'{idata.Locpos}'," +
                                $"{idata.StackPos},"+
                                $"{idata.StackDep},"+
                                $"{idata.StackHgt},"+
                                $"{idata.Casecap},"+
                                $"{idata.Minsel}," +
                                $"{idata.Rplnamt}," +
                                $"{0}," +
                                $"{idata.Locstatus}," +
                                $"'{idata.CreateUser}'," +
                                $"'{idata.CreateDtime}'," +
                                $"'{idata.ChangeUser}'," +
                                $"'{idata.ChangeDtime}'," +
                                $"'{idata.Logid}')";
                                if (dB.ExecuteTransData(SQL, model.ConnectString.Trim()))
                                {
                                    dB.CloseDB();
                                    rdata = new();
                                }

                            }
                            else
                            {


                                string SQL = $"UPDATE LOCprdd{model.WLCode.Trim()}" +
                                  $" SET [locID] = '{idata.LocId.Trim()}'" +
                                   $",[locCode] ='{idata.LocCode}'" +
                                    $",[loczone]='{idata.Loczone}'" +
                                   $" ,[locrack]='{idata.Locrack}'" +
                                   $" ,[loclev]='{idata.Loclev}'" +
                                   $" ,[locpos]='{idata.Locpos}'" +
                                   $",[stackPos]={idata.StackPos}" +
                                   $",[stackDep]={idata.StackDep}" +
                                   $",[stackHgt]={idata.StackHgt}" +
                                   $",[casecap]={idata.Casecap}" +
                                   $",[minsel]={idata.Minsel}" +
                                   $",[rplnamt]={idata.Rplnamt}" +
                                   $",[change_user] ='{idata.ChangeUser}' " +
                                   $",[change_dtime]=Getdate()" +
                                   $",[logid]={idata.Logid} " +
                                   $" where loccat ='{idata.Loccat}'" +
                                   $" and barcode ='{idata.Barcode}'" +
                                   $" and wlid ='{idata.Wlid}'" +
                                   $" and locType ={idata.LocType}" +
                                   $" and barcodekey ={idata.BarcodeKey}" +
                                   $" and prodID ='{idata.ProdId}'" +
                                   $" and unittype ='{idata.Unittype}'";
                                if (dB.ExecuteTransData(SQL, model.ConnectString.Trim()))
                                {
                                    dB.CloseDB();
                                    rdata = new();
                                }

                              

                            }
                            rdata.Saveflg = 0;
                            rdata.Message = "บันทึกข้อมูลเรียบร้อย";
                        }
                        else
                        {
                            rdata.Saveflg = 1;
                            rdata.Message = "กรุณาเลือกประเภทของตำแหน่งเก็บ";
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
            rdata.menukey = datakey;
            rdata.Loccat = loccat.Trim();
            rdata.Id = Id;

            return RedirectToAction("BLocation", "Location", rdata);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Wlocation(ModelLayout rdata, decimal Id, int datakey, string actions, string loccat)
        {
            try
            {
                //assgin tmp
                if (Id != 0)
                {
                    string locCode = string.Empty;
                    rdata.Saveflg = 0;
                    rdata.Message = string.Empty;

                   
                    
                    info = new();

                    if (actions == "S" && rdata.barcode != null)
                    {
                        ClassModel model = new();
                        _dbs = new();

                        model = GetDBConnect.GetClassModel(Id);
                        rdata.pi.stLoctypes = info.GetLoctype("R");

                        if (rdata.selectLoctype != 0)
                        {
                            rdata.pi.iprodinv.LocType = rdata.selectLoctype;
                            rdata.pi.iprodinv.LocTypeName = rdata.pi.stLoctypes.Where(p => p.LocType == rdata.selectLoctype).Select(t => t.LocTypeName).FirstOrDefault();

                            //###### Assing valeu to iprodinv ######//
                            if (rdata.pi.iprodinv.Loczone == string.Empty)
                            {
                                rdata.pi.iprodinv.Loczone = "00";
                            }
                            if (rdata.pi.iprodinv.Locrack == string.Empty)
                            {
                                rdata.pi.iprodinv.Locrack = "00";
                            }
                            if (rdata.pi.iprodinv.Loclev == string.Empty)
                            {
                                rdata.pi.iprodinv.Loclev = "00";
                            }

                            rdata.pi.iprodinv.Loccat = loccat;
                            rdata.pi.iprodinv.Loczone = rdata.pi.iprodinv.Loczone.Trim();
                            rdata.pi.iprodinv.Locrack = rdata.pi.iprodinv.Locrack.Trim();
                            rdata.pi.iprodinv.Loclev = rdata.pi.iprodinv.Loclev.Trim();
                            rdata.pi.iprodinv.StackPos = rdata.pi.iprodinv.StackPos;
                            rdata.pi.iprodinv.StackDep = rdata.pi.iprodinv.StackDep;
                            rdata.pi.iprodinv.StackHgt = rdata.pi.iprodinv.StackHgt;


                            locCode = (rdata.selectLoctype + "-" + rdata.pi.iprodinv.Loczone.Trim() + rdata.pi.iprodinv.Locrack.Trim() + rdata.pi.iprodinv.Loclev.Trim());
                            LocInfo locInfo = SerachMaxitemNo(locCode.ToString(), model.DBKey, model.ConnectString, model.WLCode);
                            int casecap = (int)(rdata.pi.iprodinv.StackPos * rdata.pi.iprodinv.StackDep * rdata.pi.iprodinv.StackHgt);
                            //###### End Assing valeu to iprodinv ######//

                            rdata = info.SearchProduct(rdata);
                            Locprdd idata = new()
                            {
                                Wlid = model.WLCode,
                                LocId = locInfo.LocId,
                                GroupId = rdata.groupId,
                                ProdKey = rdata.pi.MstProd.ProdKey,
                                ProdId = rdata.pi.MstProd.ProdId,
                                ProdName = rdata.pi.MstProd.LongDescription,
                                BarcodeKey = rdata.pi.MstBarcode.BarcodeKey,
                                Barcode = rdata.barcode,
                                Unit = (int)rdata.pi.MstBarcode.ProdBarUnit,
                                Unittype = rdata.pi.MstBarcode.ProdBarType,
                                Unitname = rdata.pi.MstBarcode.UnitName,
                                LocCode = locCode.Trim(),
                                Loccat = rdata.pi.iprodinv.Loccat.Trim(),
                                LocType = rdata.pi.iprodinv.LocType,
                                LocTypeName = rdata.pi.iprodinv.LocTypeName,
                                Loczone = rdata.pi.iprodinv.Loczone,
                                Locrack = rdata.pi.iprodinv.Locrack,
                                Loclev = rdata.pi.iprodinv.Loclev,
                                Locpos = locInfo.LocPos,
                                StackPos = rdata.pi.iprodinv.StackPos,
                                StackDep = rdata.pi.iprodinv.StackDep,
                                StackHgt = rdata.pi.iprodinv.StackHgt,
                                Casecap = casecap,
                                Minsel = rdata.pi.iprodinv.StackPos,
                                Rplnamt = casecap - rdata.pi.iprodinv.StackPos,
                                BohQty = rdata.pi.iprodinv.BohQty,
                                Locstatus = 1,
                                CreateUser = model.UserName,
                                CreateDtime = DateTime.Now,
                                ChangeUser = model.UserName,
                                ChangeDtime = DateTime.Now,
                                Logid = Id,

                            };
                            GetDBConnect dB = new();
                            if (idata != null && rdata.pi.iprodinv.BarcodeKey == 0)
                            {


                                string SQL = $"INSERT INTO LOCprdd{model.WLCode.Trim()}" +
                                  $"([wlid]" +
                                   $",[locID]" +
                                   $",[groupId]" +
                                   $",[prodKey]" +
                                   $",[prodID]" +
                                   $",[prodName]" +
                                   $",[barcodeKey]" +
                                   $",[barcode]" +
                                   $",[unit]" +
                                   $",[unittype]" +
                                   $",[unitname]" +
                                   $",[locCode]" +
                                   $",[loccat]" +
                                   $",[locType]" +
                                   $",[locTypeName]" +
                                   $",[loczone]" +
                                   $",[locrack]" +
                                   $",[loclev]" +
                                   $",[locpos]" +
                                   $",[stackPos]" +
                                   $",[stackDep]" +
                                   $",[stackHgt]" +
                                   $",[casecap]" +
                                   $",[minsel]" +
                                   $",[rplnamt]" +
                                   $",[bohQty]" +
                                   $",[locstatus]" +
                                   $",[create_user]" +
                                   $",[create_dtime]" +
                                   $",[change_user]" +
                                   $",[change_dtime]" +
                                   $",[logid] )" +
                                   $" VALUES (" +
                                $"'{idata.Wlid.Trim()}'," +
                                $"'{idata.LocId}'," +
                                $"'{idata.GroupId}'," +
                                $"{idata.ProdKey}," +
                                $"'{idata.ProdId}'," +
                                $"'{idata.ProdName}'," +
                                $"{idata.BarcodeKey}," +
                                $"'{idata.Barcode}'," +
                                $"{idata.Unit}," +
                                $"'{idata.Unittype}'," +
                                $"'{idata.Unitname}'," +
                                $"'{idata.LocCode}'," +
                                $"'{idata.Loccat}'," +
                                $"{idata.LocType}," +
                                $"'{idata.LocTypeName}'," +
                                $"'{idata.Loczone}'," +
                                $"'{idata.Locrack}'," +
                                $"'{idata.Loclev}'," +
                                $"'{idata.Locpos}'," +
                                $"{idata.StackPos}," +
                                $"{idata.StackDep}," +
                                $"{idata.StackHgt}," +
                                $"{idata.Casecap}," +
                                $"{idata.Minsel}," +
                                $"{idata.Rplnamt}," +
                                $"{0}," +
                                $"{idata.Locstatus}," +
                                $"'{idata.CreateUser}'," +
                                $"'{idata.CreateDtime}'," +
                                $"'{idata.ChangeUser}'," +
                                $"'{idata.ChangeDtime}'," +
                                $"'{idata.Logid}')";
                                if (dB.ExecuteTransData(SQL, model.ConnectString.Trim()))
                                {
                                    dB.CloseDB();
                                    rdata = new();
                                }

                            }
                            else
                            {


                                string SQL = $"UPDATE LOCprdd{model.WLCode.Trim()}" +
                                  $" SET [locID] = '{idata.LocId.Trim()}'" +
                                   $",[locCode] ='{idata.LocCode}'" +
                                    $",[loczone]='{idata.Loczone}'" +
                                   $" ,[locrack]='{idata.Locrack}'" +
                                   $" ,[loclev]='{idata.Loclev}'" +
                                   $" ,[locpos]='{idata.Locpos}'" +
                                   $",[stackPos]={idata.StackPos}" +
                                   $",[stackDep]={idata.StackDep}" +
                                   $",[stackHgt]={idata.StackHgt}" +
                                   $",[casecap]={idata.Casecap}" +
                                   $",[minsel]={idata.Minsel}" +
                                   $",[rplnamt]={idata.Rplnamt}" +
                                   $",[change_user] ='{idata.ChangeUser}' " +
                                   $",[change_dtime]=Getdate()" +
                                   $",[logid]={idata.Logid} " +
                                   $" where loccat ='{idata.Loccat}'" +
                                   $" and barcode ='{idata.Barcode}'" +
                                   $" and wlid ='{idata.Wlid}'" +
                                   $" and locType ={idata.LocType}" +
                                   $" and barcodekey ={idata.BarcodeKey}" +
                                   $" and prodID ='{idata.ProdId}'" +
                                   $" and unittype ='{idata.Unittype}'";
                                if (dB.ExecuteTransData(SQL, model.ConnectString.Trim()))
                                {
                                    dB.CloseDB();
                                    rdata = new();
                                }



                            }
                            rdata.Saveflg = 0;
                            rdata.Message = "บันทึกข้อมูลเรียบร้อย";
                        }
                        else
                        {
                            rdata.Saveflg = 1;
                            rdata.Message = "กรุณาเลือกประเภทของตำแหน่งเก็บ";
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
            rdata.menukey = datakey;
            rdata.Loccat = loccat.Trim();
            rdata.Id = Id;

            return RedirectToAction("WLocation", "Location", rdata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SerachData(LocdataList data, decimal Id, int datakey)
        {
            data.menukey = datakey;
            data.Id = Id;
            return RedirectToAction("LocationQury","Location",data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClearData(LocdataList data, decimal Id, int datakey)
        {
            data = new();
            data.menukey = datakey;
            data.Id = Id;
            return RedirectToAction("LocationQury", "Location", data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(decimal Id, int datakey, string barcode,string locid , string loccat ,decimal logid)
        {
            LocdataList data =new LocdataList();
            data.menukey = datakey;
            data.Id = Id;
            data.barcode = barcode;
            try
            {
                //assgin replce production

                if (Id != 0 && barcode != string.Empty)
                {
                    ClassModel model = new();
                    model = GetDBConnect.GetClassModel(Id);

                    if (DeleteLocprdd(logid,model.MainDB, model.ConnectString,barcode, locid,loccat,model.WLCode))
                    {
                        data.Message = "ลบข้อมูลเรียบร้อย";

                        //Mssage
                    }
                    else
                    {
                        data.SaveFlg = 1;
                        data.Message = "ลบข้อมูลไม่สำเร็จ กรุณาติดต่อผู้ดูแลระบบ";
                        //message 
                    }



                }


            }
            catch (Exception ex) { data.SaveFlg = 1; data.Message = ex.Message; }
            data.SaveFlg = 0;
            return RedirectToAction("LocationQury", "Location", data);
        }
        //Get Fundction
        public LocInfo SerachMaxitemNo(string LocCode, string DBname, string DbConnext, string wlcode)
        {
            string itemno = string.Empty;
            GetDBConnect dB = new();
            LocInfo loc = new();
            string SQL = $"use {DBname}" +
                         $" select format((isnull(count(locID),0)+1),'00') " +
                         $" from LOCprdd" +
                         $" where locCode={LocCode}" +
                         $" and wlid={wlcode}";
            if (dB.ExecuteReadData(SQL, DbConnext))
            {
                if (dB.myReader.HasRows)
                {
                    while (dB.myReader.Read())
                    {
                        loc.LocId = LocCode.Trim() + dB.myReader[0].ToString();
                        loc.LocPos = dB.myReader[0].ToString();
                        loc.LocCode = LocCode.Trim();

                    }
                }
                else
                {
                    loc.LocId = LocCode.Trim() + "01";
                    loc.LocPos = "01";
                    loc.LocCode = LocCode.Trim();
                }

            }

            return loc;
        }
        private bool DeleteLocprdd(decimal logid,string DBname, string DbConnext, string barcode,string locid, string loccat,string wlcode)
        {
            try
            {
                GetDBConnect dB = new();
                string SQL = $"	DELETE " +
                                $"	FROM LOCprdd{wlcode.Trim()}" +
                                $"  where Barcode ='{barcode.Trim()}'" +
                                $"  and wlid='{wlcode.Trim()}'" +
                                $"  and Logid ={logid}" +
                                $"  and LocId ='{locid.Trim()}'" +
                                $"  and Loccat ='{loccat.Trim()}'";
                               
                if (dB.ExecuteTransData(SQL, DbConnext.Trim()))
                {
                    dB.CloseDB();
                }

                
                
            }
            catch (Exception) { return false; }
            return true;
        }
    }
}
