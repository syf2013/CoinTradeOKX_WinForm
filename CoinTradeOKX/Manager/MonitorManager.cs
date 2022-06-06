using CoinTradeOKX.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinTradeOKX.Okex;
using Common;
using CoinTradeOKX.Event;

namespace CoinTradeOKX
{
    public class MonitorManager
    {
        List<MonitorBase> allMonitor = new List<MonitorBase>();
        public MonitorManager()
        {

        }

        public List<MonitorBase> GetAllMonitor()
        {
            return new List<MonitorBase>(this.allMonitor.ToArray());
        }

        public void AddMonotor(MonitorBase monitor)
        {
            if(this.allMonitor.Contains(monitor))
            {
                return;
            }

            allMonitor.Add(monitor);
            EventCenter.Instance.Emit(EventNames.MonitorChanged, this);
        }

        public void DestoryAllMonitors()
        {
            foreach(var m in this.allMonitor)
            {
                m.Destory();
            }

            this.allMonitor.Clear();
            EventCenter.Instance.Emit(EventNames.MonitorChanged, this);
        }

        public void RemoveMonitor(MonitorBase monitor)
        {
            this.allMonitor.Remove(monitor);
            EventCenter.Instance.Emit(EventNames.MonitorChanged, this);
        }
        
        public bool AllIsEffective()
        {
            foreach(var m in this.allMonitor)
            {
                if (!m.Effective)
                    return false;
            }

            return true;
        }

        public void Update(int dt)
        {
            foreach(var m in allMonitor)
            {
                m.Update(dt);
            }
        }

        static MonitorManager _default = null;
        public static MonitorManager Default {
            get
            {
                if(_default == null)
                {
                    _default = new MonitorManager();
                }

                return _default;
            }
        }
    }
}
