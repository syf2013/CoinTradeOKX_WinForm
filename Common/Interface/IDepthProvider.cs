using Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    public interface IDepthProvider
    {
        //DateTime Time { get; }
        void EachDeep(SideEnum side, Action<DepthInfo> callback);
    }
}
