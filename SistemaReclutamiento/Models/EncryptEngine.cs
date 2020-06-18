using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using XCrypt;

namespace SistemaReclutamiento.Models
{
    public class EncryptEngine
    {

        public static String Encriptar(string _password, string key)
        {

                TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

                byte[] byteHash;
                byte[] byteBuff;

                byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                desCryptoProvider.Key = byteHash;
                desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Encoding.UTF8.GetBytes(_password);

                string encoded =
                    Convert.ToBase64String(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                return encoded;

        }

        public static String Desencriptar(string _password, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Convert.FromBase64String(_password);

            string plaintext = Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return plaintext;
        }

    }
}