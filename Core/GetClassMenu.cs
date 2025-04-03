#nullable disable
using TNPSTOREWEB.Context;
using TNPSTOREWEB.Model;
using TNPSTOREWEB.Models;

namespace TNPSTOREWEB.Core
{
    public class GetClassMenu
    {
        public List<StClass> GetStClasses(string Classid, int Level,int Parent,int Root,int submenukey) {

            TNPSTORESYSDBContext db = new();
            List<StClass> Model = new();

            try
            {
                if (submenukey == 0)
                {
                    Model = db.StClasses.Where(t =>
                                   t.Classid == Classid
                                   && t.Menutype == Level
                                   && t.Actionflg == "Y"
                                   && t.ParentKey == Parent
                                   && t.RootKey == Root
                                   ).OrderBy(t => t.Menuseq).ToList();

                }
                else
                {
                    var menu = db.StClasses.Where(t =>
                                  t.Classid == Classid
                                  && t.Menutype == Level
                                  && t.Actionflg == "Y"
                                  && t.Menukey == Parent
                                  ).Select(x => x.ParentKey).FirstOrDefault();

                    Model = db.StClasses.Where(t =>
                                  t.Classid == Classid
                                  && t.Menutype == Level
                                  && t.Actionflg == "Y"
                                  && t.ParentKey == menu
                                  ).OrderBy(t => t.Menuseq).ToList();

                }

            } catch (Exception) {
            
            }

            return Model;
        }

        public List<StClassweb> GetStClassesweb(string Classid, int Level, int Parent, int Root, int submenukey)
        {

            TNPSTORESYSDBContext db = new();
            List<StClassweb> Model = new();

            try
            {
                if (submenukey == 0)
                {
                    Model = db.StClasswebs.Where(t =>
                                   t.Classid == Classid
                                   //&& t.Menutype == Level
                                   && t.Actionflg == "Y"
                                   //&& t.ParentKey == Parent
                                   //&& t.RootKey == Root
                                   ).OrderBy(t => t.Menuseq).ToList();

                }
             

            }
            catch (Exception)
            {

            }

            return Model;
        }


        public ModelLayout Getuserlogin(ModelLayout data)
        {
            data.usr = new();
            data.wls = new();
            data.cinfo = new();
            GetClassMenu getClassMenu = new();
            TNPSYSCTLDBContext dbc = new();
            using (var db = new TNPSTORESYSDBContext())
            {
                if(data.actions !="A")
                {
                    var datalist = db.StUserlogins.OrderBy(t => t.WlCode).ToList();
                    if (datalist != null)
                    {
                        if (data.SelectedOption != "0")
                        {
                            datalist = datalist.Where(t => t.WlCode == (data.SelectedOption).Trim()).ToList();
                        }

                        if (data.SelectGroupNo != "0" && data.SelectGroupNo != null)
                        {
                            var clssid = db.StClassinfos.Where(t => t.ClassCode == data.SelectGroupNo.Trim()).Select(t => t.ClassName).First();

                            datalist = datalist.Where(t => t.ClassId == clssid.Trim()).ToList();

                        }
                        if(data.Selectedadd != null)
                        {
                            datalist = datalist.Where(t => t.UserName == data.Selectedadd.Trim()).ToList();
                        }
                        data.usr = datalist;
                        if (data.actions == "M")
                        {
                            if(datalist != null)
                            {
                                data.addusr = datalist.First();
                            }
                           
                        }
                    }
                }


                data.wls = Getlis();
                data.cinfo = db.StClassinfos.OrderBy(t => t.ClassCode).ToList();
                data.ModelClass.stClasses = getClassMenu.GetStClassesweb(data.ModelClass.Users.ClassId, 0, 0, 0, 0);


            }
            return data;
        }

        public List<MstWl> Getlis()
        {
           List<MstWl> wl = new();
            TNPSYSCTLDBContext db = new();
                wl= db.MstWls.Where(t => t.CustKey != null).OrderBy(t => t.WlId).ToList();

            return wl;

        }
    }
}
