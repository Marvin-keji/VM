﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VM.Start.Common;

namespace VM.Start.Common.Helper
{
    public class DesEncrypt
    {
        private const String KEY = "HZQ.!02192591";
        private static byte[] _rgbKey = ASCIIEncoding.ASCII.GetBytes(KEY.Substring(0, 8));
        private static byte[] _rgbIV = ASCIIEncoding.ASCII.GetBytes(KEY.Insert(0, "w").Substring(0, 8));
        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="text">需要加密的值</param>
        /// <returns>加密后的结果</returns>
        public static string Encrypt(string text)
        {
            try
            {
                DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
                using (MemoryStream memStream = new MemoryStream())
                {
                    CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateEncryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
                    StreamWriter sWriter = new StreamWriter(crypStream);
                    sWriter.Write(text);
                    sWriter.Flush();
                    crypStream.FlushFinalBlock();
                    memStream.Flush();
                    return Convert.ToBase64String(memStream.GetBuffer(), 0, (int)memStream.Length);
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="encryptText"></param>
        /// <returns>解密后的结果</returns>
        public static string Decrypt(string encryptText)
        {
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            byte[] buffer = Convert.FromBase64String(encryptText);

            using (MemoryStream memStream = new MemoryStream())
            {
                CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateDecryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
                crypStream.Write(buffer, 0, buffer.Length);
                crypStream.FlushFinalBlock();
                return ASCIIEncoding.UTF8.GetString(memStream.ToArray());
            }
        }
    }
}
