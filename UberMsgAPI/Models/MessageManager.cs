using System;
using System.Collections.Generic;
using System.Linq;

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
            var query = from user in context.Users
                        where user.Username == message.Reciver
                        select user;

            if (query.ToList().Count == 0)
                throw new InvalidOperationException("Unknown reciver");

            message.TimeStamp = DateTime.Now;
            context.Messages.Add(message);
            context.SaveChanges();
        }
    }
}
