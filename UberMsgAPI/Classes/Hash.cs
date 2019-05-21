using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberMsgAPI.Classes
{
    public class Hash
    {
        private byte[] value;

        public byte[] Value { get => value; }

        public Hash(byte[] val)
        {
            value = val;
        }

        public override bool Equals(object obj)
        {
            var o = obj as byte[];

            for (int i = 0; i < value.Length; i++)
                if (o[i] != value[i])
                    return false;

            return true;
        }
 
   }
}
