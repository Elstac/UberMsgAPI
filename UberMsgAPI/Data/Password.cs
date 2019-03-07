using System.ComponentModel.DataAnnotations;

namespace UberMsgAPI
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PassHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
