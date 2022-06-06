using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Classes
{
    /// <summary>
    /// 转账记录
    /// </summary>
    public class TransferRecord
    {
        public string Name
        {
            get;set;
        }

        public DateTime UtcTime
        {
            get;set;
        }

        public DateTime LocalTime
        {
            get;set;
        }

        public decimal Amount
        {
            get;set;
        }

        public TransferRecord()
        {

        }

        public TransferRecord(string name,decimal amount,DateTime time)
        {
            this.Name = name;
            this.Amount = amount;
            this.UtcTime = time;
        }
    }
}
