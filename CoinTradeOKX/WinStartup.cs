using CoinTradeOKX.Event;
using CoinTradeOKX.Manager;
using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Entity;
using Common;
using Common.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX
{
    public partial class WinStartup : Form
    {
        private string logFileName = "";
        public WinStartup()
        {
            InitializeComponent();
        }

        private DialogResult  OpenConfig()
        {
            var winConfig = new WinCTCConfig();// new WinConfig();

            //winConfig.ForceUpdate = true;
            DialogResult result = winConfig.ShowDialog();

            return result;
        }

        private void ExitApplication()
        {
            this.Close();
            Application.Exit();
        }

        private void BeginInitialize()
        {
            if (!Config.Instance.HasPassword() && !Config.Instance.TryLoadFromFile())
            {
                if(this.OpenConfig() != DialogResult.OK)
                {
                    ExitApplication();
                }
            }

            if(!Config.Instance.HasPassword())
            {
                MessageBox.Show("请设置登录密码");
                (new WinPassword()).ShowDialog();
            }
        }

        async private void Start()
        {
            this.pnlLogin.Visible = false;
            this.lblMessage.Text = "正在初始化......";
            this.lblMessage.ForeColor = Color.Black;
            this.btnRetry.Enabled = false;
            string error = string.Empty;

            var logFile = Path.Combine(Application.StartupPath, "log",this.logFileName);
            var logDir = Path.Combine(Application.StartupPath, "log");

           
            if (Directory.Exists(logDir) == false)
            {
                try
                {
                    Directory.CreateDirectory(logDir);
                }
                catch(Exception ex)
                {
                    error = string.Format("创建日志文件夹失败 {0}",ex.Message);
                    goto label_error;
                }
            }

            if (!File.Exists(logFile))
            {
                try
                {
                    using (var s = File.Create(logFile))
                    {
                        s.Close();
                    }
                }
                catch(Exception ex)
                {
                    error = string.Format("创建日志文件失败 {0}", ex.Message);
                    goto label_error;
                }
            }

            Logger.Instance.FilePath = logFile;


            //对时
            lblMessage.Text = "正在与服务器对时......";
            bool success = await this.CorrectingServerTime();

            if(!success)
            {
                error = "无法获取服务器时间";
                goto label_error;
            }

            lblMessage.Text = "正在获取币对信息......";

            do
            {
                lblMessage.Text = "正在获取币对信息......";
        
                var strError = await InstrumentTableVsUSDT.Instance.UpdateInstrument();

                if (!string.IsNullOrEmpty(strError))
                {
                    error = "无法获取交易币对详细信息 " + strError;
                    break;
                }
            } while (false);

            label_error:

            if (!string.IsNullOrEmpty(error))
            {
                this.lblMessage.Text = error;
                this.lblMessage.ForeColor = Color.Red;
                this.btnRetry.Enabled = true;
            }
            else
            {
                this.BeginInitialize();
                this.pnlLogin.Visible = true;
                this.pnlStart.Visible = false;
            }
        }

        private Task<bool> CorrectingServerTime()
        {
            return Task.Run<bool>(() =>
            {
                var timeApi = new TimeServiceApi();

                var json = timeApi.execSync();

                if (json.Value<int>("code") == 0)
                {
                    JArray data = json["data"] as JArray;

                    DateUtil.SetServerTimestamp(data[0].Value<long>("ts"));

                    return true;
                }

                return false;
            });
        }

        private void WinInitiate_Load(object sender, EventArgs e)
        {
            this.logFileName = string.Format("error_log_{0}.log", DateTime.Now.ToString("yyMMddHHmmss"));
            this.Start();
           // this.BeginInitialize();
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string pwd = this.txtPwd.Text;
            if (Config.Instance.ValidatePasswordAndLoad(pwd))
            {
                if (!AccountManager.UpdateAccount()) //API失效，被删除或者过期
                {
                    MessageBox.Show("API可能已经失效请更新API", "API登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (this.OpenConfig() != DialogResult.OK)
                    {
                        ExitApplication();

                        return;
                    }
                } 
                this.OpenMainWindow(null);
            }
            else
            {
                MessageBox.Show("密码错误");
            }
        }

        private void OpenMainWindow(object args)
        {
            WindowManager.Instance.OpenWindow<WinMain>();
            this.Close();
        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPwd_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void WinStartup_FormClosing(object sender, FormClosingEventArgs e)
        {
         
        }
    }
}
