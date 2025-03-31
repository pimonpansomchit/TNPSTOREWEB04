#nullable disable
using System.Net;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models.Request;
using TNPSTOREWEB.Models;

namespace TNPSTOREWEB.Core
{
    public class GetdataReport
    {

        public ModelLayout RelListHistory(ModelLayout query)
        {


            query.ModelHis.replenish = new();
            query.ModelHis.replenishD = new();


            using (var dts = new TNPSTORESYSDBContext())
            {

                if (query.SelectedOption != null && query.SelectedOption != "0")
                {

                    List<TrnreplenishD> datalist = new();
                   
                        query.ModelHis.replenishD = (from t1 in dts.TrnreplenishDs
                                                     join t in dts.Trnreplenishes
                                                     on t1.DocNo equals t.DocNo
                                                     where t1.Wlid == query.wlcode
                                                     select t1).ToList();

                        datalist = query.ModelHis.replenishD;

                        if (datalist != null)
                        {
                            if (query.barcode != null)
                            {
                                datalist = datalist.Where(t => (t.Barcode == query.barcode || t.DocNo == query.barcode)
                                && t.Wlid == query.wlcode).ToList();
                            }
                            if (query.SelectGroupNo != "0")
                            {
                                datalist = datalist.Where(t => t.GroupId == query.SelectGroupNo.Trim()).ToList();
                            }
                            if (query.SelectStatus != "0")
                            {
                                datalist = datalist.Where(t => t.TranStatus == query.SelectStatus).ToList();
                               
                            }
                           
                                if (query.DateFm != null && query.DateTo != null)
                                {
                                    DateTime datastart = Convert.ToDateTime(query.DateFm + " 00:00:00");
                                    DateTime dataend = Convert.ToDateTime(query.DateTo + " 23:59:59");

                                    datalist = datalist.Where(t => t.CreateDtime >= datastart).Where(t => t.CreateDtime <= dataend).ToList();
                                }
                            
                        }
                      
                    
                    

                    if (datalist != null)
                    {
                        ClassModel model = new();
                        model = GetDBConnect.GetClassModel(query.Id);
                        GetDBConnect dB = new();
                        string mapfm = "";
                        string mapto = "";
                        string statusname = "";
                        query.dataRpt = new();
                        foreach (var i in datalist)
                        {
                           statusname = query.Transtauts.Where(t => t.StatusId.Trim() == i.TranStatus.Trim()).Select(t => t.StatusName).FirstOrDefault();

                            //select location
                            string locationto = $"	SELECT Loczone,Locrack,Loclev" +
                                                  $" FROM LOCprdd{i.Wlid.Trim()} t join " +
                                                  $" TRNreplenishD t1 " +
                                                  $" on t.Barcode = t1.Barcode" +
                                                  $" and t.LocId = t1.LocIDTo" +
                                                  $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                                  $" and t1.wlid='{i.Wlid.Trim()}'";

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
                                                  $" FROM LOCprdd{i.Wlid.Trim().Trim()} t join " +
                                                  $" TRNreplenishD t1 " +
                                                  $" on t.Barcode = t1.Barcode" +
                                                  $" and t.LocId = t1.LocIDFm" +
                                                  $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                                  $" and t1.wlid='{i.Wlid.Trim()}'";

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
                            query.dataRpt.Add(new ReplRpt {
                                    DocNo=i.DocNo,
                                    DocDate=i.CreateDtime.ToString(),
                                    barcode=i.Barcode.Trim(),
                                    productName=i.ItemName.Trim(),
                                    LocIdfmName=mapfm,
                                    LocIdtoName=mapto,
                                    UnitName=i.UnitName.Trim(),
                                    GroupId=i.GroupId.Trim(),
                                    ItemQty=i.ItemQty,
                                    ItemQtyRep=i.ItemQtyRep,
                                    TranStatusname= statusname,
                                    CreateDtime =i.CreateDtime,
                                    CreateUser=i.CreateUser,
                                    ChangeDtime=i.ChangeDtime,
                                    ChangeUser=i.ChangeUser,
                                                
                                    });

                        }

                        if(query.dataRpt!=null)
                        {
                            if (query.barcode == null && query.SelectGroupNo == "0"
                            && query.SelectStatus == "0" && query.DateFm != null && query.DateTo != null)
                            {
                                DateTime datastarts = Convert.ToDateTime(query.DateFm + " 00:00:00");
                                DateTime dataends = Convert.ToDateTime(query.DateTo + " 23:59:59");

                                var orderdata = query.dataRpt.Where(t => t.CreateDtime >= datastarts).Where(t => t.CreateDtime<= dataends).ToList().OrderBy(t => t.DocNo);
                                query.dataRpt = orderdata.ToList();
                            }
                        }
                      

                        
                    }
                    


                }
                
                return query;
            }
        }


