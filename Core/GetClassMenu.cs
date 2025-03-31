using TNPSTOREWEB.Context;
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
    }
}
