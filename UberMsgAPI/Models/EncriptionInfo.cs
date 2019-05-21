using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Models
{
    public class EncriptionInfo : IEncriptionInfo
    {
        private UserDbContext context;

        public EncriptionInfo(UserDbContext context)
        {
            this.context = context;
        }

        public ulong GetPublicKey(string sender, string reciver)
        {
            var query = from connection in context.Connections
                        where connection.Reciver == sender && connection.Sender == reciver
                        select connection;

            var l = query.ToList();

            if (l.Count == 0)
                throw new InvalidOperationException("Connection not established");

            return l[0].PublicKey;
        }
    }
}
