
#nullable disable
using Microsoft.Data.SqlClient;
using System.Data;
using TNPWMSWEB.Context;
using TNPWMSWEB.Model.Request;
using TNPWMSWEB.Models;
namespace TNPWMSWEB.Core
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

