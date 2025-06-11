using TNPWMSWEB.Context;
using TNPWMSWEB.Model;
using TNPWMSWEB.Models;

namespace MBSASSET.Core
{
    public class GetDataList
    {
        TNPWMSSYSDBContext db = new();
        TNPSYSCTLDBContext dbs =new();
        public List<MstWl> Getlistwh()
        {
            List<MstWl> wh = new();
            
            wh = dbs.MstWls.Where(t => t.WlId != null).OrderBy(t => t.WlId).ToList();

            return wh;

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
                                grouplists = groupResult.Select(g => new { g.GroupId, g.GroupName }).OrderBy(p => p.GroupId).ToList() // Collecting ItemNames in a list
                            };
                foreach (var t in group)
                {
                    if (t.GroupId != null && t.GroupName != null)
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
        public List<Ctlstatus> Getliststatus(string statustype)
        {
            List<Ctlstatus> regstatus = new();
                regstatus = db.Ctlstatuses.Where(t => t.StatusCode == statustype).OrderBy(o => o.Statusno).ToList();
            return regstatus;
        }
        public List<Ctlclassinfo> GetlistClass()
        {
            List<Ctlclassinfo> classlist =new();
            var classlists = db.Ctlclassinfos.OrderBy(o => o.ClassCode).ToList();
           
            return classlist;
        }
       
        
    }
}
