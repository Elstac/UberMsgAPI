using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Classes
{
    public interface ILoginActivator
    {
        string ActivateLogin(string username);
    }
}
