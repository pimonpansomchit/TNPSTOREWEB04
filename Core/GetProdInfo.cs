#nullable disable
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;

namespace TNPSTOREWEB.Core
{
    public class GetProdInfo
    {
        public ModelLayout SearchProduct(ModelLayout query)
        {
            
            using (var dts = new TNPSYSCTLDBContext())
            {

                var prod = (from t in dts.MstProds
                            join t1 in dts.MstBarcodes
                            on t.ProdKey equals t1.ProdKey
                            where t1.ProdBarcode == query.barcode
                            select t).FirstOrDefault();

                if (prod != null)
                {
                    query.pi.MstProd = prod;

                    var barcode = (from t in dts.MstProds
                                   join t1 in dts.MstBarcodes
                                   on t.ProdKey equals t1.ProdKey
                                   where t1.ProdBarcode == query.barcode
                                   select t1).FirstOrDefault();
                    query.pi.MstBarcode = barcode;

                    //query find location
                    var groupid = (from t in dts.MstProdgroups
                                   join t1 in dts.MstBarcodes
                                   on t.ProdKey equals t1.ProdKey
                                   where t1.ProdBarcode == query.barcode
                                   select t.GroupId).FirstOrDefault();
                    if(groupid==null)
                    {
                        groupid = "";
                    }
                    query.groupId = groupid;

                }


            }
           
                if (query.barcode != null )
                {
                    ClassModel model = new();
                    model = GetDBConnect.GetClassModel(query.Id);
                    GetDBConnect dB = new();
                    try
                    {
                    //get from-to location
                        string SQL = $"	SELECT (isnull((select min(locID) from LOCprdd{model.WLCode.Trim()} where barcode = '{query.barcode.Trim()}' and loccat = 'S' and locType=1), '0-00000'))," +
                                     $" (isnull((select min(locID) from LOCprdd{model.WLCode.Trim()} where barcode = '{query.barcode.Trim()}' and loccat='R'),'0-00000'))"+
                                     $"	FROM LOCprdd{model.WLCode.Trim()}" +
                                     $"  where Barcode ='{query.barcode.Trim()}'" +
                                     $"  and wlid='{model.WLCode.Trim()}'";
                                   
                                   
                        if (dB.ExecuteReadData(SQL, model.ConnectString.Trim()))
                        {
                            if (dB.myReader.HasRows)
                            {
                                while (dB.myReader.Read())
                                {
                                    query.Locidto = dB.myReader[0].ToString().Trim();
                                    query.Locidfm = dB.myReader[1].ToString().Trim();
                                
                                }

                                if(query.Locidfm == null )
                                { query.Locidfm="0-00000"; }
                                if (query.Locidto == null)
                                { query.Locidto = "0-00000"; }

                        }
                        }

                        dB.CloseDB();
                    Locprdd idata = new(); 
                    if(query.selectLoctype != 0)
                    {

           
                    //get locinv 
                    SQL = $" SELECT"+
                          $" [locID]"+
                          $",[groupId]"+
                          $",[prodKey]"+
                          $",[prodID]"+
                          $",[prodName]"+
                          $",[barcodeKey]"+
                          $",[barcode]"+
                          $",[unit]"+
                          $",[unittype]"+
                          $",[unitname]"+
                          $",[locCode]"+
                          $",[loccat]"+
                          $",[locType]"+
                          $",[locTypeName]"+
                          $",[loczone]"+
                          $",[locrack]"+
                          $",[loclev]"+
                          $",[locpos]"+
                          $",[stackPos]"+
                          $",[stackDep]"+
                          $",[stackHgt]"+
                          $",[casecap]"+
                          $",[minsel]"+
                          $",[rplnamt]"+
                          $" ,isnull([bohQty],0)"+
                          $",[locstatus]"+
                          $",[create_user]"+
                          $",[create_dtime]"+
                          $",[change_user]"+
                          $",[change_dtime]"+
                          $",[logid]"+
                                     $"	FROM LOCprdd{model.WLCode.Trim()}" +
                                     $"  where Barcode ='{query.barcode.Trim()}'" +
                                     $"  and wlid='{model.WLCode.Trim()}'" +
                                     $"  and Loccat='{query.Loccat.Trim()}'" +
                                     $"  and LocType={query.selectLoctype}";

                    if (dB.ExecuteReadData(SQL, model.ConnectString.Trim()))
                    {
                        if (dB.myReader.HasRows)
                        {
                            while (dB.myReader.Read())
                            {
                                idata = new()
                                {
                                    Wlid = model.WLCode,
                                    LocId = dB.myReader[0].ToString().Trim(),
                                    GroupId = dB.myReader[1].ToString().Trim(),
                                    ProdKey = (decimal)dB.myReader[2],
                                    ProdId = dB.myReader[3].ToString().Trim(),
                                    ProdName = dB.myReader[4].ToString().Trim(),
                                    BarcodeKey = (decimal)dB.myReader[5],
                                    Barcode = dB.myReader[6].ToString().Trim(),
                                    Unit = (int)dB.myReader[7],
                                    Unittype = dB.myReader[8].ToString().Trim(),
                                    Unitname = dB.myReader[9].ToString().Trim(),
                                    LocCode = dB.myReader[10].ToString().Trim(),
                                    Loccat = dB.myReader[11].ToString().Trim(),
                                    LocType = (int)dB.myReader[12],
                                    LocTypeName = dB.myReader[13].ToString().Trim(),
                                    Loczone = dB.myReader[14].ToString().Trim(),
                                    Locrack = dB.myReader[15].ToString().Trim(),
                                    Loclev = dB.myReader[16].ToString().Trim(),
                                    Locpos = dB.myReader[17].ToString().Trim(),
                                    StackPos = (int)dB.myReader[18],
                                    StackDep = (int)dB.myReader[19],
                                    StackHgt = (int)dB.myReader[20],
                                    Casecap = (int)dB.myReader[21],
                                    Minsel = (int)dB.myReader[22],
                                    Rplnamt = (int)dB.myReader[23],
                                    BohQty = (int)dB.myReader[24],
                                    Locstatus = (int)dB.myReader[25],
                                    CreateUser = model.UserName,
                                    CreateDtime = DateTime.Now,
                                    ChangeUser = model.UserName,
                                    ChangeDtime = DateTime.Now,
                                    Logid = (decimal)dB.myReader[30],

                                };
                                break;
                            }

                            if(idata != null)
                            {
                                query.pi.iprodinv=idata;
                            }
                        }
                    }
                }
                }
                catch (Exception) { }
            }
           
            return query;
        }
        public LocdataList SearchProductLoc(LocdataList query)
        {

            using (var dts = new TNPSYSCTLDBContext())
            {

                var prod = (from t in dts.MstProds
                            join t1 in dts.MstBarcodes
                            on t.ProdKey equals t1.ProdKey
                            where t1.ProdBarcode == query.barcode
                            select t).FirstOrDefault();

                if (prod != null)
                {
                    query.MstProd = prod;

                    var barcode = (from t in dts.MstProds
                                   join t1 in dts.MstBarcodes
                                   on t.ProdKey equals t1.ProdKey
                                   where t1.ProdBarcode == query.barcode
                                   select t1).FirstOrDefault();
                    query.MstBarcode = barcode;

                    //query find location
                    var groupid = (from t in dts.MstProdgroups
                                   join t1 in dts.MstBarcodes
                                   on t.ProdKey equals t1.ProdKey
                                   where t1.ProdBarcode == query.barcode
                                   select t.GroupId).FirstOrDefault();
                    if (groupid == null)
                    {
                        groupid = "";
                    }
                    query.groupid = groupid;

                }


            }

            if (query.barcode != null)
            {
                ClassModel model = new();
                model = GetDBConnect.GetClassModel(query.Id);
                GetDBConnect dB = new();
                query.locinfo = new List<LocList>();
                string map = string.Empty;
                try
                    {
                    string SQL = $"	SELECT Logid,LocId,Loccat,LocType,LocTypeName,Loczone," +
                                 $" Locrack,Loclev,StackPos,StackDep,StackHgt  " +
                                 $"	FROM LOCprdd{model.WLCode.Trim()}" +
                                 $"  where Barcode ='{query.barcode.Trim()}'" +
                                 $"  and wlid='{model.WLCode.Trim()}'" +
                                 $" order by Loccat,LocType,LocId";

                        if (dB.ExecuteReadData(SQL, model.ConnectString.Trim()))
                        {
                            if (dB.myReader.HasRows)
                            {
                                while (dB.myReader.Read())
                                {
                                map = "โซน:" + dB.myReader[5].ToString().Trim() + " เบย์:" + dB.myReader[6].ToString().Trim() + " ชั้น:" + dB.myReader[7].ToString().Trim() +
                                        " ขา:" + dB.myReader[8].ToString().Trim() + " ลึก:" + dB.myReader[9].ToString().Trim() + " สูง:" + dB.myReader[10].ToString().Trim();
                                query.locinfo.Add(new LocList { Id = (decimal)dB.myReader[0], LocId = dB.myReader[1].ToString().Trim(), Loccat = dB.myReader[2].ToString().Trim(), LocType = (int)dB.myReader[3]
                                    , LocTypeName = dB.myReader[4].ToString().Trim(), Locdetail = map,barcode=query.barcode });


                            }

                        }
                            
                        }

                        dB.CloseDB();


                    }
                    catch (Exception) { }
               
                }
            
            return query;
        }
        public List<StLoctype> GetLoctype(string loccat)
        {
            List<StLoctype> stLoctypes = new List<StLoctype>();
            using (var dt = new TNPSTORESYSDBContext())
            {
                stLoctypes=dt.StLoctypes.Where(t=>t.LocCat==loccat.Trim()).ToList();
            }
            return stLoctypes;
        }
        public ModelLayout SearchRelList(ModelLayout query)
        {
           
            query.rp.assignInfos = new();
            query.rp.replenish = new();
            using (var dts = new TNPSTORESYSDBContext())
            {
                if (query.SelectedOption != null && query.SelectedOption!="0")
                {
                    query.rp.replenish = (from t in dts.Trnreplenishes
                                      join t1 in dts.TrnreplenishDs
                                      on t.DocNo equals t1.DocNo
                                      where t.TranStatus == "OPN"
                                      && t.Wlid == query.wlcode
                                      && t1.GroupId == query.SelectedOption.Trim()
                                      select t).OrderBy(t => t.DocNo).ToList();


                    var replH = from item in query.rp.replenish
                                group item by new { item.DocNo, item.TranDate, item.TotalItem } into groupResult
                                select new
                                {
                                    DocNo = groupResult.Key.DocNo,
                                    TranDate = groupResult.Key.TranDate,
                                    TotalItem = groupResult.Key.TotalItem,
                                    trnreplenishes = groupResult.Select(g => new { g.DocNo, g.TranDate, g.TotalItem }).ToList() // Collecting ItemNames in a list
                                };
                    foreach (var t in replH)
                    {
                        if (t.DocNo != null)
                        {
                            query.rp.assignInfos.Add(new AssignInfo { DocNo = t.DocNo, TranDate = t.TranDate.ToString("dd/MM/yyyy"), TotalItem = (int)t.TotalItem });
                        }

                    }

                }

                else
                {
                    query.rp.replenish = (from t in dts.Trnreplenishes
                                      where t.TranStatus == "OPN"
                                      && t.Wlid == query.wlcode
                                      select t).OrderBy(p => p.DocNo).ToList();
                  
                    foreach (var t in query.rp.replenish)
                    {
                        if (t.DocNo != null)
                        {
                            query.rp.assignInfos.Add(new AssignInfo { DocNo = t.DocNo, TranDate = t.TranDate.ToString("dd/MM/yyyy"), TotalItem = (int)t.TotalItem });
                        }

                    }
                }

                return query;
            }
        }
        public ModelLayout SearchRelListItem(ModelLayout query)
        {

            query.rp.assignInfos = new();
            query.rp.replenish = new();
            query.rp.replenishD = new();
            using (var dts = new TNPSTORESYSDBContext())
            {
                if (query.SelectdocNo != null)
                {
                    if (query.barcode == null)
                    {
                        if (query.SelectedOption != null && query.SelectedOption != "0")
                        {
                            query.rp.replenishD = (from t in dts.Trnreplenishes
                                                join t1 in dts.TrnreplenishDs
                                                on t.DocNo equals t1.DocNo
                                                where (t1.TranStatus == "OPN" || t1.TranStatus == "PAL")
                                                && t.Wlid == query.wlcode
                                                && t1.GroupId == query.SelectedOption.Trim()
                                                && t.DocNo == query.SelectdocNo.Trim()
                                                select t1).OrderBy(t => t.LocIdfm).ToList();
                        }
                        else
                        {
                            query.rp.replenishD = (from t in dts.Trnreplenishes
                                                join t1 in dts.TrnreplenishDs
                                                on t.DocNo equals t1.DocNo
                                                where (t1.TranStatus == "OPN" || t1.TranStatus == "PAL")
                                                && t.Wlid == query.wlcode
                                                && t.DocNo == query.SelectdocNo.Trim()
                                                select t1).OrderBy(t => t.LocIdfm).ToList();
                        }

                    }
                    else
                    {
                        query.rp.replenishD = (from t in dts.Trnreplenishes
                                            join t1 in dts.TrnreplenishDs
                                            on t.DocNo equals t1.DocNo
                                            where (t1.TranStatus == "OPN" || t1.TranStatus == "PAL")
                                            && t.Wlid == query.wlcode
                                            && t1.Barcode == query.barcode.Trim()
                                            && t.DocNo == query.SelectdocNo.Trim()
                                            select t1).OrderBy(t => t.LocIdfm).ToList();

                    }

                }

                if (query.rp.replenishD != null)
                {
                    ClassModel model = new();
                    model = GetDBConnect.GetClassModel(query.Id);
                    GetDBConnect dB = new();
                    string mapfm = "";
                    string mapto = "";
                    query.rp.locprdds = new();
                    foreach (var i in query.rp.replenishD)
                    {
                        //select location
                        string locationto = $"	SELECT Loczone,Locrack,Loclev" +
                                              $" FROM LOCprdd{model.WLCode.Trim()} t join " +
                                              $" TRNreplenishD t1 " +
                                              $" on t.Barcode = t1.Barcode" +
                                              $" and t.LocId = t1.LocIDTo" +
                                              $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                              $" and t1.wlid='{model.WLCode.Trim()}'";

                        if (dB.ExecuteReadData(locationto, model.ConnectString.Trim()))
                        {
                            if (dB.myReader.HasRows)
                            {
                                while (dB.myReader.Read())
                                {
                                    mapto = "โซน:" + dB.myReader[0].ToString().Trim() + " เบย์:" + dB.myReader[1].ToString().Trim() + " ชั้น:" + dB.myReader[2].ToString().Trim();
                                    break;
                                }
                            }
                        }

                        string locationfm = $"	SELECT Loczone,Locrack,Loclev" +
                                              $" FROM LOCprdd{model.WLCode.Trim()} t join " +
                                              $" TRNreplenishD t1 " +
                                              $" on t.Barcode = t1.Barcode" +
                                              $" and t.LocId = t1.LocIDFm" +
                                              $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                              $" and t1.wlid='{model.WLCode.Trim()}'";

                        if (dB.ExecuteReadData(locationfm, model.ConnectString.Trim()))
                        {
                            if (dB.myReader.HasRows)
                            {
                                while (dB.myReader.Read())
                                {
                                    mapfm = "โซน:" + dB.myReader[0].ToString().Trim() + " เบย์:" + dB.myReader[1].ToString().Trim() + " ชั้น:" + dB.myReader[2].ToString().Trim();
                                    break;
                                }
                            }
                        }


                        UnitBarcode unitBarcode = GetUnitBarcode(i.ItemCode, model.MainDB, model.ConnectString.Trim());
                        //แตกเป็น unit ชิ้น ตามหน่วยการ input

                        int unitqty = (int)i.Unit * (int)i.ItemQty;
                        string convert = string.Empty;
                        int balance = unitqty;
                        int unitC = 0;
                        int unitI = 0;
                        int unitU = 0;
                        if (unitBarcode != null)
                        {
                            //check unittype of input
                            if (i.UnitType == "U")
                            {
                                if (unitBarcode.UnitC <= unitqty && unitBarcode.UnitC != 0)
                                {
                                    double C = unitqty / (unitBarcode.UnitC);
                                    var RplC = Math.Round(C, 0, MidpointRounding.AwayFromZero);
                                    balance = unitqty - ((int)RplC * (unitBarcode.UnitC));
                                    convert = "หีบ:" + RplC;
                                    unitC = (int)RplC;

                                }

                                if (unitBarcode.UnitI <= balance && unitBarcode.UnitI != 0)
                                {

                                    double I = balance / (unitBarcode.UnitI);
                                    var RplI = Math.Round(I, 0, MidpointRounding.AwayFromZero);
                                    balance = balance - ((int)RplI * (unitBarcode.UnitI));
                                    convert += "แพ็ค:" + RplI;
                                    unitI = (int)RplI;

                                    if (balance > 0)
                                    {
                                        var RplU = (int)balance / i.Unit;
                                        convert += "ชิ้น:" + RplU;
                                        unitU = (int)RplU;
                                    }
                                }


                            }
                            if (i.UnitType == "I")
                            {
                                if (unitBarcode.UnitC <= unitqty)
                                {
                                    double C = unitqty / (unitBarcode.UnitC);
                                    var RplC = Math.Round(C, 0, MidpointRounding.AwayFromZero);
                                    balance = unitqty - ((int)RplC * (unitBarcode.UnitC));
                                    convert = "หีบ:" + RplC;
                                    unitC = (int)RplC;
                                    if (balance > 0)
                                    {
                                        var RplI = (int)balance / i.Unit;
                                        convert += " แพ็ค:" + RplI;
                                        unitI = (int)RplI;
                                    }

                                }

                            }
                        }
                        query.rp.locprdds.Add(new RelLocList { barcode = i.Barcode.Trim(), LocIdtoName = mapto.Trim(), LocIdfmName = mapfm.Trim(), UnitConvert = convert.Trim(), QtyC = unitC, QtyI = unitI, QtyU = unitU, DocNo = i.DocNo });

                    }


                }
            }
            return query;
        }
        public List<Mstgroup> GetGroupList()
        {
            List<Mstgroup> grouplist = new List<Mstgroup>();
            using (var dt = new TNPSYSCTLDBContext())
            {
                var grouplists = dt.MstProdgroups.ToList();

                var group = from item in grouplists
                                 group item by new { item.GroupId, item.GroupName } into groupResult
                                 select new
                                 {
                                     GroupId = groupResult.Key.GroupId,
                                     GroupName = groupResult.Key.GroupName,
                                     grouplists = groupResult.Select(g =>new { g.GroupId, g.GroupName }).OrderBy(p=>p.GroupId).ToList() // Collecting ItemNames in a list
                                 };
                foreach (var t in group)
                {
                    if(t.GroupId !=null && t.GroupName != null)
                    {
                        var Groupnames = t.GroupId + ":" + t.GroupName;
                        grouplist.Add(new Mstgroup { GroupId = t.GroupId, GroupName = Groupnames });
                    }
                    
                }

            }
            var order = grouplist.ToList().OrderBy(t => t.GroupId);
            grouplist = order.ToList();
            return grouplist;
        }
        public UnitBarcode GetUnitBarcode(string prodid, string DBname, string DbConnext)
        {
            GetDBConnect dB = new();
            UnitBarcode unitBarcode = new();

            try
            {
                string SQL = $"use {DBname}" +
                    $" SELECT distinct T1.prod_id,T2.group_id," +
                    $" (select top 1 (T.prod_bar_unit) from dbo.mst_barcode T where T.prod_id =T1.prod_id and T.prod_bar_type ='U' order by T.barcode_key desc) as unitU," +
                    $" (select top 1 (T.prod_bar_unit) from dbo.mst_barcode T where T.prod_id =T1.prod_id and T.prod_bar_type ='I' order by T.barcode_key desc) as unitI," +
                    $" (select top 1 (T.prod_bar_unit) from dbo.mst_barcode T where T.prod_id =T1.prod_id and T.prod_bar_type ='C' order by T.barcode_key desc) as unitC" +
                    $" from dbo.mst_barcode T1 left join mst_prodgroup T2 " +
                    $" on T1.prod_id = T2.prod_id" +
                    $" and T1.prod_key = T2.prod_key "+
                    $" where T1.prod_id = '{prodid}'";

                if (dB.ExecuteReadData(SQL, DbConnext))
                {
                    if (dB.myReader.HasRows)
                    {
                        while (dB.myReader.Read())
                        {
                            unitBarcode.prodid = dB.myReader[0].ToString();
                            unitBarcode.groupid = dB.myReader[1].ToString();
                            unitBarcode.UnitU = Convert.ToInt32(dB.myReader[2]);
                            unitBarcode.UnitI = Convert.ToInt32(dB.myReader[3]);
                            unitBarcode.UnitC = Convert.ToInt32(dB.myReader[4]);
                            break;
                        }
                    }
                    else
                    {
                        unitBarcode.groupid = string.Empty;
                        unitBarcode.UnitU = 0;
                        unitBarcode.UnitI = 0;
                        unitBarcode.UnitC = 0;
                    }
                }
                dB.CloseDB();
            }
            catch (Exception) { }
            return unitBarcode;
        }
        public ModelLayout RelListHistory(ModelLayout query)
        {

            query.rp.assignInfos = new();
            query.rp.replenish = new();
            query.rp.replenishD = new();
            using (var dts = new TNPSTORESYSDBContext())
            {
               
                    
             if (query.SelectedOption != null && query.SelectedOption != "0")
                   {

                    if (query.SelectedOption == "1")
                    {
                        query.rp.replenishD = (from t1 in dts.TrnreplenishDs
                                            where t1.TranStatus == "RPL"
                                            && t1.Wlid == query.wlcode
                                            select t1).ToList();
                    }
                    else if (query.SelectedOption == "2") {

                        if (query.SelectdocNo == null)
                        {
                            query.rp.replenish = (from t1 in dts.Trnreplenishes
                                               where t1.TranStatus == "RPL"
                                               && t1.Wlid == query.wlcode
                                               select t1).ToList();
                        }
                        else
                        {
                            query.rp.replenishD = (from t1 in dts.TrnreplenishDs
                                                where t1.TranStatus == "RPL"
                                               && t1.Wlid == query.wlcode
                                               && t1.DocNo ==query.SelectdocNo
                                               select t1).ToList();
                        }
                       
                    }
                    else {
                        if(query.barcode !=null)
                        {
                            query.rp.replenishD = (from t1 in dts.TrnreplenishDs
                                                where t1.TranStatus == "RPL"
                                                && t1.Wlid == query.wlcode.Trim()
                                                && t1.DocNo == query.SelectdocNo.Trim()
                                                && t1.Barcode == query.barcode.Trim()
                                                select t1).ToList();
                        }
                        else
                        {
                            query.rp.replenishD = (from t1 in dts.TrnreplenishDs
                                                where t1.TranStatus == "RPL"
                                                && t1.Wlid == query.wlcode.Trim()
                                                select t1).ToList();
                        }
                       
                        }    
                     }
                     

                if (query.rp.replenishD != null)
                {
                    ClassModel model = new();
                    model = GetDBConnect.GetClassModel(query.Id);
                    GetDBConnect dB = new();
                    string mapfm = "";
                    string mapto = "";
                    query.rp.locprdds = new();
                    foreach (var i in query.rp.replenishD)
                    {
                        //select location
                        string locationto = $"	SELECT Loczone,Locrack,Loclev" +
                                              $" FROM LOCprdd{model.WLCode.Trim()} t join " +
                                              $" TRNreplenishD t1 " +
                                              $" on t.Barcode = t1.Barcode" +
                                              $" and t.LocId = t1.LocIDTo" +
                                              $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                              $" and t1.wlid='{model.WLCode.Trim()}'";

                        if (dB.ExecuteReadData(locationto, model.ConnectString.Trim()))
                        {
                            if (dB.myReader.HasRows)
                            {
                                while (dB.myReader.Read())
                                {
                                    mapto = "โซน:" + dB.myReader[0].ToString().Trim() + " เบย์:" + dB.myReader[1].ToString().Trim() + " ชั้น:" + dB.myReader[2].ToString().Trim();
                                    break;
                                }
                            }
                        }

                        string locationfm = $"	SELECT Loczone,Locrack,Loclev" +
                                              $" FROM LOCprdd{model.WLCode.Trim()} t join " +
                                              $" TRNreplenishD t1 " +
                                              $" on t.Barcode = t1.Barcode" +
                                              $" and t.LocId = t1.LocIDFm" +
                                              $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                              $" and t1.wlid='{model.WLCode.Trim()}'";

                        if (dB.ExecuteReadData(locationfm, model.ConnectString.Trim()))
                        {
                            if (dB.myReader.HasRows)
                            {
                                while (dB.myReader.Read())
                                {
                                    mapfm = "โซน:" + dB.myReader[0].ToString().Trim() + " เบย์:" + dB.myReader[1].ToString().Trim() + " ชั้น:" + dB.myReader[2].ToString().Trim();
                                    break;
                                }
                            }
                        }


                        
                        query.rp.locprdds.Add(new RelLocList { barcode = i.Barcode.Trim(), LocIdtoName = mapto.Trim(), LocIdfmName = mapfm.Trim(), DocNo=i.DocNo });

                    }


                }
            }
            return query;
        }
        public List<DataStatus> GetrepStatus()
        {
            List<DataStatus> datalists = new();
            using (var dt = new TNPSTORESYSDBContext())
            {
                var datalist = dt.StStatuses.Where(t => t.StatusCode == "REP").OrderBy(t => t.Statusno).Select(t => new { t.StatusId, t.StatusName }).ToList();
                foreach (var i in datalist)
                {
                    datalists.Add(new DataStatus { StatusId = i.StatusId, StatusName = i.StatusName });
                }
            }
            
           
            return datalists;
        }

