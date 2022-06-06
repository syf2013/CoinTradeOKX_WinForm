using CoinTradeOKX.Event;
using CoinTradeOKX.Manager;
using CoinTradeOKX.Okex;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Common;
using Common.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX
{
    public partial class WinCTCConfig : Form
    {

        private readonly string WSSAddress = "wss://ws.okx.com:8443/ws/v5/public";
        private MarketTypeEnum originalMarket = MarketTypeEnum.OTCMarket;
        public WinCTCConfig()
        {
            InitializeComponent();

            config = Config.Instance;
        }


        private bool IsUpdated = false;
        public bool ForceUpdate
        {
            get; set;
        }



        private List<TextBox> RequireTexts = new List<TextBox>();
        private void button1_Click(object sender, EventArgs e)
        {
            if(!this.CheckRequireTextbox())
            {
                return;
            }

            ApiKey oldApi = Config.Instance.ApiInfo;

            var config = new Config();// Config.Instance;
            Account apiOwner = null;
            string text = "";

            try
            {
                config.Anchor = this.cbAnchor.Text;
                OTCAccount otcAccount = config.Account;
                ApiKey api = config.ApiInfo;

                otcAccount.MarketType = MarketTypeEnum.CTCMarket;

                text = this.txtLoginName.Text.Trim();

                otcAccount.SetLoginName(text);

                text = this.txtApiKey.Text.Trim();

                if (text.Length < 8)
                    throw new Exception("API Key不得少于8位");

                if (text != Config.MaskString(oldApi.GetKey()))
                    api.SetKey(text);
                else
                    api.SetKey(oldApi.Key);

                text = this.txtApiPassphrase.Text.Trim();

                if (text.Length < 6)
                    throw new Exception("API Passphrase不得少于6位");

                if (text != Config.MaskString(oldApi.GetPassphrase()))
                    api.SetPassphrase(text);
                else
                    api.SetPassphrase(oldApi.Passphrase);

                text = this.txtApiSecretKey.Text.Trim();
                if (text.Length < 8)
                    throw new Exception("API SecretKey不得少于8位");
                if (text != Config.MaskString(oldApi.GetSecretKey()))
                    api.SetSecretKey(text);
                else
                    api.SetSecretKey(oldApi.SecretKey);

                api.IsSimulated = rdoSimulated.Checked;

                api.ApiAddress = this.txtWebSocket.Text.Trim();

                Platform platform = config.PlatformConfig;

                string currenciesString = this.txtCurrencies.Text.Trim().ToUpper();

                currenciesString = currenciesString.Replace("；", ";").Replace(" ", "");

                List<string> currencies = new List<string>(currenciesString.Split(',', ';'));

                for (int i = currencies.Count - 1; i >= 0; i--)
                {
                    string currency = currencies[i];
                    if (string.IsNullOrEmpty(currency))
                    {
                        currencies.RemoveAt(i);
                    }
                    else
                    {
                        string instrumentId = string.Format("{0}-{1}", currency.ToUpper(), config.Anchor.ToUpper());
                        if (!InstrumentTableVsUSDT.Instance.HasInstrument(instrumentId))
                        {
                            throw new Exception("不存在交易对" + instrumentId);
                        }
                    }
                }

                if (currencies.Count < 1)
                    throw new Exception("请填写至少一种交易币种");

                if (currencies.Contains(config.Anchor))
                    throw new Exception("稳定币种不能出现在币种列表");

                platform.Currencies = currencies;

                apiOwner = AccountManager.GetAccountByApi(api);

                if (apiOwner == null)
                {
                    throw new Exception("API连接失败");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }


            try
            {
                config.SaveToFile();
            }
            catch (Exception ex)
            {
                WinMessage.Show(MessageType.Error, "保存配置失败" + ex.Message);
            }

            AccountManager.UpdateAccount(apiOwner);
            Config.UpdateConfig(config);

            IsUpdated = true;
            this.DialogResult = DialogResult.OK;
            this.Close();

            EventCenter.Instance.Emit(EventNames.ConfigChanged, null);
        }

        private string ComboBoxToUserType(ComboBox cb)
        {
            if (cb.SelectedIndex == 0)
                return UserType.All;

            if (cb.SelectedIndex == 1)
                return UserType.Certified;

            return UserType.Common;
        }

        private void SelectUserTypeCombobox(string userType,ComboBox cb)
        {
            if(userType == UserType.All)
            {
                cb.SelectedIndex = 0;
            }
            else if(userType == UserType.Certified)
            {
                cb.SelectedIndex = 1;
            }
            else if(userType == UserType.Common)
            {
                cb.SelectedIndex = 2;
            }
        }

        Config config = null;
        private void WinConfig_Load(object sender, EventArgs e)
        {
            config = Config.Instance;

            OTCAccount account = config.Account;
            ApiKey api = config.ApiInfo;

            this.originalMarket = account.MarketType;

            if (originalMarket == MarketTypeEnum.None)
                account.MarketType = MarketTypeEnum.OTCMarket;
            this.txtLoginName.Text = account.GetLoginName();
 
            this.txtApiKey.Text = Config.MaskString(api.GetKey());
            this.txtApiPassphrase.Text = Config.MaskString( api.GetPassphrase());
            this.txtApiSecretKey.Text = Config.MaskString(api.GetSecretKey());

            this.txtWebSocket.Text = !string.IsNullOrEmpty(api.ApiAddress) ? api.ApiAddress : WSSAddress;

            this.cbAnchor.Text = config.Anchor;

            this.rdoSimulated.Checked = config.ApiInfo.IsSimulated;

            this.txtCurrencies.Text = string.Join(";",config.PlatformConfig.Currencies);

            RequireTexts.Add(txtApiPassphrase);
            RequireTexts.Add(txtApiKey);
            RequireTexts.Add(txtApiSecretKey);
            RequireTexts.Add(txtLoginName);

            RequireTexts.Add(txtCurrencies);
            RequireTexts.Add(txtWebSocket);


            UpdateInstrument();
        }

        private async void UpdateInstrument()
        {
            await InstrumentTableVsUSDT.Instance.UpdateInstrument();
        }

        private bool CheckRequireTextbox()
        {
            bool hasEmepy = false;
            foreach (var t in RequireTexts)
            {
                if (string.IsNullOrEmpty(t.Text))
                {
                    t.BackColor = Color.FromArgb(255, 190, 190);

                    hasEmepy = true;
                }
                else
                {
                    t.BackColor = Color.White;
                }
            }

            return !hasEmepy;
        }

        private void cbAnchor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CheckMarketChange()
        {

        }

        private void rdoCtc_Click(object sender, EventArgs e)
        {
            this.CheckMarketChange();
        }

        private void WinCTCConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ForceUpdate && !IsUpdated)
            {
                WinMessage.Show(MessageType.Error, "请设置完整配置");
                e.Cancel = true;
            }
        }
    }
}
