
#nullable disable
namespace MBSASSET.Model.Request
{
    public class MessageModel
    {
        public string StatusKey { get; set; }
        public string StatusID { get; set; } = null!;
        public string Statusmsg { get; set; }
        public string Message { get; set; }
        public int MessageId { get; set; }
    }
}
