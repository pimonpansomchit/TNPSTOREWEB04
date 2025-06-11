#nullable disable
using MBSASSET.Model.Request;
using TNPWMSWEB.Context;
namespace TNPWMSWEB.Core
{
    public class Systemcontrol
    {
        public MessageModel GetMessage(string Msgcode, string Msg)
        {
            MessageModel model = new();
            using (TNPWMSSYSDBContext db = new())
            {
                var msgiinfo = db.Ctlmessages.Where(t => t.MsgCode == Msgcode.Trim()).First();
                if(msgiinfo != null)
                {
                    model.StatusID= Msgcode.Trim();
                    model.Statusmsg = msgiinfo.MsgShotTh.Trim();
                    model.StatusKey = msgiinfo.MsgType.Trim();
                    model.Message = msgiinfo.MsgLangTh + Msg.Trim();
                    
                }

            }

            return model;
        }
      

        
    }
}
