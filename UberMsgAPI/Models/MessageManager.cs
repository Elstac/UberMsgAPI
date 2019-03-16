using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Models
{
    public class MessageManager : IMessageManager
    {
        private UserDbContext context;

        public MessageManager(UserDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Message> GetMessages(string[] users)
        {
            var query = from message in context.Messages
                        where (message.Sender == users[0] && message.Reciver == users[1]) || (message.Sender == users[1] && message.Reciver == users[0])
                        select message;

            return query.OrderByDescending(msg => msg.TimeStamp).ToList();
        }

        public IEnumerable<Message> GetMessages(string[] users, int number)
        {
            return GetMessages(users).ToList().GetRange(0, number);
        }

        public IEnumerable<Message> GetMessages(string[] users, int number, int offset)
        {
            return GetMessages(users).ToList().GetRange(offset, number);
        }

        public void SaveMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
