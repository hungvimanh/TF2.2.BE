using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TwelveFinal.Repositories.Models;

namespace DataSeeding
{
    public class CommonInit
    {
        protected TFContext DbContext;
        public CommonInit(TFContext _context)
        {
               DbContext = _context;
        }

        public static Guid CreateGuid(string name)
        {
            MD5 md5 = MD5.Create();
            Byte[] myStringBytes = ASCIIEncoding.Default.GetBytes(name);
            Byte[] hash = md5.ComputeHash(myStringBytes);
            return new Guid(hash);
        }
    }
}
