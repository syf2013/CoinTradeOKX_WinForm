using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeOKX.Okex.Entity
{

    //获取币币市场以usdt计价的币对
    public class InstrumentTableVsUSDT
    {

        Dictionary<string, Instrument> InstrumentTable = new Dictionary<string, Instrument>();
        Dictionary<string, Instrument> list = new Dictionary<string, Instrument>();
        InstrumentTableVsUSDT() { }

        public Dictionary<string,Instrument> GetAllInstrument()
        {
            return this.list;
        }

        public Instrument GetInstrument(string currency)
        {
            currency = currency.ToUpper();

            return this.list.ContainsKey(currency) ? this.list[currency] : null;
        }


        private static InstrumentTableVsUSDT _instance = null;

        private void ParseFromJsonV5(JArray list)
        {
            foreach (var item in list)
            {
                Instrument instrument = new Instrument();
                instrument.ParseFromJsonV5(item);

                if (string.Compare(item["quoteCcy"].Value<string>(), Config.Instance.Anchor, true) == 0)
                {
                    this.list[instrument.BaseCurrency.ToUpper()] = instrument;
                }

                this.InstrumentTable[instrument.InstrumentId] = instrument;
            }
        }

        public bool HasInstrument(string instrumentId)
        {
            return this.InstrumentTable.ContainsKey(instrumentId);
        }

        public bool HasInstrument(string currency1, string currency2)
        {
            string instrumentId = string.Format("{0}-{1}",currency1.ToUpper(),currency2.ToUpper());

            return HasInstrument(instrumentId);
        }

        public Task<string> UpdateInstrument()
        {
            return Task.Run(() =>
            {
                var api = new Okex_Rest_Api_InstrumentsV5();

                var instruments = api.execSync();

                string error = "";

                if (instruments["code"].Value<int>() == 0)
                {
                    ParseFromJsonV5(instruments["data"] as JArray);
                }
                else
                {
                    error = instruments["msg"].Value<string>();
                }

                return error;
            });
        }

        public static InstrumentTableVsUSDT Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new InstrumentTableVsUSDT();

                }

                return _instance;
            }
        }
    }
}
