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
    public partial class WinConfig : Form
    {

        private readonly string WSSAddress = "wss://ws.okx.com:8443/ws/v5/public";
        private MarketTypeEnum originalMarket = MarketTypeEnum.OTCMarket;
        private bool RestartTipsShow = false;
        public WinConfig()
        {
            InitializeComponent();

            config = Config.Instance;
        }

        private bool IsUpdated = false;
        public bool ForceUpdate
        {
            get;set;
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
            try
            {
                OTCAccount otcAccount = config.Account;
                ApiKey api = config.ApiInfo;

                config.Anchor = this.cbAnchor.Text;
                config.AnchorSize = decimal.Parse(this.txtAnchroSize.Text);
                config.AnchorOrder = uint.Parse(this.txtAnchorOrder.Text);

                otcAccount.MarketType = this.rdoOtc.Checked ? MarketTypeEnum.OTCMarket : MarketTypeEnum.CTCMarket;
                otcAccount.MarketType = this.rdoCtc.Checked ? MarketTypeEnum.CTCMarket : MarketTypeEnum.OTCMarket;

                otcAccount.RealName = this.txtRealname.Text.Trim();

                string text;
                decimal deposit = 0;
                if(decimal.TryParse(txtDeposit.Text, out deposit))
                {
                    otcAccount.Deposit = deposit;
                }

                if (otcAccount.MarketType == MarketTypeEnum.OTCMarket)
                {
                    text = this.txtReleasePwd.Text.Trim();

                    if (text.Length < 8)
                        throw new Exception("放币密码不得少于8位");

                    if (text != Config.MaskString(otcAccount.GetReleasePassword()))
                        otcAccount.SetReleasePassword(text);
                }

                text = this.txtLoginName.Text.Trim();

                //if (text != Config.MaskString(account.GetLoginName()))
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

                platform.OrderCountDownMS = uint.Parse(this.txtOTCOrderCountDown.Text);
                platform.ReorderCountDownMS = uint.Parse(this.txtReorderCountDown.Text);
                platform.ApiTimeout = uint.Parse(this.txtApiTimeout.Text);
                platform.SellOrderUserType = ComboBoxToUserType(this.cmbSellUser);
                platform.BuyOrderUserType = ComboBoxToUserType(this.cmbBuyUser);
                platform.SellPaidAddToCash = chkAddCash.Checked;
                platform.MaxUnpaidBuyOrder = int.Parse(this.txtMaxUnpaidBuyOrder.Text);
                platform.DaySellAmountLimit = Convert.ToUInt32( this.nudDaySellAmountLimit.Value);

                if(platform.MaxUnpaidBuyOrder < 1)
                    throw new Exception("最大未处理买单数不能小于1");

                if (platform.ApiTimeout < 3)
                    throw new Exception("API 超时不能小于3秒");

                string currenciesString = this.txtCurrencies.Text.Trim().ToUpper();

                currenciesString = currenciesString.Replace("；", ";").Replace(" ","");

                List<string> currencies = new List<string>(currenciesString.Split(',', ';'));

                for(int i = currencies.Count - 1;i>=0; i--)
                {
                    string curruncy = currencies[i];
                    if (string.IsNullOrEmpty(curruncy))
                    {
                        currencies.RemoveAt(i);
                    }
                    else
                    {
                        string instrumentId = string.Format("{0}-{1}", curruncy.ToUpper(), config.Anchor.ToUpper());
                        if (!InstrumentTableVsUSDT.Instance.HasInstrument(instrumentId))
                        {
                            throw new Exception("不存在交易对" + instrumentId);
                        }
                    }
                }

                if (currencies.Count < 1)
                    throw new Exception("请填写至少一种可交易币种");

                if (currencies.Contains(config.Anchor))
                    throw new Exception("锚定币种不能出现在币种列表");

                if(config.AnchorOrder < 1)
                    throw new Exception("锚定币挂单数不能少于1");

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
            catch(Exception ex)
            {
                WinMessage.Show(MessageType.Error, "保存配置失败" + ex.Message);
            }

            AccountManager.UpdateAccount(apiOwner);
            Config.UpdateConfig(config);


            IsUpdated = true;
            

            this.DialogResult = DialogResult.OK;
            this.Close();

            EventCenter.Instance.Emit(EventNames.ConfigChanged, null);

            if((originalMarket == MarketTypeEnum.CTCMarket || originalMarket == MarketTypeEnum.OTCMarket) && originalMarket != config.Account.MarketType)
            {
                Application.Exit();
            }
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

            this.txtRealname.Text = account.RealName;
            this.txtLoginName.Text = account.GetLoginName();
            this.txtDeposit.Text = account.Deposit.ToString();
           
 
            this.txtReleasePwd.Text = Config.MaskString(account.GetReleasePassword());

            this.txtApiKey.Text = Config.MaskString(api.GetKey());
            this.txtApiPassphrase.Text = Config.MaskString( api.GetPassphrase());
            this.txtApiSecretKey.Text = Config.MaskString(api.GetSecretKey());

            this.txtWebSocket.Text = !string.IsNullOrEmpty(api.ApiAddress) ? api.ApiAddress : WSSAddress;

            this.cbAnchor.Text = config.Anchor;
            this.txtAnchroSize.Text = config.AnchorSize.ToString();
            this.txtAnchorOrder.Text = config.AnchorOrder.ToString();
            this.chkAddCash.Checked = config.PlatformConfig.SellPaidAddToCash;
            this.nudDaySellAmountLimit.Value = config.PlatformConfig.DaySellAmountLimit;
            this.txtMaxUnpaidBuyOrder.Text = config.PlatformConfig.MaxUnpaidBuyOrder.ToString();
            //this.txtExchangeRate.Text = config.ExchangeRate.ToString();

            this.rdoOtc.Checked = config.Account.MarketType == MarketTypeEnum.OTCMarket;
            this.rdoCtc.Checked = config.Account.MarketType == MarketTypeEnum.CTCMarket;
            this.rdoSimulated.Checked = config.ApiInfo.IsSimulated;


            this.txtOTCOrderCountDown.Text = config.PlatformConfig.OrderCountDownMS.ToString();
            this.txtReorderCountDown.Text = config.PlatformConfig.ReorderCountDownMS.ToString();
            this.txtApiTimeout.Text = config.PlatformConfig.ApiTimeout.ToString();
            this.txtCurrencies.Text = string.Join(";",config.PlatformConfig.Currencies);

            SelectUserTypeCombobox(config.PlatformConfig.SellOrderUserType, cmbSellUser);
            SelectUserTypeCombobox(config.PlatformConfig.BuyOrderUserType, cmbBuyUser);

            
            RequireTexts.Add(txtRealname);
            RequireTexts.Add(txtLoginName);
            RequireTexts.Add(txtCurrencies);

            RequireTexts.Add(txtApiKey);
            RequireTexts.Add(txtApiPassphrase);
            RequireTexts.Add(txtApiSecretKey);
            RequireTexts.Add(txtWebSocket);

            RequireTexts.Add(txtAnchroSize);
            RequireTexts.Add(txtOTCOrderCountDown);
            RequireTexts.Add(txtReorderCountDown);
            RequireTexts.Add(txtApiTimeout);
            
            RequireTexts.Add(txtAnchorOrder);
            RequireTexts.Add(txtMaxUnpaidBuyOrder);


            //this.CheckRequireTextbox();

            UpdateInstrument();
        }

        private async void UpdateInstrument()
        {
            await InstrumentTableVsUSDT.Instance.UpdateInstrument();
        }
        private bool CheckRequireTextbox()
        {
            bool hasEmpty = false;
            foreach (var t in RequireTexts)
            {
                if (string.IsNullOrEmpty(t.Text))
                {
                    t.BackColor = Color.FromArgb(255, 190, 190);
                    hasEmpty = true;
                }
                else
                {
                    t.BackColor = Color.White;
                }
            }

            return !hasEmpty;
        }

        private void cbAnchor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rdoOtc_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox3.Enabled = this.rdoOtc.Checked;
            this.pnlOtcUsdx.Visible = this.rdoOtc.Checked;
        }

        private void rdoCtc_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox3.Enabled = this.rdoOtc.Checked;
            this.pnlOtcUsdx.Visible = this.rdoOtc.Checked;
        }

        private void CheckMarketChange()
        {
            if (RestartTipsShow)
                return;

            if(originalMarket == MarketTypeEnum.None)
            {
                return;
            }

            MarketTypeEnum market = rdoCtc.Checked ? MarketTypeEnum.CTCMarket : MarketTypeEnum.OTCMarket;
            
            if(market != originalMarket)
            {
                RestartTipsShow = true;

                MessageBox.Show("修改交易模式后将需要重新启动");
            }
        }

        private void rdoCtc_Click(object sender, EventArgs e)
        {
            this.CheckMarketChange();
        }

        private void WinConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ForceUpdate && !IsUpdated)
            {
                WinMessage.Show(MessageType.Error, "请设置完整配置");
                e.Cancel = true;
            }
        }
    }
}
