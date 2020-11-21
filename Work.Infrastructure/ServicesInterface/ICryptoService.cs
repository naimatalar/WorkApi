using System;
using System.Collections.Generic;
using System.Text;

namespace Work.Infrastructure.ServicesInterface
{
    public interface ICryptoService
    {
        string Encrypt(string payload,string salt);
        string Decrypt(string cryptoString, string salt);
    }
}
