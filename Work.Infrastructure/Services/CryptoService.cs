using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Work.Infrastructure.ServicesInterface;

namespace Work.Infrastructure.Services
{
   public class CryptoService:ICryptoService
    {
        public string Encrypt(string payload, string salt)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(salt);
            var b64S = System.Convert.ToBase64String(plainTextBytes);
            string result = "";
            try
            {
                byte[] numArray = Convert.FromBase64String(b64S);
                DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider()
                {
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7,
                    Key = numArray
                };
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] bytes = Encoding.Default.GetBytes(payload);
                        cryptoStream.Write(bytes, 0, (int)bytes.Length);
                        cryptoStream.FlushFinalBlock();
                        result = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }

            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MethodBase currentMethod = MethodBase.GetCurrentMethod();
                object[] objArray = new object[] { payload, exception.Message, exception.InnerException };

            }
            return result;
        }
   

        public string Decrypt(string cryptoString, string salt)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(salt);
            var b64S= System.Convert.ToBase64String(plainTextBytes);
            bool flag;
            string result = "";
            try
            {
                byte[] numArray = Convert.FromBase64String(b64S);
                if (!string.IsNullOrEmpty(cryptoString))
                {
                    DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider()
                    {
                        Mode = CipherMode.ECB,
                        Padding = PaddingMode.PKCS7,
                        Key = numArray
                    };
                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptoString)))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            result = (new StreamReader(cryptoStream)).ReadToEnd();
                        }
                    }
                    return result;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MethodBase currentMethod = MethodBase.GetCurrentMethod();
                object[] objArray = new object[] { cryptoString, exception.Message, exception.InnerException };
                flag = false;
            }
            return result;
        }
    }
}

