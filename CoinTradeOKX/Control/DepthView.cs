using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTradeOKX.Okex.Const;
using CoinTradeOKX.Okex.Entity;
using Common.Classes;
using Common.Interface;

namespace CoinTradeOKX.Control
{
    public partial class DepthView : UserControl
    {
        private IDepthProvider provider = null;
        private int PriceDecimal = 2;
        private DepthItem items = new DepthItem();
        public DepthView()
        {
            InitializeComponent();
        }

        public void SetProvider(IDepthProvider provider)
        {
            this.provider = provider;
        }

        public void SetPriceDecimal(int priceDecimal)
        {
            this.PriceDecimal = priceDecimal;
        }
       



        private void ShowDeep(SideEnum side, Panel panel)
        {
            int index = 0;
            var controls = panel.Controls;
            
            this.provider.EachDeep(side, (deep) => {
                DepthItem v = null;

                if (controls.Count > index)
                {
                    v = controls[index] as DepthItem;
                    v.Visible = true;
                }
                else
                {
                    v = new DepthItem();
                    controls.Add(v);
                }
                v.PriceDecimal = this.PriceDecimal;
                v.SetData(deep.Price, deep.Total, (int)deep.Orders, side);
                index++;
            });

            for (var i = index; i < controls.Count; i++)
            {
                controls[i].Visible = false;
            }

            this.flpSell.VerticalScroll.Value = this.flpSell.VerticalScroll.Maximum;
        }

        private void UpdateDeep()
        {
            if(this.provider != null)
            {
                this.ShowDeep(SideEnum.Sell, this.flpSell);
                this.ShowDeep(SideEnum.Buy, this.flpBuy);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this.provider != null)
                this.UpdateDeep();
        }
    }
}
