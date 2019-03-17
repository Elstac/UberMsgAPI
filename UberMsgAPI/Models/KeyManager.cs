using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Models
{
    public class KeyManager : IKeyManager
    {
        private UserDbContext context;
        private int g = 2137;
        private ulong n = 17446744073213751615;

        public KeyManager(UserDbContext context)
        {
            this.context = context;
        }

        public void EstablishConnection(string reciverUsername, string senderUsername, ulong publicKey)
        {
            var query = from connection in context.Connections
                        where connection.Sender == senderUsername && connection.Reciver == reciverUsername
                        select connection;

            if (query.ToList().Count == 1)
                throw new InvalidOperationException("Connection already established");

            context.Connections.Add(new Connection { PublicKey = publicKey, Sender = senderUsername, Reciver = reciverUsername });
            context.SaveChanges();
        }

        public Tuple<int, ulong> GetGenerator()
        {
            return new Tuple<int, ulong>(g, n);
        }

        public IEnumerable<Connection> GetPendingConnections(string username)
        {
            var query = from connection in context.Connections
                        where connection.Reciver == username && !connection.Estalished
                        select connection;

            return query.ToArray();
        }
    }
}
