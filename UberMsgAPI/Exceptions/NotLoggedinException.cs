using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI
{
    public class NotLoggedinException:Exception
    {
        public NotLoggedinException(string message):base(message)
        {

        }
    }
}
