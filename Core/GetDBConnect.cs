
#nullable disable
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using System.Configuration;
using System.Data;
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Models;
using TNPSTOREWEB.Models.Request;
namespace TNPSTOREWEB.Core
{
    public class GetDBConnect
    {
        public string ServerName;
        public string strCmd;
        public SqlDataReader myReader;
        public SqlDataAdapter myAdapter;
        public SqlTransaction myTransaction;
        public string sqlStr;
        public SqlConnection myConn;
        public SqlCommand myCmd;
        public bool ConnSta = false;
        public string DbName;
        public string ErrStrg;

        public static ClassModel GetClassModel(decimal? Logid)
        {
            TNPSTORESYSDBContext db = new();
            TNPSYSCTLDBContext dbs = new();
            ClassModel Model = new();
            
              

                try
                {
                    SysDatalog data = new();
                    var sysUserLog = db.SysDatalogs.Where(t =>
                    t.LogId == Logid
                    ).FirstOrDefault();

                    if (sysUserLog != null)
                    {
                        var flgService = (from t in db.StUserlogins join 
                                          t1 in db.StWlprices
                                          on t.WlCode equals t1.WlCode
                                          join t2 in db.Ctlconfigs
                                          on t.DbName equals t2.StoreDb
                                          where t.UserName == sysUserLog.UserLogin
                                          select new { t.UserName, t.ClassId, t.WlCode, t.DbName,t1.DbNameString,t2.MainMstDb,t2.Dcid,t2.Whid,t.WlKey }).FirstOrDefault();




                        if (flgService != null)
                        {
                            Model.UserName = flgService.UserName;
                            Model.ClassId = flgService.ClassId;
                            Model.DBKey = flgService.DbName;
                            Model.WLCode = flgService.WlCode;
                            Model.ConnectString = flgService.DbNameString;
                            Model.MainDB= flgService.MainMstDb;
                            Model.DCcode = flgService.Dcid;
                            Model.WHcode=flgService.Whid;
                            Model.WLName = (from t in dbs.MstWls 
                                            where t.WlId.Trim() == Model.WLCode.Trim() 
                                            select  t.WlName).FirstOrDefault().ToString();
                            if (Logid != null)
                            {
                                Model.logid = (decimal)Logid;
                            }
                            else { Model.logid = 0; }
                            Model.wlKey = flgService.WlKey;
                        return Model;
                    }
                        else
                        { return null; }
                }
                else
                { return null; }


            }
                catch (Exception)
                {

                    return null;
                }

            
        }

        public SqlCommand GetCommand(string sqlStr, string DbConnect)
        {
            SqlCommand TextCmd;

            myConn = new SqlConnection(DbConnect);
            myConn.Open();
            TextCmd = new SqlCommand(sqlStr, myConn);

            return TextCmd;
        }

        public bool ExecuteReadData(string sqlState, string DbConnect)
        {
            bool chk = false;



            myConn = new SqlConnection(DbConnect);

            if (myConn.State == ConnectionState.Open)
            {
                if (!IsNothing(myReader))
                    myReader.Close();
            }

            myConn.Open();

            try
            {

                SqlCommand myCmd = new SqlCommand(sqlState, myConn);
                myCmd.CommandTimeout = 0;
                myReader = myCmd.ExecuteReader();
                chk = true;
            }
            catch (SqlException ex)
            {
                ErrStrg = ex.Message;
            }


            catch (Exception ex)
            {
                ErrStrg = ex.Message;
            }

            return chk;
        }

        public bool ExecuteTransData(string sqlState, string DbConnect)
        {
            bool chk = false;

            myConn = new SqlConnection(DbConnect);
            myConn.Open();
            try
            {
                SqlCommand mycmd = new SqlCommand(sqlState, myConn);
                mycmd.CommandTimeout = 0;
                mycmd.ExecuteNonQuery();
                chk = true;
            }
            catch (SqlException)
            {
                chk = false;
            }

            catch (Exception)
            {
                chk = false;
            }

            finally
            {
            }

            return chk;
        }

        public void CloseDB()
        {
            try
            {
                if (myConn == null)
                    return;
                if (myConn.State == ConnectionState.Open)
                {
                    if (!IsNothing(myReader))
                        myReader.Close();

                    myConn.Close();

                    ConnSta = false;
                }
            }
            catch (Exception ex)
            {
                ErrStrg = ex.Message;
            }
        }
        private bool IsNothing(SqlDataReader myReader)
        {
            throw new NotImplementedException();
        }

    }


}

