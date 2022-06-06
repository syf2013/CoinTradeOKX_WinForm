using CoinTradeOKX.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTradeOKX.Invoke
{
    /*js 函数接口接口定义， 函数名称既为类名，每个[JsonProperty]字段就是一个参数字段*/
    public class OTCInvoke
    {
        static int invoke_id = 1;

        public static int Timeout = 3000;

        protected bool isExecuting = false;

        public OTCInvoke()
        {
            this._Invoke_ = string.Format("invoke_{0}", invoke_id++);
        }

        protected string _Invoke_ { get; private set; } //public
        protected virtual string Namespance { get; set; }//public

        public string GetParams()
        {
            var s = JsonSerializer.Create();

            using (StringWriter sw = new StringWriter())
            {
                s.Serialize(sw, this);

                sw.Flush();

                var sb = sw.GetStringBuilder();
                var args = sb.ToString();
                sw.Close();
                return args;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(this.Namespance))
                sb.AppendFormat("{0}.", this.Namespance);

            sb.Append(this.GetType().Name);
            sb.AppendFormat("({0})", this.GetParams());

            return sb.ToString();
        }

        public virtual Task<JToken> execAsync()
        {
            return Task<JToken>.Run(()=> { return this.execSync(); });
        }

        public virtual JToken execSync()
        {
            return null;
        }
    }
}
