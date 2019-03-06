using System;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace UberMsgAPI.Classes
{
    public class Hasher : IHasher
    {

        public string ComputeHash(string password, byte[] salt)
        {
            using (var sha = SHA256.Create())
            {

                var input = new List<byte>();
                input.AddRange(salt);
                input.AddRange(System.Text.Encoding.Unicode.GetBytes(password));

                var rawResult = sha.ComputeHash(input.ToArray());
                return rawResult.ToString();
            }
        }
    }
}
