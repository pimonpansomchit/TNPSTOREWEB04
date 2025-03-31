
#nullable disable
namespace TNPSTOREWEB.Model.Request
{
    public class MessageModel
    {
        public string StatusKey { get; set; }
        public string StatusID { get; set; } = null!;
        
        public string Message { get; set; }
    }
}
