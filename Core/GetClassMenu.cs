#nullable disable
using TNPWMSWEB.Context;
using TNPWMSWEB.Model;
using TNPWMSWEB.Model.Request;
using TNPWMSWEB.Models;

namespace TNPWMSWEB.Core
{
    public class GetClassMenu
    {
        public ClassMenuWeb GetStClassesweb(ModelLayout model) {

            TNPWMSSYSDBContext db = new();
            ClassMenuWeb modelClass = new ClassMenuWeb()
            {
                wmsClasses=new(),
                Users=new(),
            };
                

            try
            {
                modelClass.Users = GetClassModel(model.Id);

                modelClass.wmsClasses = db.Ctlclasswebs.Where(t =>
                                    t.Classid == modelClass.Users.ClassId
                                    && t.Actionflg == "Y"
                                    ).OrderBy(t => t.Menuseq).ToList();


               

            } catch (Exception) {
            
            }
            
            return modelClass;
        }


        public static ClassUserLogin GetClassModel(decimal? Logid)
        {
            TNPWMSSYSDBContext db = new();
            ClassUserLogin Model = new();



            try
            {
                SysDatalog data = new();
                var sysUserLog = db.SysDatalogs.Where(t =>
                t.LogId == Logid
                ).FirstOrDefault();

                if (sysUserLog != null)
                {
                    var flgService = (from t in db.SysDatalogs
                                      join t2 in db.Ctlconfigs
                                      on t.Whid equals t2.Whid
                                      join t3 in db.Ctluserlogins
                                      on t.UserLogin equals t3.UserName
                                      where t.UserLogin == sysUserLog.UserLogin
                                      select new { t.UserLogin, t.Whid,t3.Dcid, t2.Whdb,t2.Whconnect, t2.MainMstDb, t2.Mainconnect,t3.ClassId }).FirstOrDefault();




                    if (flgService != null)
                    {
                        Model.UserName = flgService.UserLogin;
                        Model.ClassId = flgService.ClassId;
                        Model.DBKey = flgService.Whdb;
                        Model.ConnectString = flgService.Whconnect;
                        Model.Connectmain = flgService.Mainconnect;
                        Model.MainDB = flgService.MainMstDb;
                        Model.DCcode = flgService.Dcid;
                        Model.WHcode = flgService.Whid;
                        Model.WLCode = flgService.Whid;
        
                        if (Logid != null)
                        {
                            Model.logid = (decimal)Logid;
                        }
                        else { Model.logid = 0; }
                       
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
       


    }
}
