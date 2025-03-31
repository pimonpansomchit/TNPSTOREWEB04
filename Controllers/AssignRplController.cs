#nullable disable
using Microsoft.AspNetCore.Mvc;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Core;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models.Request;

namespace TNPSTOREWEB.Controllers
{
    public class AssignRplController : Controller
    {
       
        private GetProdInfo info =new();
        public ActionResult Index(ClassModel model, ModelLayout rdata)
        {

            info = new();
            rdata.rp = new();
            if (model.logid != 0 && rdata.Id == null)
            {
                rdata.Id = model.logid;
                rdata.menukey = model.menukey;
                
            }
            else
            {
               
                model = GetDBConnect.GetClassModel(rdata.Id);
                
            }
            if (rdata.Id != null)
            {
                if (rdata.SelectedOption == null)
                {
                    rdata.SelectedOption = "0";
                }
                rdata.Message = "";
                rdata.Saveflg = 0;
                rdata.rp.replenish = new();
                rdata.wlcode = model.WLCode.Trim();
                rdata.rp.prodgroups = info.GetGroupList();
                rdata = info.SearchRelList(rdata);
                if (rdata.rp.replenish == null)
                {
                    rdata.Message = "ไม่พบข้อมูลการขอสั่งเติม";
                    rdata.Saveflg = 1;
                }
                return View(rdata);
            }
            else
            {
                return RedirectToAction("Login", "Authen");
            }
        }

        public ActionResult ItemDetail(ModelLayout rdata)
        {

            info = new();
            ClassModel model = new();
            rdata.rp = new();

            if (rdata.Id != null)
            {
                model = GetDBConnect.GetClassModel(rdata.Id);
                if (rdata.SelectedOption == null)
                {
                    rdata.SelectedOption = "0";
                }
                    rdata.Message = "";
                    rdata.Saveflg = 0;
                    rdata.rp.replenish = new();
                    rdata.wlcode = model.WLCode.Trim();
                    rdata = info.SearchRelListItem(rdata);
                
                if (rdata.rp.replenishD == null)
                {
                    rdata.Message = "ไม่พบข้อมูลรายการขอสั่งเติม";
                    rdata.Saveflg = 1;
                   
                }
            }
            else
            {
                return RedirectToAction("Login", "Authen");
            }
           
            return View(rdata);


        }

        public ActionResult ItemDetailList(ModelLayout rdata)
        {

            info = new();
            ClassModel model = new();
            rdata.rp = new();
            if (rdata.Id != null)
            {
                model = GetDBConnect.GetClassModel(rdata.Id);
                if (rdata.SelectedOption == null)
                {
                    rdata.SelectedOption = "0";
                }
                rdata.Message = "";
                rdata.Saveflg = 0;
                rdata.rp.replenish = new();
                rdata.wlcode = model.WLCode.Trim();
                rdata = info.RelListHistory(rdata);

                if (rdata.rp.replenishD == null)
                {
                    rdata.Message = "ไม่พบข้อมูลรายการขอสั่งเติม";
                    rdata.Saveflg = 1;

                }
            }
            else
            {
                return RedirectToAction("Login", "Authen");
            }

            return View(rdata);


        }

        public ActionResult ScanDetail(ModelLayout rdata)
        {

            info = new();
            ClassModel model = new();
            rdata.rp = new();
            if (rdata.Id != null)
            {
                model = GetDBConnect.GetClassModel(rdata.Id);
               
                rdata.Message = "";
                rdata.Saveflg = 0;
                rdata.rp.replenish = new();
                rdata.wlcode = model.WLCode.Trim();
                rdata = info.SearchRelListItem(rdata);

                if (rdata.rp.replenishD == null)
                {
                    rdata.Message = "ไม่พบข้อมูลรายการขอสั่งเติม";
                    rdata.Saveflg = 1;

                }
            }
            else
            {
                return RedirectToAction("Login", "Authen");
            }

            return View(rdata);


        }

