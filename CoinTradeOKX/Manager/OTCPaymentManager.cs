using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Manager
{
 

    public class OTCPaymentManager
    {
        private OTCPaymentManager()
        {

        }

        

        private static OTCPaymentManager _instance = null;
        private static object _lock = new object();
        public static OTCPaymentManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(_lock)
                    {
                        if(_instance == null)
                        {
                            var pm = new OTCPaymentManager();
                            _instance = pm;
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
