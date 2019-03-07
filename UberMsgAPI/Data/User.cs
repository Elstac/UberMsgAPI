using System.ComponentModel.DataAnnotations;

namespace UberMsgAPI
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public int Role { get; set; }
    }
}
