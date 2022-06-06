using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex.Behavior;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX.Okex.Behavior
{
    public static class BehaviorConfig
    {
        /**
        * 保存配置到文件
        */
        public static string SaveBehaviorConfig(string currency, BehaviorBase behavior)
        {

            var type = behavior.GetType();

            string configPath = GetConfigPath(currency, behavior);

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            JObject json = new JObject();

            foreach (var p in properties)
            {
                BehaviorParameter attribute = p.GetCustomAttribute<BehaviorParameter>();

                if (attribute == null)
                    continue;

                Type valueType = p.PropertyType;
                object value = p.GetValue(behavior);
                if (valueType == typeof(string))
                {
                    json[p.Name] = value.ToString();
                }
                else if (valueType == typeof(bool))
                {
                    json[p.Name] = (bool)value;
                }
                else if (valueType == typeof(DateTime))
                {
                    json[p.Name] = DateUtil.GetTimestampSec((DateTime)value);
                }
                else if(valueType.BaseType == typeof(Enum))
                {
                    json[p.Name] = value.ToString();
                }
                else
                {
                    double number = 0;

                    number = double.Parse(value.ToString());

                    json[p.Name] = number;
                }
            }

            try
            {
                File.WriteAllText(configPath, json.ToString());
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        private static string GetConfigPath(string currency, BehaviorBase behavior)
        {
            string startPath = Application.StartupPath;
            var type = behavior.GetType();
            var dir = Path.Combine(startPath,"BehaviorConfigs", currency.ToLower());

            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return Path.Combine(dir,  string.Format("{0}.json", type.Name));
        }

        /**
        * 加载配置
        */
        public static void LoadBehaviorConfig(string currency, BehaviorBase behavior)
        {
            string configPath = GetConfigPath(currency, behavior);

            if(File.Exists(configPath))
            {
                string text = File.ReadAllText(configPath);

                JObject json = JObject.Parse(text);

                var type = behavior.GetType();

                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var p in properties)
                {
                    BehaviorParameter attribute = p.GetCustomAttribute<BehaviorParameter>();

                    if (attribute == null)
                        continue;

                    if (!json.ContainsKey(p.Name))
                        continue;

                    string name = p.Name;

                    Type valueType = p.PropertyType;
                    JToken value = json[name];


                    if (valueType == typeof(bool))
                    {
                        p.SetValue(behavior, value.Value<bool>());
                    }
                    else if(valueType == typeof(DateTime))
                    {
                        long timestamp = value.Value<long>();
                        p.SetValue(behavior, DateUtil.TimestampSecToDateTime(timestamp));
                    }
                    else if (valueType == typeof(string))
                    {
                        p.SetValue(behavior, value.Value<string>());
                    }
                    else if (valueType == typeof(int))
                    {
                        p.SetValue(behavior,value.Value<int>());
                    }
                    else if (valueType == typeof(float))
                    {
                        p.SetValue(behavior, value.Value<float>());
                    }
                    else if (valueType == typeof(decimal))
                    {
                        p.SetValue(behavior, value.Value<decimal>());
                    }
                    else if (valueType == typeof(uint))
                    {
                        p.SetValue(behavior, value.Value<uint>());
                    }
                    else if(valueType.BaseType == typeof(Enum))
                    {
                        p.SetValue(behavior, Enum.Parse(valueType, value.ToString()));// uint.Parse(value.ToString()));
                    }
                    else if (valueType == typeof(long))
                    {
                        p.SetValue(behavior, value.Value<long>());
                    }
                    else
                    {
                        p.SetValue(behavior,  value.Value<double>());
                    }
                }
            }
        }

        public static void LoadBehaviorListConfig(string currency, IEnumerable<BehaviorBase> list)
        {
            foreach(var t in list)
            {
                LoadBehaviorConfig(currency, t);
            }
        }
    }
}
