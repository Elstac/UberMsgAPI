using System;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace UberMsgAPI.Classes
{
    public class Hasher : IHasher
    {

        public Hash ComputeHash(string password, byte[] salt)
        {
            using (var sha = SHA256.Create())
            {
                var input = new List<byte>();
                input.AddRange(salt);
                input.AddRange(System.Text.Encoding.Unicode.GetBytes(password));

                return new Hash( sha.ComputeHash(input.ToArray()));
            }
        }
    }
}
