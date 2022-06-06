using CoinTradeOKX.Monitor;
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
    public partial class WinBuyUSDX : Form
    {
        public WinBuyUSDX()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var currency = USDXMarket.Instance.Currency;
            decimal money = this.nupAmountTotal.Value;
            decimal price = this.nupAmountTotal.Value;
            decimal minAmount = this.nudMinAmount.Value;
            decimal amount = Math.Floor(money / price);

            minAmount = Math.Min(money, minAmount);


            OTCOrderManager.Instance.OTCPlaceBuyOrder(currency, amount, PriceTypeEnum.Fixed, price, 0, minAmount);
        }
    }
}
