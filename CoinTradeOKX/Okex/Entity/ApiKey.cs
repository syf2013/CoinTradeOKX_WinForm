using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{
    public class ApiKey
    {
        public string Key { get;  set; } //
        public string Passphrase { get;  set; }// 
        public string SecretKey { get;  set; }//
        public string ApiAddress { get; set; }

        /// <summary>
        /// 是否是模拟盘
        /// </summary>
        public bool IsSimulated { get; set; } 

        public string GetKey()
        {
            return this.Key;
        }

        public string GetPassphrase()
        {
            return this.Passphrase;
        }

        public string GetSecretKey()
        {
            return this.SecretKey;
        }

        public void SetKey(string val)
        {
            this.Key  = val;
        }

        public void SetPassphrase(string val)
        {
            this.Passphrase = val;
        }

        public void SetSecretKey(string val)
        {
            this.SecretKey = val;
        }

        public bool ParseFromJson(JToken json)
        {
            try
            {
                this.Key = json["Key"].Value<string>();
                this.Passphrase = json["Passphrase"].Value<string>();
                this.SecretKey = json["SecretKey"].Value<string>();
                this.ApiAddress = json["ApiAddress"].Value<string>();
                this.IsSimulated = json["IsSimulated"] != null ? json["IsSimulated"].Value<bool>():false;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
