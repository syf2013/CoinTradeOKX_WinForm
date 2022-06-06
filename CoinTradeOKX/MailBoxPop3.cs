using MailKit.Net.Pop3;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace CoinTradeGecko.Mail
{

    public class NewMessageEventArgs:EventArgs
    {
        public IList<MimeMessage> Messages { get; set; }
    }
   
    public class MailBoxPop3 :IDisposable
    {
       private Pop3Client pop3 = null;
        private string host = "";
        private int port = 0;
        private string username = "";
        private string password = "";
        private int lastCount = 0;
        private CancellationTokenSource updateCancelToken = null;

        private bool isRunning = false;
        private bool isFirstRun = true;

        public event EventHandler OnAuthenticated = null;
        public event EventHandler<NewMessageEventArgs> OnNewMessage = null;
        public MailBoxPop3( string host,int port, string name, string password)
        {
            this.host = host;
            this.port = port;
            this.username = name;
            this.password = password;
            this.BeginUpdate();
        }

        public void Reconnect()
        {
            isRunning = true;
            using (var pop3 = new Pop3Client())
            {
                do
                {
                    var task = pop3.ConnectAsync(host, port);
                    try
                    {
                        task.Wait();
                    }
                    catch(Exception ex)
                    {
                        break;
                    }

                    if (!pop3.IsConnected)
                        break;

                    task = pop3.AuthenticateAsync(username, password);
                    try
                    {
                        task.Wait();
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                    if (!pop3.IsAuthenticated)
                        break;

                    int count = 0;

                    try
                    {
                        count = pop3.GetMessageCount();
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                    if (!isFirstRun)
                    {
                        int newCount = count - lastCount;
                        if (newCount > 0)
                        {
                            IList<MimeMessage> newMessages = null;
                            try
                            {
                                newMessages = pop3.GetMessages(count - newCount, newCount);
                            }
                            catch (Exception ex)
                            {
                                break;
                            }
                            this.OnNewMessage?.Invoke(this, new NewMessageEventArgs() { Messages = newMessages });
                        }
                    }

                    isFirstRun = false;
                    lastCount = count;

                    pop3.Disconnect(true); //<<---
                } while (false);
            }
            isRunning = false;
            //this.pop3.Connected += Pop3_Connected;
        }

        private void Pop3_Connected(object sender, MailKit.ConnectedEventArgs e)
        {
            this.pop3.Authenticated += Pop3_Authenticated;
            this.pop3.AuthenticateAsync(username, password);
        }

        private void Pop3_Authenticated(object sender, MailKit.AuthenticatedEventArgs e)
        {
            this.lastCount = pop3.GetMessageCount();
            this.OnAuthenticated?.Invoke(this, null);

            this.BeginUpdate();
        }

        /*public IList<MimeMessage> GetMessages(int count)
        {
            int mailCount = this.pop3.GetMessageCount();
            return this.pop3.GetMessages(mailCount - count, count);
        }
        */

        private void BeginUpdate()
        {
            this.updateCancelToken = new CancellationTokenSource();
            Task.Run(() => {

                while (true)
                {
                    if (updateCancelToken.IsCancellationRequested)
                        break;
                    if (!isRunning)
                    {
                        try
                        {
                            this.Reconnect();
                        }
                        catch(Exception ex)
                        {
                            Logger.Instance.LogException(ex);
                        }
                    }
                    Task.Delay(1500);
                }

                Console.Write("end update");
            });
        }

        public void Dispose()
        {
            if (this.pop3 != null)
            {
                if(this.updateCancelToken !=null)
                {
                    this.updateCancelToken.Cancel();
                    this.updateCancelToken = null;
                }
                if (this.pop3.IsConnected)
                {
                    if(this.pop3.IsAuthenticated)
                    {
                        
                    }
                    this.pop3.Disconnect(true);
                }
                this.pop3.Dispose();
            }

            this.OnAuthenticated = null;
            this.OnNewMessage = null;
        }
    }
}
