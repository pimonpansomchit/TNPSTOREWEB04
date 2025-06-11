#nullable disable
using TNPWMSWEB.Context;
using TNPWMSWEB.Model;
using TNPWMSWEB.Model.Request;
using TNPWMSWEB.Models;

namespace TNPWMSWEB.Core
{
    public class GetProdInfo
    {
        public ProdInfo SearchProduct(string serachbarcode)
        {
            ProdInfo prodInfo = new ProdInfo();
            using (var dts = new TNPSYSCTLDBContext())
            {

                var prod = (from t in dts.MstProds
                            join t1 in dts.MstBarcodes
                            on t.ProdKey equals t1.ProdKey
                            where t1.ProdBarcode == serachbarcode
                            select t).FirstOrDefault();

                if (prod != null)
                {
                    prodInfo.MstProd = prod;

                    var barcode = (from t in dts.MstProds
                                   join t1 in dts.MstBarcodes
                                   on t.ProdKey equals t1.ProdKey
                                   where t1.ProdBarcode == serachbarcode
                                   select t1).FirstOrDefault();
                    prodInfo.MstBarcode = barcode;

                    //query find location
                    var groupid = (from t in dts.MstProdgroups
                                   join t1 in dts.MstBarcodes
                                   on t.ProdKey equals t1.ProdKey
                                   where t1.ProdBarcode == serachbarcode
                                   select t.GroupId).FirstOrDefault();
                    if (groupid == null)
                    {
                        groupid = "";
                    }
                    prodInfo.GropId = groupid;

                }


            }
            return prodInfo;
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
                    $" and T1.prod_key = T2.prod_key " +
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
                else
                {
                    unitBarcode.groupid = string.Empty;
                    unitBarcode.UnitU = 0;
                    unitBarcode.UnitI = 0;
                    unitBarcode.UnitC = 0;
                }
                dB.CloseDB();
            }
            catch (Exception) { }
            return unitBarcode;
        }
       



    }
}
