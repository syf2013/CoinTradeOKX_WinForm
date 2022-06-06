using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CoinTradeOKX.Monitor;

namespace CoinTradeOKX.Okex
{
    [MonitorName(Name = "时间校准")]
    public class TimeMonitor:RESTMonitor
    {
        public TimeMonitor()
            :base(new TimeServiceApi(),15000)
        {

        }

        protected override void OnDataUpdate(JToken data)
        {
            long ts = ((data as JArray)[0]).Value<long>("ts");
            DateUtil.SetServerTimestamp(ts);
            this.Feed();
            base.OnDataUpdate(data);
        }
    }
}
