using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CoinTradeGecko.Mail
{
    public class MailBox
    {
        long LastUpdate = 0;
        ImapClient MailClient = null;
        int lastCount = 0;
        IMailFolder inbox;
        string username = "";
        string password = "";

        public event Action<int> OnNewMail = null;  
        List<MimeMessage> messages = new List<MimeMessage>();
        string host;

        public event Action Opened = null;
        Pop3Client pop3 = null;
        public MailBox(string host,string user, string password)
        {
            this.host       = host;
            this.username   = user;
            this.password   = password;
            this.ReConnect();

          //  pop3 = new Pop3Client();
          //  pop3.ConnectAsync(host, 21, true);
          //  pop3.Connected += Pop3_Connected;
        }

        private void Pop3_Connected(object sender, ConnectedEventArgs e)
        {
            pop3.Authenticated += Pop3_Authenticated;
            pop3.AuthenticateAsync(username, password);
            Console.Write("");
            
        }

        private void Pop3_Authenticated(object sender, AuthenticatedEventArgs e)
        {
            var count = pop3.GetMessageCount();
            var messages = pop3.GetMessages(count - 10, count);

            
        }

        public void ReConnect()
        {
            MailClient = new ImapClient();
            MailClient.Alert += MailClient_Alert;
            MailClient.Connected += MailClient_Connected;
            MailClient.Disconnected += MailClient_Disconnected;

           
            MailClient.ConnectAsync(host, 143);
        }

        private void Inbox_Opened(object sender, EventArgs e)
        {
          
            lastCount = inbox.Count;
            inbox.RecentChanged += Inbox_RecentChanged;
            inbox.CountChanged += Inbox_CountChanged;
            this.Opened?.Invoke();

            //this.DoIdel();
        }

        private void MailClient_Disconnected(object sender, DisconnectedEventArgs e)
        {
            if (!e.IsRequested)
                this.ReConnect();
        }
        private void MailClient_Connected(object sender, ConnectedEventArgs e)
        {
            MailClient.Connected -= MailClient_Connected;
            MailClient.Authenticated += MailClient_Authenticated;
            MailClient.AuthenticateAsync(username, password);
        }
        private void MailClient_Authenticated(object sender, AuthenticatedEventArgs e)
        {
            //MailClient.EnableUTF8Async();
            //MailClient.EnableQuickResync();

            inbox = MailClient.Inbox;
            inbox.Opened += Inbox_Opened;
            inbox.OpenAsync(FolderAccess.ReadOnly);

           var c =  this.MailClient.Capabilities;

            var msg = e.Message;
            Console.Write(msg);
        }

        private CancellationTokenSource idleDoneToken = null;
        private CancellationTokenSource idleCancelToken = null;
        private Task idle_task = null;
        private void DoIdel()
        {
            ExitIdel();
            idleDoneToken = new CancellationTokenSource();
            idleCancelToken = new CancellationTokenSource();
            idle_task = MailClient.IdleAsync(idleDoneToken.Token, idleCancelToken.Token);
        }

        private void ExitIdel()
        {
            if (idleDoneToken != null)
            {
                idleCancelToken.Cancel();
                idleDoneToken.Cancel();
                idle_task.Wait();
                idle_task = null;
                idleCancelToken.Dispose();
                idleCancelToken = null;
                idleDoneToken.Dispose();
                idleDoneToken = null;
            }
        }

        private void Inbox_CountChanged(object sender, EventArgs e)
        {
            
            var newCount = inbox.Count - this.lastCount;
            // GetMessage(newCount);
            this.lastCount = inbox.Count;
            if (newCount > 0)
            {
                this.OnNewMail.Invoke(newCount);
            }
        }

        public void Close()
        {
            if (this.inbox != null && this.inbox.IsOpen)
            {
                this.inbox.CloseAsync();
            }
            if (this.MailClient != null)
            {
                this.MailClient.Disconnected -= MailClient_Disconnected;
                if (this.MailClient.IsConnected)
                this.MailClient.Disconnect(true);
                this.MailClient.Dispose();
            }

            this.inbox = null;
            this.MailClient = null;
        }

        private void Inbox_RecentChanged(object sender, EventArgs e)
        {
            var newCount = inbox.Count - this.lastCount;
            // GetMessage(newCount);
            this.lastCount = inbox.Count;
            if (newCount > 0)
            {
                this.OnNewMail.Invoke(newCount);
            }
        }
        private void MailClient_Alert(object sender, MailKit.AlertEventArgs e)
        {
             string alert = e.Message;
            Console.Write(alert);
        }

        CancellationToken idle_token = new CancellationToken(true);
        public IEnumerable<MimeMessage> GetMessage(int count)
        {
            this.ExitIdel();
            List<MimeMessage> list = new List<MimeMessage>();
            for(int i = 1; i <= count;i++)
            {
                var ms = inbox.GetMessage(lastCount - i);
                list.Add(ms);
            }

            this.DoIdel();

            return list;
        }

        public void Idel()
        {
            if (MailClient.Capabilities.HasFlag(ImapCapabilities.Idle))
            {
                if (!MailClient.IsIdle)
                {
                    CancellationTokenSource tokenSource = new CancellationTokenSource();
                    // var token = new CancellationToken(true);

                    MailClient.Idle(tokenSource.Token);
                }
            }
        }

        public void Update()
        {
            Console.Write("send noop");
            if (this.MailClient != null && this.MailClient.IsAuthenticated)
            {
               
          //      MailClient.NoOp();
            }
        }
    }
}