        public ModelLayout OutListHistory(ModelLayout query)
        {


            query.ModelHis.outofstock = new();
           

            using (var dts = new TNPSTORESYSDBContext())
            {

                if (query.SelectedOption != null && query.SelectedOption != "0")
                {

                    List<TrnoutofStockD> datalist = new();

                    query.ModelHis.outofstock = (from t1 in dts.TrnoutofStockDs
                                                 where t1.Wlid == query.wlcode
                                                 select t1).ToList();

                    datalist = query.ModelHis.outofstock;

                    if (datalist != null)
                    {
                        if (query.barcode != null)
                        {
                            datalist = datalist.Where(t => t.Barcode.Contains($"{query.barcode}")
                            && t.Wlid == query.wlcode).ToList();
                        }
                        if (query.SelectGroupNo != "0")
                        {
                            datalist = datalist.Where(t => t.GroupId == query.SelectGroupNo.Trim()).ToList();
                        }
                        if (query.SelectStatus != "0")
                        {
                            datalist = datalist.Where(t => t.TranStatus == query.SelectStatus).ToList();

                        }

                        if (query.DateFm != null && query.DateTo != null)
                        {
                            DateTime x = Convert.ToDateTime(query.DateFm);
                            DateTime y = Convert.ToDateTime(query.DateFm);

                            DateOnly datastart = new DateOnly(x.Year, x.Month, x.Day); 
                            DateOnly dataend = new DateOnly(y.Year, y.Month, y.Day);

                            datalist = datalist.Where(t => t.TranDate >= (datastart)).Where(t => t.TranDate <= dataend).ToList();
                        }

                    }




                    if (datalist != null)
                    {
                        ClassModel model = new();
                        model = GetDBConnect.GetClassModel(query.Id);
                        GetDBConnect dB = new();
                        string mapfm = "";
                        string mapto = "";
                     
                        query.dataOutRpt = new();
                        foreach (var i in datalist)
                        {

                            //select location
                            string locationto = $"	SELECT Loczone,Locrack,Loclev" +
                                                  $" FROM LOCprdd{i.Wlid.Trim()} t join " +
                                                  $" TRNreplenishD t1 " +
                                                  $" on t.Barcode = t1.Barcode" +
                                                  $" and t.LocId = t1.LocIDTo" +
                                                  $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                                  $" and t1.wlid='{i.Wlid.Trim()}'";

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
                                                  $" FROM LOCprdd{i.Wlid.Trim().Trim()} t join " +
                                                  $" TRNreplenishD t1 " +
                                                  $" on t.Barcode = t1.Barcode" +
                                                  $" and t.LocId = t1.LocIDFm" +
                                                  $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                                  $" and t1.wlid='{i.Wlid.Trim()}'";

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
                            query.dataOutRpt.Add(new OutofSRpt
                            {
                                DocDate = i.TranDate.ToString("dd/MM/yyyy"),
                                barcode = i.Barcode.Trim(),
                                productName = i.ItemName.Trim(),
                                GroupId = i.GroupId.Trim(),
                                UnitName = i.UnitName.Trim(),
                                LocIdtoName = mapto,
                                ItemQty = i.ItemQty,
                                CreateUser = i.CreateUser,
                                CreateDtime=i.CreateDtime,
                            });

                        }

                        


                    }



                }

                return query;
            }
        }

