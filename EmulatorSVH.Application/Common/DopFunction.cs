using System.Security.Cryptography;
using System.Text;


namespace ServerSVH.Application.Common
{
    
    public class ResLoadMess
    {
        public string UUID { get; set; }= string.Empty;
        public string DocId { get; set; } = string.Empty;
        public string CodeDoc { get; set; } = string.Empty;
        public string NumDoc { get; set; } = string.Empty;
        public string DateDoc { get; set; } =string.Empty;
    };

}
