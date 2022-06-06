using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 事件中心
    /// </summary>
    public class EventCenter
    {
        private EventCenter()
        {

        }

        private Dictionary<string, HashSet<Action<object>>> EventListners = new Dictionary<string, HashSet<Action<object>>>();

        public void Emit(string name, object argument = null)
        {
            if (EventListners.ContainsKey(name))
            {
                var listeners = EventListners[name];
                lock (listeners)
                {
                    //List<Action<Object>> temp = listeners.ToList();
                    foreach (var l in listeners)
                    {
                        l(argument);
                    }
                }
            }
        }

        public void AddEventListener(string name, Action<object> listener)
        {
            lock(this)
            {
                HashSet<Action<object>> listeners = null;
                if(EventListners.ContainsKey(name))
                {
                    listeners = EventListners[name];
                }
                else
                {
                    listeners = new HashSet<Action<object>>();
                    EventListners[name] = listeners;
                }

                lock (listeners)
                {
                    if (!listeners.Contains(listener))
                    {
                        listeners.Add(listener);
                    }
                }
            }
        }

        public void RemoveListener(string name, Action<object> listener)
        {
            lock (this)
            {
                if (EventListners.ContainsKey(name))
                {
                    HashSet<Action<object>> listeners = EventListners[name];
                    lock (listeners)
                    {
                        listeners.Remove(listener);
                    }
                }
            }
        }

        private static EventCenter _instance = null;
        private static object locker = new object();
        public static EventCenter Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(locker)
                    {
                        if(_instance == null)
                        {
                            var ins = new EventCenter();

                            _instance = ins;
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