        public ActionResult AssignRplList(ClassModel model, decimal? Id, int datakey, int type)
        {
            info = new();
            ModelLayout rdata = new ModelLayout()
            {
                rp = new() { replenish = new() , replenishD = new() , locprdds = new(), checkQty = new() },
                Options = new[] { "1", "2", "3" },
                SelectedOption = type.ToString(),
            };

            
            if (model.logid != 0 && rdata.Id == null)
            {
                rdata.Id = model.logid;
                rdata.menukey = model.menukey;

            }
            else
            {
                rdata.menukey = datakey;
                rdata.Id = Id;
                model = GetDBConnect.GetClassModel(rdata.Id);

            }
            if (rdata.Id != null)
            {
                if(type ==0 )
                {
                    type=1;
                }
                if (rdata.SelectedOption == null)
                {
                    rdata.SelectedOption = "0";
                }
                else
                {
                    rdata.SelectedOption = type.ToString();

                }
                rdata.Message = "";
                rdata.Saveflg = 0;
                rdata.wlcode = model.WLCode.Trim();
                rdata.display = type;
                rdata = info.RelListHistory(rdata);
                if (rdata.rp.replenish == null )
                {
                    rdata.Message = "ไม่พบข้อมูลการขอสั่งเติม";
                    rdata.Saveflg = 1;
                }
                if (rdata.rp.replenishD == null)
                {
                    rdata.Message = "ไม่พบข้อมูลการขอสั่งเติม";
                    rdata.Saveflg = 1;
                }

            }
            else
            {
                return RedirectToAction("Login", "Authen");
            }
            
            return View(rdata);
        }
        //POS: ReplenishController

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ModelLayout rdata, decimal Id, int datakey)
        {
            try
            {
                //assgin tmp
                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";

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
            rdata.Id = Id;
            return RedirectToAction("Index", "AssignRpl", rdata);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRplList(ModelLayout rdata, decimal Id, int datakey,string type)
        {
            //assgin tmp
            string types = string.Empty;
                if (Id != 0)
                {
                if (rdata.Options != null) {
                    
                        types = rdata.Options[0];
                    
                }
                else
                {
                     types = type;
                }
               

                    return RedirectToAction("AssignRplList", new { Id = Id, datakey = datakey, type = types });
                }

                else
                {
                    return RedirectToAction("Login", "Authen");
                } 
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemDetailList(ModelLayout rdata, decimal Id, int datakey, string docno,int Type )
        {
            try
            {
                //assgin tmp
                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    rdata.SelectdocNo = docno.Trim();
                    rdata.SelectedOption = Type.ToString();
                    

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
            rdata.Id = Id;
            return RedirectToAction("ItemDetailList", "AssignRpl", rdata);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemDetail(ModelLayout rdata, decimal Id, int datakey,string docno)
        {
            try
            {
                //assgin tmp
                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    rdata.SelectdocNo = docno.Trim();

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
            rdata.Id = Id;
            return RedirectToAction("ItemDetail", "AssignRpl", rdata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ScanDetail(ModelLayout rdata, decimal Id, int datakey, string barcode, string docno)
        {
            try
            {
                //assgin tmp
                if (Id != 0)
                {
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    if(rdata.barcode==null && barcode !=null)
                    {
                        rdata.barcode = barcode.Trim();
                    }
                    rdata.SelectdocNo = docno.Trim();

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
            rdata.Id = Id;
            return RedirectToAction("ScanDetail", "AssignRpl", rdata);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ScanQty(ModelLayout rdata, decimal Id, int datakey, string barcode, string docno,string boh)
        {


            try
            {
                //assgin tmp
                if (Id != 0)
                {
                    ClassModel model = new();
                    
                    model = GetDBConnect.GetClassModel(Id);
                    rdata.rp.checkQty = new();
                    rdata.Saveflg = 0;
                    rdata.Message = "";
                    rdata.SelectdocNo = docno.Trim();
                    rdata.barcode= barcode.Trim();
                    

                    //check qty rpl can cnot more
                    if (rdata.Qtyrpl == null)
                    { rdata.Qtyrpl = 0; }

                    if(rdata.barcode!=null && rdata.Qtyrpl!=null)
                    {
                        rdata.rp.checkQty.barcode = barcode.Trim();
                        rdata.rp.checkQty.Qtyrep = rdata.Qtyrpl;
                        rdata.rp.checkQty.wlcode = model.WLCode;
                        rdata.rp.checkQty.docno = docno.Trim();
                        rdata.rp.checkQty.bohflg = boh.Trim();
                        rdata.rp.checkQty = GetcheckQty(rdata.rp.checkQty);


                        if (rdata.rp.checkQty.checkflg == true)
                        {
                            if (UpdateTRNreplenish(model.UserName.Trim(), model.DBKey, model.ConnectString, rdata.rp.checkQty))
                            {
                                rdata.Saveflg = 0;
                                rdata.Message = "บันทึกข้อมูลสำเร็จ";
                            }
                        }
                        else
                        {
                            rdata.Saveflg = (int)rdata.rp.checkQty.flgcode;
                            rdata.Message = rdata.rp.checkQty.message;
                            rdata.menukey = datakey;
                            rdata.Id = Id;
                            return RedirectToAction("ScanDetail", "AssignRpl", rdata);
                        }

                    }
                    else
                    {
                        rdata.Saveflg = 1;
                        rdata.Message = "กรุณาใส่จำนวนให้ครบถ้วน ";
                        rdata.menukey = datakey;
                        rdata.Id = Id;
                        return RedirectToAction("ScanDtail", "AssignRpl", rdata);
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
            rdata.Id = Id;
            rdata.barcode = string.Empty;
            return RedirectToAction("ItemDetail", "AssignRpl", rdata);
        }

        //get function Update

        private bool UpdateTRNreplenish(string Username,string DBname, string DbConnext, 
               CheckQty data)
        {

            GetDBConnect dB = new();
           
            bool flg = true;
            info = new();
            
            try
            {

                string SQL = $" UPDATE [TRNreplenishD] " +
                            $"  set [ItemQtyRep] =isnull([ItemQtyRep],0)+{data.Qtyrep}" +
                            $"      ,[TranStatus] ='{data.status.Trim()}' " +
                            $"      ,[TranFlag] ={data.statuscode} " +
                            $"      ,[ChangeDtime]=GETDATE() " +
                            $"      ,[ChangeUser] ='{Username.Trim()}'" +
                            $"	where  wlid ='{data.wlcode.Trim()}'	" +
                                $"	  and [TranFlag] =2	" +
                                $"	  and [TranStatus] in ('OPN','PAL')"+
                                $"    and [Barcode] ='{data.barcode.Trim()}'"+
                                $"    and [DocNo] ='{data.docno.Trim()}'";

                if (dB.ExecuteTransData(SQL, DbConnext))
                {
                    dB.CloseDB();

                    SQL = $" UPDATE [TRNreplenish] " +
                            $"  set TotalItemRep  = isnull((select count(*) from TRNreplenishD " +
                                                        $"	where  wlid ='{data.wlcode}'	" +
                                                            $"	  and [TranFlag] =3	" +
                                                            $"	  and [TranStatus] ='RPL'" +
                                                            $"    and [DocNo] ='{data.docno.Trim()}'),0)"+
                             $"       ,[TranStatus] = case when" +
                             $"                      isnull((select count(*) from TRNreplenishD " +
                                                        $"	where  wlid ='{data.wlcode}'	" +
                                                            $"	  and [TranFlag] =3	" +
                                                            $"	  and [TranStatus] ='RPL'" +                          
                                                            $"    and [DocNo] ='{data.docno.Trim()}'),0) = TotalItem then 'RPL' else 'OPN' end " +
                              $"       ,[TranCode] = case when" +
                              $"                      isnull((select count(*) from TRNreplenishD " +
                                                        $"	where  wlid ='{data.wlcode}'	" +
                                                            $"	  and [TranFlag] =3	" +
                                                            $"	  and [TranStatus] ='RPL'" +
                                                            $"    and [DocNo] ='{data.docno.Trim()}'),0) = TotalItem then 3 else 2 end " +
                              $"       ,[ChangeDtime]=GETDATE() " +
                              $"       ,[ChangeUser] ='{Username.Trim()}'" +
                              $"	where  wlid ='{data.wlcode}'	" +
                                $"	  and [Trancode] =2	" +
                                $"	  and [TranStatus] ='OPN'" +
                                $"    and [DocNo] ='{data.docno.Trim()}'";


                    if (dB.ExecuteTransData(SQL, DbConnext))
                    {
                        dB.CloseDB();
                    }
                        flg = true;
                }
                else
                {
                    flg = false;
                }

                return flg;
            }
            catch (Exception) { return false; }

        }

        private CheckQty GetcheckQty(CheckQty data)
        {
           
            if (data.barcode != null)
            {
                using (var _dbs = new TNPSTORESYSDBContext())
                {
                    var qtyRep = (from t in _dbs.Trnreplenishes
                                  join t1 in _dbs.TrnreplenishDs
                                  on t.DocNo equals t1.DocNo
                                  where t.DocNo == data.docno.Trim()
                                  && t1.Barcode == data.barcode.Trim()
                                  && t.Wlid == data.wlcode.Trim()
                                  select new { t1.ItemQty ,t.TotalItem,t1.ItemQtyRep}).FirstOrDefault();
                  
                    

                    if (qtyRep != null)
                    {

                        data.Qtyrepold = qtyRep.ItemQtyRep;
                        if(qtyRep.ItemQtyRep==null)
                        {
                            data.Qtyrepold = 0;
                        }
                        data.ItemQty = qtyRep.ItemQty;
                        data.TotalItem=(int)qtyRep.TotalItem;

                        if (data.Qtyrep > qtyRep.ItemQty)
                        {
                            data.flgcode = 1;
                            data.message = "ไม่สามารถทำการจัดสินค้า เกินจำนวนสั่งเติมได้";
                            data.checkflg=false;
                            data.status = "OPN";
                            data.statuscode = 2;

                        }
                        else
                        {
                            if(((data.Qtyrep+ data.Qtyrepold) == qtyRep.ItemQty))
                            {
                                data.flgcode = 0;
                                data.message = "บันทึกสำเร็จ";
                                data.checkflg = true;
                                data.status = "RPL";
                                data.statuscode = 3;
                            }
                            else
                            {
                                if(data.bohflg =="Y")
                                {
                                    data.flgcode = 0;
                                    data.message = "บันทึกสำเร็จ";
                                    data.checkflg = true;
                                    data.status = "RPL";
                                    data.statuscode = 3;
                                }
                                else
                                {
                                    data.flgcode = 0;
                                    data.message = "มีจัดสินค้ายอดคงค้าง";
                                    data.checkflg = true;
                                    data.status = "PAL";
                                    data.statuscode = 2;
                                }
                               

                            }
                        }
                        
                    }
                   
                }
            }
            return data;
        }
    
    }
}
