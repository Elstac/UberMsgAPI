using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberMsgAPI.Classes
{
    public class TokenGenerator : ITokenGenerator
    {
        public string GetToken(int length)
        {
            var rng = new Random();
            var token = new StringBuilder();

            for (int i = 0; i < length; i++)
                token.Append((char)rng.Next(33,126));

            return token.ToString();
        }
    }
}