        public ModelLayout ExpListHistory(ModelLayout query)
        {


            query.ModelHis.expriegood = new();


            using (var dts = new TNPSTORESYSDBContext())
            {

                if (query.SelectedOption != null && query.SelectedOption != "0")
                {

                    List<TrnexpiredofGoodD> datalist = new();

                    query.ModelHis.expriegood = (from t1 in dts.TrnexpiredofGoodDs
                                                 where t1.Wlid == query.wlcode
                                                 select t1).ToList();

                    datalist = query.ModelHis.expriegood;

                    if (datalist != null)
                    {
                        if (query.barcode != null)
                        {
                            datalist = datalist.Where(t => t.Barcode.Contains($"{query.barcode}")
                            && t.Wlid == query.wlcode).ToList();
                        }
                        if (query.SelectGroupNo != "0")
                        {
                            datalist = datalist.Where(t => t.GroupId == query.SelectGroupNo.Trim()).ToList();
                        }
                        if (query.SelectStatus != "0")
                        {
                            datalist = datalist.Where(t => t.TranStatus == query.SelectStatus).ToList();

                        }

                        if (query.DateFm != null && query.DateTo != null)
                        {
                            DateTime x = Convert.ToDateTime(query.DateFm);
                            DateTime y = Convert.ToDateTime(query.DateFm);

                            DateOnly datastart = new DateOnly(x.Year, x.Month, x.Day);
                            DateOnly dataend = new DateOnly(y.Year, y.Month, y.Day);

                            datalist = datalist.Where(t => t.TranDate >= (datastart)).Where(t => t.TranDate <= dataend).ToList();
                        }

                    }




                    if (datalist != null)
                    {
                        ClassModel model = new();
                        model = GetDBConnect.GetClassModel(query.Id);
                        GetDBConnect dB = new();
                        string mapfm = "";
                        string mapto = "";

                        query.dataExpRpt = new();
                        foreach (var i in datalist)
                        {

                            //select location
                            string locationto = $"	SELECT Loczone,Locrack,Loclev" +
                                                  $" FROM LOCprdd{i.Wlid.Trim()} t join " +
                                                  $" TRNreplenishD t1 " +
                                                  $" on t.Barcode = t1.Barcode" +
                                                  $" and t.LocId = t1.LocIDTo" +
                                                  $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                                  $" and t1.wlid='{i.Wlid.Trim()}'";

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
                                                  $" FROM LOCprdd{i.Wlid.Trim().Trim()} t join " +
                                                  $" TRNreplenishD t1 " +
                                                  $" on t.Barcode = t1.Barcode" +
                                                  $" and t.LocId = t1.LocIDFm" +
                                                  $" where t1.Barcode ='{i.Barcode.Trim()}'" +
                                                  $" and t1.wlid='{i.Wlid.Trim()}'";

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
                            query.dataExpRpt.Add(new ExpRpt
                            {
                                DocDate = i.TranDate.ToString("dd/MM/yyyy"),
                                barcode = i.Barcode.Trim(),
                                productName = i.ItemName.Trim(),
                                GroupId = i.GroupId.Trim(),
                                UnitName = i.UnitName.Trim(),
                                LocIdtoName = mapto,
                                ItemQty = i.ItemQty,
                                Expdate=i.ExprieDate.ToString(),
                                LotNo = i.LotNo.Trim(),
                                CreateUser = i.CreateUser,
                                CreateDtime = i.CreateDtime,
                            });

                        }




                    }



                }

                return query;
            }
        }


        public ModelLayout ConfigTypeLoc(ModelLayout query)
        {
            query.loc = new();
            if (query.SelectedOption != null)
            {

               
                using (var dts = new TNPSTORESYSDBContext())
                {
                    query.loc = (from t1 in dts.StLoctypes
                                                 select t1).ToList();
                    if ((query.SelectedOption == "S"|| query.SelectedOption == "R" )&& query.loc !=null)
                    {
                        query.loc = query.loc.Where(t => t.LocCat == query.SelectedOption).ToList();
                    }

                    
                }
                query.loc=query.loc.OrderByDescending(t => t.LocCat).ThenBy(t => t.LocType).ToList();
            }
            return query;
        }

    }
}
