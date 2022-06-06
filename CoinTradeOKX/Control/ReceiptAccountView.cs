using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTradeOKX.Okex.Entity;
using Common.Classes;

namespace CoinTradeOKX.Control
{
    public partial class ReceiptAccountView : UserControl
    {
        public ReceiptAccountView()
        {
            InitializeComponent();
        }

        public void SetAccount(ReceiptAccountItem account)
        {
            this.lblName.Text = account.AccountName;
            this.lblBank.Text = account.BankName;
            this.lblAccount.Text = account.AccountNo;
            
            switch(account.ApplyType)
            {
                case AccountApplyType.All:
                    this.lblApplyType.Text = "收付款";
                    break;
                case AccountApplyType.Payment:
                    this.lblApplyType.Text = "付款";
                    break;
                case AccountApplyType.Receipt:
                    this.lblApplyType.Text = "收款";
                    break;
            }
        }
    }
}
