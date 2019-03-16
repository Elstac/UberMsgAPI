using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI
{
    public class AlreadyLoggedInException:Exception
    {
        public AlreadyLoggedInException(string message):base(message)
        {
            
        }
    }
}
