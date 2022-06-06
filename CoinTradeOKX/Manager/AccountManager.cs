using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Manager
{

    //internal class Account
    internal class AccountManager
    {
        private static Account _current;
        public static Account Current
        {
            get
            {
                return _current;
            }
        }

        public static Account GetAccountByApi(ApiKey api)
        {
            var accountApi = new Okex_Rest_Api_Account();
            accountApi.SetApi(api);
            var token = accountApi.execSync();

            int code = token.Value<int>("code");

            if (code == 0)
            {
                JArray arr = token["data"] as JArray;
                var account = new Account();
                account.ParseFromJson(arr[0]);

                return account;
             
            }

            return null;
        }

        public static void UpdateAccount(Account account)
        {
            _current = account;
        }

        public static bool UpdateAccount()
        {
            var accountApi = new Okex_Rest_Api_Account();
            var token = accountApi.execSync();

            int code = token.Value<int>("code");

            if (code == 0)
            {
                JArray arr = token["data"] as JArray;
                var account = new Account();
                account.ParseFromJson(arr[0]);
                _current = account;
            }

            return code == 0;
        }
    }
}