        public Messageinfo TrackDuplication(string type,string barcode,int recdno)
        {
            Messageinfo msg = new();

            using (var dts = new TNPSTORESYSDBContext())
            {
                DateTime dt = DateTime.Now;
                DateOnly d = new( dt.Year, dt.Month, dt.Day );

                if (type == "R")
                {
                    var datalist = dts.TmpreplenishDs.Where(t => t.TranStatus == "CRE" && t.TranDate==d && t.Barcode==barcode.Trim() && t.RecdNo == (recdno-1)).ToList();
                    if(datalist !=null && datalist.Count()>0)
                    {
                        msg.code = 1;
                        msg.messsage = "บาร์โค้ดนี้ มีการสั่งเติมไปแล้วรายการก่อนหน้า !!!";
                    }
                    else
                    {
                        msg.code = 0;
                        msg.messsage = "";
                    }
                }
                else
                {
                    if (type == "O")
                    {
                        var datalist = dts.TrnoutofStockDs.Where(t => t.TranStatus == "CRE" && t.TranDate == d && t.Barcode == barcode.Trim() && t.RecdNo == (recdno-1)).ToList();
                        if (datalist != null && datalist.Count() > 0)
                        {
                            msg.code = 1;
                            msg.messsage = "บาร์โค้ดนี้ มีการสั่งเติมไปแล้วรายการก่อนหน้า !!!";
                        }
                        else
                        {
                            msg.code = 0;
                            msg.messsage = "";
                        }
                    }
                    else
                    {
                        if (type == "E")
                        {
                            var datalist = dts.TrnexpiredofGoodDs.Where(t => t.TranStatus == "CRE" && t.TranDate == d && t.Barcode == barcode.Trim() && t.RecdNo == (recdno-1)).ToList();
                            if (datalist != null && datalist.Count() > 0)
                            {
                                msg.code = 1;
                                msg.messsage = "บาร์โค้ดนี้ มีการสั่งเติมไปแล้วรายการก่อนหน้า !!!";
                            }
                            else
                            {
                                msg.code = 0;
                                msg.messsage = "";
                            }
                        }
                    }
                }

               
            }


            return msg;
        }

