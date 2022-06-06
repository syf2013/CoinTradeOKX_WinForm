using System;
using System.Collections.Generic;

namespace Common.Classes
{
    public class Pool<T> where T : new()
    {
        Queue<T> pool = new Queue<T>();

       
        public T Get()
        {
            lock (this)
            {
                if (this.pool.Count > 0)
                {
                    return this.pool.Dequeue();
                }
            }

            return new T();
        }

        public void Put(T obj, bool doLock = true)
        {
            if (doLock)
            {
                lock (this)
                {
                    this.pool.Enqueue(obj);
                }
            }
            else
            {
                this.pool.Enqueue(obj);
            }
        }

        public void Put(IEnumerable<T> list)
        {
            lock (this)
            {

                foreach (T o in list)
                {
                    this.Put(o, false);
                }
            }
        }

        private static Dictionary<string, object> pools = new Dictionary<string, object>();
        public static Pool<T> GetPool()
        {
            Type type = typeof(T);

            string name = type.FullName;

            lock (pools)
            {
                if (pools.ContainsKey(name))
                {
                    return pools[name] as Pool<T>;
                }

                Pool<T> p = new Pool<T>();

                pools[name] = p;
                return p;
            }
        }
    }
}