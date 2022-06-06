using Common.Classes;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoinTradeGecko.Mail
{
    public class TransferRecordParser:IDisposable
    {
        private const int MaxListSize = 1500;
        private object list_locker = new object();
        public event Action<TransferRecord> OnNewTransferRecord = null;

        private List<TransferRecord> Records
        {
            get;
            set;
        }

        public string BankMailAddress
        {
            get;set;
        }
        //您账户(?<account>\d+)于\d+月\d+日收到.行转入人民币(?<money>\d+\.\d+)，付方(?<name>.+?)[，$]
        public string Matching
        {
            get;set;
        }

        private Regex MatchRegex
        {
            get;set;
        }

        public MailBoxPop3 MailBox
        {
            get; private set;
        }

        public TransferRecordParser(string host,int port ,string username,string password)
        {
            var mailBox = new MailBoxPop3(host, port, username, password);
            this.MailBox = mailBox;
            mailBox.OnNewMessage += MailBox_OnNewMessage;

            this.Records = new List<TransferRecord>();
        }

        private void ParseMailToTransferRecord(MimeMessage mail)
        {
            if (this.MatchRegex == null)
            {
                this.MatchRegex = new Regex(this.Matching);
            }

            string mailContent = mail.TextBody != null ? mail.TextBody : mail.HtmlBody;

            if (string.IsNullOrEmpty(mailContent))
                return;

            var match = this.MatchRegex.Match(mailContent);
            if (match != null && match.Success)
            {
                string name = "";
                if(match.Groups["name"] != null)
                {
                    name = match.Groups["name"].Value;
                }
                else
                {
                    return;
                }
                string strMoney;
                if (match.Groups["money"] != null)
                {
                    strMoney = match.Groups["money"].Value;
                }
                else
                {
                    return;
                }

                decimal money = 0;

                decimal.TryParse(strMoney, out money);

                TransferRecord record = new TransferRecord
                {
                    Amount = money,
                    Name = name,
                    UtcTime = mail.Date.UtcDateTime,
                    LocalTime = mail.Date.DateTime

                };

                lock(list_locker)
                {
                    if (Records.Count > MaxListSize)
                        Records.RemoveAt(Records.Count - 1);
                    Records.Insert(0,record);
                }

                this.OnNewTransferRecord?.Invoke(record);
            }
        }

        public void RemoveRecord(TransferRecord record)
        {
            lock(list_locker)
            {
                this.Records.Remove(record);
            }
        }


        /// <summary>
        /// 查找转账记录
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="money">金额</param>
        /// <param name="utcTime">utc时间</param>
        /// <param name="error">允许误差率</param>
        /// <returns></returns>
        public TransferRecord FindRecord(string name,decimal money,DateTime utcTime, decimal error = 0)
        {
            lock(list_locker)
            {
                foreach(var r in Records)
                {
                    if(r.Name == name && r.UtcTime > utcTime)
                    {
                        if (Math.Abs( r.Amount - money) / money <= error)
                            return r;
                    }
                }
            }

            return null;
        }
        private void MailBox_OnNewMessage(object sender, NewMessageEventArgs e)
        {
            if (string.IsNullOrEmpty(this.BankMailAddress))
                return;

            foreach (var m in e.Messages)
            {
                if (m.From.Count <= 0)
                    continue;

                var address = m.From[0] as MailboxAddress;

                if (string.Compare(this.BankMailAddress, address.Address, true) == 0)
                {
                    this.ParseMailToTransferRecord(m);
                }
            }
        }

        public void Dispose()
        {
            if(this.MailBox != null)
            {
                this.MailBox.Dispose();
            }


            this.OnNewTransferRecord = null;
        }
    }
}
