using Common.Classes;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Classes
{
    public class DepthBook:IDepthProvider
    {
        private IList<DepthInfo> _sellList = new List<DepthInfo>();
        private IList<DepthInfo> _buyList = new List<DepthInfo>();

        public DateTime Time
        {
            get;
            set;
        }

        

        public void EachDeep(SideEnum side, Action<DepthInfo> callback)
        {
            IList<DepthInfo> list = null;

            switch (side)
            {
                case SideEnum.Buy:
                    list = this._buyList;
                    break;
                case SideEnum.Sell:
                    list = this._sellList;
                    break;
            }

            lock (list)
            {
                foreach (var d in list)
                {
                    callback(d);
                }
            }
        }

        public void Update(SideEnum type, IList<DepthInfo> datas)
        {
            IList<DepthInfo> list = null;

            switch (type)
            {
                case SideEnum.Buy:
                    list = this._buyList;
                    break;
                case SideEnum.Sell:
                    list = this._sellList;
                    break;
            }

            lock(list)
            {
                Pool<DepthInfo> pool = Pool<DepthInfo>.GetPool();
                pool.Put(list);
                list.Clear();

                foreach(var d in datas)
                {
                    list.Add(d);
                }
            }
        }
    }
}
