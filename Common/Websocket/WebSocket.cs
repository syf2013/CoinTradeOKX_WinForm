using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTradeOKX
{
   public  class WebSocket: IWebSocket
    {
        ClientWebSocket _socket;
        CancellationToken token = CancellationToken.None;

        public event Action OnOpen = null;
        public event Action<string> OnError = null;
        public event Action OnClose = null;
        public event Action<byte[],int> OnMessage = null;

        private bool sending = false;
        private bool Closing = false;

        private byte[] receive_array = new byte[2048 * 1024];
        ArraySegment<byte> buffer_receive;

        private Queue<ArraySegment<byte>> SendQueue = new Queue<ArraySegment<byte>>();

        public bool IsOpen
        {
            get
            {
                return this._socket != null && !Closing  && this._socket.State == WebSocketState.Open;
            }
        }

        public  WebSocket()
        {
            this.buffer_receive = new ArraySegment<byte>(receive_array);
            _socket = new ClientWebSocket();
        }

        async public void ConnectAsync(string address)
        {

            lock (SendQueue)
            {
                SendQueue.Clear();
            }
            Closing = false;
            try
            {
                await _socket.ConnectAsync(new Uri(address), token);
            }
            catch(Exception ex)
            {
                OnError?.Invoke(ex.Message);
                return;
            }
            if (_socket.State == WebSocketState.Open)
            {
                this.OnOpen?.Invoke();
            }
            else if (_socket.State == WebSocketState.Closed)
            {
                this.OnError?.Invoke("connect failed");
                return;
            }

            receiveSocketData(_socket, buffer_receive, token);
        }

        async public void receiveSocketData(ClientWebSocket socket, ArraySegment<byte> buffer, CancellationToken token)
        {
            while (true)
            {
                try
                {
                    var result = await socket.ReceiveAsync(buffer, token);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        this.CloseAsync();
                        break;
                    }

                    int count = result.Count;
                    byte[] datas = buffer.ToArray();
                    OnMessage?.Invoke(datas, count);
                }
                catch (Exception ex)
                {
                    if (socket.State == WebSocketState.CloseReceived || socket.State == WebSocketState.Closed)
                    {
                        OnClose?.Invoke();
                        break;
                    }

                    OnError?.Invoke(ex.Message);
                    break;
                }
            }
        }

        public async void CloseAsync()
        {
            if (this._socket != null)
            {
                Closing = true;
                if (_socket.State == WebSocketState.Open)
                {

                    //await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", token);
                    await _socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", token);
                }
            }
        }

         public void SendAsync(string datas)
        {
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(datas);
            this.SendAsync(bytes);
        }

        async public void SendAsync(byte[] datas)
        {
            if (!this.IsOpen)
                return;

            var buff = new ArraySegment<byte>(datas);

            if (sending)
            {
                lock (SendQueue)
                {
                    SendQueue.Enqueue(buff);
                }

                return;
            }

            sending = true;
            try
            {
                await _socket.SendAsync(buff, WebSocketMessageType.Text, true, token);
            }
            catch (Exception ex)
            {
                this.OnError?.Invoke(ex.Message);
            }

            sending = false;
            bool resend = false;
            lock (SendQueue)
            {
                if (SendQueue.Count > 0)
                {
                    buff = SendQueue.Dequeue();
                    resend = true;
                }
            }

            if(resend)
            {
                SendAsync(buff.Array);
            }
        }

        ~WebSocket()
        {
            _socket.Abort();
            _socket.Dispose();
        }
    }
}
