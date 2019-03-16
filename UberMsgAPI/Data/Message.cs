using System;
using System.ComponentModel.DataAnnotations;

namespace UberMsgAPI
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Reciver { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
