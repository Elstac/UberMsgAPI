using System;
using System.Collections.Generic;

namespace UberMsgAPI.Models
{
    public interface IKeyManager
    {
        IEnumerable<Connection> GetPendingConnections(string username);
        void EstablishConnection(string reciverUsername,string senderUsername, ulong publicKey);
        Tuple<int, ulong> GetGenerator();
    }
}
