using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI
{
    public class Connection
    {
        [Key]
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Reciver { get; set; }
        public ulong PublicKey { get; set; }
        public bool Estalished { get; set; }
    }
}
