using Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX
{
    public static class ConfigCrypto
    {
        public static string Key { get; set; }
        public static readonly string AESIV = "I'm wenquan     ";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Encrypt(string val)
        {
            return EncryptWithKey(val, Key);
        }

        public static string EncryptWithKey(string val,string key)
        {
            if (string.IsNullOrEmpty(key))
                return val;
            return CryptoUtil.AESEncrypt(val, key, AESIV);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Decrypt(string val)
        {
            return DecryptWithKey(val, Key);
        }

        public static string DecryptWithKey(string val,string key)
        {
            if (string.IsNullOrEmpty(key))
                return val;

            return CryptoUtil.AESDecrypt(val, key, AESIV);
        }

    }
}
