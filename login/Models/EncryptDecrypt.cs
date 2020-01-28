using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace login.Models
{
    public class EncryptDecrypt
    {
        public string Base64Encode(string plaintext)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plaintext);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string Base64Decode(string BaseEncodedData)
        {
            var Base64EncodedBytes = System.Convert.FromBase64String(BaseEncodedData);
            return System.Text.Encoding.UTF8.GetString(Base64EncodedBytes);
        }
            
    }
}