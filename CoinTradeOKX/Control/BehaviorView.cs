using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTradeOKX.Okex.Behavior;
using CoinTradeOKX.Monitor;

namespace CoinTradeOKX.Control
{

    public partial class BehaviorView : UserControl
    {
        private BehaviorBase _behavior = null;
        private string currency = "";
        public BehaviorBase Behavior
        {
            get
            {
                return this._behavior;
            }
            private  set
            {
                this._behavior = value;

                this.chkEnable.Enabled = value != null;
                string name = "";
                if (value != null)
                {
                    var type = value.GetType();
                    foreach (var attr in type.GetCustomAttributes(false))
                    {
                        if (attr is BehaviorNameAttribute MonitorNameAttribute)
                        {
                           name = (attr as BehaviorNameAttribute).Name;
                            break;
                        }
                    }
                    chkEnable.ForeColor =  Color.Black;
                    chkEnable.Checked = value.Enable;
                }
                else
                {
                    chkEnable.Checked = false;
                    chkEnable.ForeColor = Color.Gray;
                }

                
                this.chkEnable.Text = name;
            }
        }

        public BehaviorView()
        {
            InitializeComponent();

            this.chkEnable.Enabled = false;
        }

        public void SetBehavior(string currency, BehaviorBase behavior)
        {
            this.currency = currency;
            this.Behavior = behavior;
        }


        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            this.Behavior.Enable = chkEnable.Checked;
            chkEnable.ForeColor = chkEnable.Checked ? Color.Black : Color.Gray;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this._behavior != null)
            {
                this.lblExcuting.ForeColor = this._behavior.Executing ? Color.Red : Color.Black;
                this.lblMessage.Text = this._behavior.Message;
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            WinBehaviorParam win = new WinBehaviorParam();
            win.Show();
            win.SetBehavior(this.currency, this.Behavior);
        }


        public void EnableBehavior()
        {
            this.Behavior.Enable = true;
            this.chkEnable.Checked = true;
        }

        public void DisableBehavior()
        {
            this.Behavior.Enable = false;
            this.chkEnable.Checked = false;
        }
    }
}
