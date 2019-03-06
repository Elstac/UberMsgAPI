using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Classes
{
    interface IHasher
    {
        string ComputeHash(string password, byte[] salt);
    }
}
