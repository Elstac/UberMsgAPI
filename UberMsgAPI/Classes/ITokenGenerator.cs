using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Classes
{
    public interface ITokenGenerator
    {
        string GetToken(int length);
    }
}
