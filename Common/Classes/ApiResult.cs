using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Classes
{
    public class ApiResult
    {
        public ApiResult()
        {
            this.Code = 0;
            this.Message = "";
        }


        public ApiResult(JToken token)
            :this()
        {
            if(token != null)
            {
                if(token["code"] != null)
                {
                    this.Code = token.Value<int>("code");

                    if(token["msg"] != null)
                    {
                        this.Message = token.Value<string>("msg");
                    }
                }
            }
        }

        public string Message { get; set; }
        public int Code { get; set; }
    }
}
