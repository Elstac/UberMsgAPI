using System.ComponentModel.DataAnnotations;

namespace UberMsgAPI
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PassHash { get; set; }
    }
}
