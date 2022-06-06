using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CoinTradeOKX
{
     public enum LogType
    {
        Info = 1,
        Error = 2,
        Debug = 3
    }

    public class Log
    {
        public LogType Type;
        public string Content;
        public DateTime Time;

        public Log(LogType type,string content)
        {
            Type = type;
            Content = content;
            Time = DateTime.Now;
        }

        public override string ToString()
        {
            string tp = "";
            switch(Type)
            {
                case LogType.Error:
                    tp = "Error";
                    break;
                case LogType.Info:
                    tp = " Info";
                    break;
                case LogType.Debug:
                    tp = "Debug";
                    break;
            }

            return string.Format("[{0}] {1}: {2}", Time.ToString("HH:mm:ss"), tp, Content);
        }
    }
    public class Logger
    {
        StreamWriter writer = null;
        static Logger _instance = null;
        Queue<Log> queue = new Queue<Log>();
        public int Capacity = 1000;

        public Action<Log> NewLog = null;

        string filePath = "";

        Thread write_thread = null;
        public string FilePath
        {
            get { return filePath; }
            set
            {
                if (filePath == value)
                    return;

                if(writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    writer = null;
                }

                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        writer = new StreamWriter(value, true);
                        writer.AutoFlush = true;
                    }
                    catch(Exception ex)
                    {
                        Log(LogType.Error, string.Format("无法创建或打开日志文件{0}: {1}", value, ex.Message));
                    }

                    if(writer !=null)
                    {
                        this.write_thread = new Thread(this.waitForWrite);
                        this.write_thread.Start();
                    }
                }

                filePath = value;
            }
        }

        private const string ExitCommand = "__do_exit_log__";

        private Queue<Log> writeQueue = new Queue<Log>();
        object write_locker = new object();
        const int max_write_queue_size = 1000;
        Semaphore sem = new  Semaphore(0, max_write_queue_size);

        private void waitForWrite()
        {
            while (sem.WaitOne())
            {
                Log log = null;
                lock (write_locker)
                {
                    log = this.writeQueue.Dequeue();
                    if (log.Content == ExitCommand)
                    {
                        writer.Close();
                        writer.Dispose();
                        this.writer = null;
                        break;
                    }
                }

                writer.WriteLine(log.ToString());
            }
        }

        private void WriteToFile(Log l)
        {
            lock(write_locker)
            {
                if (writeQueue.Count >= max_write_queue_size)
                    return;
                writeQueue.Enqueue(l);
            }
            
            sem.Release();
        }

        static object queueLocker = new object();


        public void Exit()
        {
            this.LogError(ExitCommand);
            sem.Release();
        }

        public void LogError(string log)
        {
            this.Log(LogType.Error, log);
        }

        public void LogException(Exception ex)
        {
            //TODO
            this.LogError( string.Format("{0}\r\n{1}" , ex.StackTrace, ex.Message));
        }

        public void LogInfo(string log)
        {
            this.Log(LogType.Info, log);
        }

        public void LogDebug(string content)
        {
            this.Log(LogType.Debug,  content);
        }

        public void Log(LogType type, string content )
        {
            Log l = new Log(type, content);

            if ((type == LogType.Error || type == LogType.Debug) && !string.IsNullOrEmpty(this.FilePath))
            {
                this.WriteToFile(l);
            }

            lock (queueLocker)
            {
                while (queue.Count >= Capacity)
                    queue.Dequeue();
                queue.Enqueue(l);
            }
            NewLog?.Invoke(l);
        }


        public string LogContent(bool errorOnly)
        {
            StringBuilder sb = new StringBuilder();
            lock (queueLocker)
            {
                foreach (var l in queue)
                {
                    if (errorOnly && l.Type != LogType.Error)
                        continue;

                    sb.AppendLine(l.ToString());
                }
            }

            return sb.ToString();
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }

                return _instance;
            }
        }


    }
}