        public int MaxTrackcount(string type)
        {
            int datalist = 0;

            using (var dts = new TNPSTORESYSDBContext())
            {
                DateTime dst = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                DateTime dte = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
                DateOnly d = new(dst.Year, dst.Month, dst.Day);

                if (type == "R")
                {
                    var mx = dts.TrnreplenishDs.Where(t => t.CreateDtime >= dst && t.CreateDtime <= dte).ToList();
                    var mxt= dts.TmpreplenishDs.Where(t => t.TranStatus == "CRE" && t.TranDate == d).ToList();

                    if (mx.Count() !=0 && mxt.Count() ==0)
                    {
                        datalist = dts.TrnreplenishDs.Where(t => t.CreateDtime >= dst && t.CreateDtime <= dte).OrderByDescending(p => p.RecdNo).Select(t => t.RecdNo).First();
                    }
                    else
                    {
                        if (mx.Count() != 0 && mxt.Count() != 0)
                            {
                             datalist = dts.TmpreplenishDs.Where(t => t.TranStatus == "CRE" && t.TranDate == d).OrderByDescending(p => p.RecdNo).Select(t => t.RecdNo).First();

                        }
                    }
                    

                }
                else
                {
                    if (type == "O")
                    {
                        var mx = dts.TrnoutofStockDs.Where(t => t.TranStatus == "CRE" && t.TranDate == d).ToList();

                        if (mx.Count() > 0)
                        {
                            datalist=dts.TrnoutofStockDs.Where(t => t.TranStatus == "CRE" && t.TranDate == d).OrderByDescending(t => t.RecdNo).Select(t => t.RecdNo).First();

                        }

                       
                    }
                    else
                    {
                        if (type == "E")
                        {
                            var mx = dts.TrnexpiredofGoodDs.Where(t => t.TranStatus == "CRE" && t.TranDate == d).ToList();
                            if (mx.Count() > 0)
                            {
                                datalist = dts.TrnexpiredofGoodDs.Where(t => t.TranStatus == "CRE" && t.TranDate == d).OrderByDescending(t => t.RecdNo).Select(t => t.RecdNo).First();
                            }
                        }
                    }
                }


            }
           

            return datalist;
        }
    } }
