using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Classes;

namespace CoinTradeOKX.Control
{
    public partial class TransferView : UserControl
    {
        public TransferRecord Record
        {
            get;private set;
        }

        public TransferView()
        {
            InitializeComponent();
        }

        public void SetRecord(TransferRecord record)
        {
            this.lblName.Text = record.Name;
            this.lblMoney.Text = record.Amount.ToString("0.00");
            this.lblTime.Text = record.LocalTime.ToString("yyyy-MM-dd HH:mm:ss");

            this.Record = record;
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            this.Parent = null;
        }
    }
}
