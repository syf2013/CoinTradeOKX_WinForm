using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    

    public class CTCDepthRESTProvider : IDisposable
    {
        private bool disposed = false;

        private CTCDepthRESTProvider()
        {
            this.BeginBudate();
        }


        private void BeginBudate()
        {
            Thread t = new Thread(this._update_tickets);
            t.Start();
        }

        private void _update_tickets()
        {
            while (!disposed)
            {

            }
        }

        public void Dispose()
        {
            this.disposed = true;
        }

        private JToken tickets = null;

        private static CTCDepthRESTProvider _instance;
        private static object locker = new object();



        public static CTCDepthRESTProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new CTCDepthRESTProvider();
                        }
                    }
                }

                return _instance;
            }
        }

    }
}
