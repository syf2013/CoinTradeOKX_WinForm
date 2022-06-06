using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Behavior
{
    public class BehaviorBase : IDisposable
    {
        public bool Executing { get; protected set; }

        public virtual bool Enable { get;  set; }

        public virtual string Message
        {
            get
            {
                return "";
            }
        }

        public  virtual void OnParamaterChanged()
        {

        }

        public virtual void Dispose()
        {
            this.Enable = false;
        }
    }
}
