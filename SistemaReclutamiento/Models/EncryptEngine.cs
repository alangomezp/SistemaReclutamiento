using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCrypt;

namespace SistemaReclutamiento.Models
{
    public class EncryptEngine
    {

        public static String Encriptar(string _password)
        {
            XCryptEngine xcrip = new XCryptEngine();

            try
            {
                xcrip.InitializeEngine(XCryptEngine.AlgorithmType.SHA);
                return xcrip.Encrypt(_password);
            }
            finally
            {

                xcrip.DestroyEngine();
            }

        }

        public static String Desencriptar(string _password)
        {
            XCryptEngine xcrip = new XCryptEngine();

            try
            {
                xcrip.InitializeEngine(XCryptEngine.AlgorithmType.SHA);
                return xcrip.Decrypt(_password);
            }
            finally
            {

                xcrip.DestroyEngine();
            }

        }

    }
}