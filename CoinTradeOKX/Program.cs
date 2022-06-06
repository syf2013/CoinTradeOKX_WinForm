using Common.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // var root = new WinRoot();
            Initialize();
            //WindowManager.Instance.OpenWindow<CefBrowser>();
            //WindowManager.Instance.OpenWindow<WinMain>();
            //WindowManager.Instance.OpenWindow<WinConfig>();
            WindowManager.Instance.OpenWindow<WinStartup>();
            //WindowManager.Instance.OpenWindow<WinTimeLine>();
            Application.Run();
        }

        // 获取方法
        public static IList<string> GetDescriptionByEnum(Type enumType)
        {
            var fileds = enumType.GetFields();

            List<string> names = new List<string>();

            foreach (var f in fileds)
            {
               
                object[] objs = f.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
                if (objs.Length > 0)    //当描述属性没有时，直接返回名称
                {
                    DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                    names.Add(descriptionAttribute.Description);
                }
            }

            return names;
        }

        /**
         * 初始化
         */
        private static void Initialize ()
        {
            ServicePointManager.DefaultConnectionLimit = 1024;//设置最大并发连接数
        }
    }
}
