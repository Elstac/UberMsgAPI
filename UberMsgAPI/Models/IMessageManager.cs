using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Models
{
    public interface IMessageManager
    {
        void SaveMessage(Message message);
        IEnumerable<Message> GetMessages(string[] users);
        IEnumerable<Message> GetMessages(string[] users,int number);
        IEnumerable<Message> GetMessages(string[] users,int number,int offset);
    }
}
