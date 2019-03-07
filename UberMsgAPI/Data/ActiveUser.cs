using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI
{
    public class ActiveUser
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
