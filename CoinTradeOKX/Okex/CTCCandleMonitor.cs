using CoinTradeOKX.Monitor;
using Common.Classes;
using Common.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex
{
    //class CTCCandleMonitor
    //{
    //CTCCandleRestApi
    //}


    [MonitorName(Name = "K线数据")]
    public class CTCCandleMonitor : RESTMonitor,ICandleProvider
    {
        public string Currency { get; private set; }

        private bool skipOnce = false;

        List<Candle> candles = new List<Candle>(200);

        CandleGranularity granularity = CandleGranularity.M1;

        private static string Granularity2V5String(CandleGranularity granularity)
        {
            switch(granularity)
            {
                case CandleGranularity.M1:
                    return "1m";
                case CandleGranularity.M3:
                    return "3m";
                case CandleGranularity.M5:
                    return "5m";
                case CandleGranularity.M15:
                    return "15m";
                case CandleGranularity.M30:
                    return "30m";
                case CandleGranularity.H1:
                    return "1H";
                case CandleGranularity.H4:
                    return "4H";
                case CandleGranularity.H6:
                    return "6H";
                case CandleGranularity.H12:
                    return "12H";
                case CandleGranularity.D1:
                    return "1D";
                case CandleGranularity.Week1:
                    return "1W";
                case CandleGranularity.Month1:
                    return "1M";
                case CandleGranularity.Y1:
                    return "1Y";
                default:
                    return "1m";
            }
        }

        public override string CustomName
        {
            get
            {
                return this.granularity.ToString();
            }
        }

        public CandleGranularity Granularity
        {
            get { return granularity; }
            set
            {
                lock (this.candles)
                {
                    this.CandlesToPool();
                }

                granularity = value;
                this.Interval = Convert.ToUInt32((Convert.ToUInt32(granularity) * 1000) / 3.0f);

#if !OKEX_API_V5
                var api = this.api as CTCCandleRestApi;
                api.Granularity = value;
#else
                var api = this.api as CTCCandleRestApiV5;
                api.Granularity = Granularity2V5String(value);
#endif

                this.skipOnce = true;
            }
        }

        public void UpdateLastPrice(decimal price, DateTime time)
        {
            lock(this.candles)
            {
                var last = this.candles.Count > 0 ? this.candles[0] : null;

                if(last != null)
                {
                    TimeSpan ts = time - last.Time;

                    if(ts.TotalSeconds > (uint)this.granularity)
                    {
                        Candle candle = Pool<Candle>.GetPool().Get();
                        candle.Open = candle.High = candle.Low = candle.Close = price;

                        int y, M, d, h, m;

                        y = time.Year;
                        M = time.Month;
                        d = time.Day;
                        h = time.Hour;
                        m = time.Minute;

                        switch (this.granularity)
                        {
                            case CandleGranularity.M1:
                                break;
                            case CandleGranularity.M3:
                                m -= m % 3;
                                break;
                            case CandleGranularity.M5:
                                m -= m % 5;
                                break;
                            case CandleGranularity.M15:
                                m -= m % 15;
                                break;
                            case CandleGranularity.M30:
                                m -= m % 30;
                                break;
                            case CandleGranularity.H1:
                                m = 0;
                                break;
                            case CandleGranularity.H4:
                                m = 0;
                                h -= h % 4;
                                break;
                            case CandleGranularity.H6:
                                m = 0;
                                h -= h % 6;
                                break;
                            case CandleGranularity.H12:
                                m = 0;
                                h -= h % 12;
                                break;
                            case CandleGranularity.D1:
                                h = 0;
                                m = 0;
                                break;
                            case CandleGranularity.Week1:
                                DateTime newTime = time;
                                newTime = newTime.AddDays(-(int)newTime.DayOfWeek);
                                y = newTime.Year;
                                M = newTime.Month;
                                d = newTime.Day;
                                h = 0;
                                m = 0;
                                break;
                            case CandleGranularity.Month1:
                                d = 1;h = 0;m = 0;
                                break;
                            case CandleGranularity.Y1:
                                M = 0; d = 1; h = 0; m = 0;
                                break;
                        }

                        candle.Time = new DateTime(y, M, d, h, m, 0);

                        this.candles.Insert(0, candle);
                    }
                    else
                    {
                        last.Close = price;
                        last.High = Math.Max(price, last.High);
                        last.Low = Math.Min(price, last.Low);
                    }
                }
            }
        }

        public CTCCandleMonitor(string currency, string currency2, CandleGranularity granularity)
#if !OKEX_API_V5 
            : base(new CTCCandleRestApi(currency, currency2, granularity), 800)
#else
            : base(new CTCCandleRestApiV5(currency, currency2, Granularity2V5String(granularity)), 800)
#endif
        {
            this.Granularity = granularity;
            this.Currency = currency.ToUpper();
            this.skipOnce = false;
            base.api.OnData += Api_OnData;
        }

        private Pool<Candle> CandlesToPool()
        {
            Pool<Candle> pool = Pool<Candle>.GetPool();

            pool.Put(this.candles);
            this.candles.Clear();

            return pool;
        }

        public void EachCandle(Action<Candle> callback)
        {
            lock (candles)
            {
                foreach (var c in candles)
                {
                    callback(c);
                }
            }
        }

#if OKEX_API_V5
        private void Api_OnData(JToken obj)
        {
            if (obj.Value<int>("code") == 0)
            {
                JArray data = obj["data"] as JArray;

                lock (this.candles)
                {
                    if (this.skipOnce) //标记修改粒度后跳过一次回调， 避免收到脏数据
                    {
                        this.skipOnce = false;
                        return;
                    }


                    Candle firstCandle = null;

                    if (this.candles.Count > 0)
                    {
                        firstCandle = this.candles[0];
                        this.candles.Remove(firstCandle);
                    }

                    Pool<Candle> pool = this.CandlesToPool();
                    foreach (JToken jt in data)
                    {
                        Candle candle = pool.Get();
                        var arr = jt as JToken;

                        candle.Time = DateUtil.TimestampMSToDateTime(arr[0].Value<long>());
                        candle.Open = decimal.Parse(arr[1].Value<string>());
                        candle.High = decimal.Parse(arr[2].Value<string>());
                        candle.Low = decimal.Parse(arr[3].Value<string>());
                        candle.Close = decimal.Parse(arr[4].Value<string>());
                        candle.Volume = decimal.Parse(arr[5].Value<string>());

                        this.candles.Add(candle);
                    }

                    if (firstCandle != null)
                    {
                        Candle first = null;

                        if (this.candles.Count > 0)
                        {
                            first = this.candles[0];
                            if (firstCandle.Time > first.Time)
                            {
                                this.candles.Insert(0, firstCandle);
                                firstCandle = null;
                            }
                        }

                        if (firstCandle != null)
                        {
                            pool.Put(firstCandle);
                        }
                    }
                }

                this.Feed();
                //this.OnData?.Invoke(this);
            }
        }
#else
        private void Api_OnData(JToken obj)
        {
            if (obj.Value<int>("code") == 0)
            {
                JArray data = obj["data"] as JArray;

                lock (this.candles)
                {
                    if (this.skipOnce) //标记修改粒度后跳过一次回调， 避免收到脏数据
                    {
                        this.skipOnce = false;
                        return;
                    }


                    Candle firstCandle = null ;

                    if(this.candles.Count > 0)
                    {
                        firstCandle = this.candles[0];
                        this.candles.Remove(firstCandle);
                    }

                    Pool<Candle> pool = this.CandlesToPool();
                    foreach (JToken jt in data)
                    {
                        Candle candle = pool.Get();
                        var arr = jt as JToken;

                        candle.Time = DateTime.Parse(arr[0].Value<string>());
                        candle.Open = decimal.Parse(arr[1].Value<string>());
                        candle.High = decimal.Parse(arr[2].Value<string>());
                        candle.Low = decimal.Parse(arr[3].Value<string>());
                        candle.Close = decimal.Parse(arr[4].Value<string>());
                        candle.Volume = decimal.Parse(arr[5].Value<string>());

                        this.candles.Add(candle);
                    }

                    if(firstCandle != null)
                    {
                        Candle first = null;

                        if (this.candles.Count > 0)
                        {
                            first = this.candles[0];
                           if(firstCandle.Time > first.Time)
                            {
                                this.candles.Insert(0, firstCandle);
                                firstCandle = null;
                            }
                        }

                        if(firstCandle != null)
                        {
                            pool.Put(firstCandle);
                        }
                    }
                }

                this.Feed();
                //this.OnData?.Invoke(this);
            }
        }
#endif


    }
}
