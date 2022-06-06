using CoinTradeOKX.Control;
using CoinTradeOKX.Monitor;
using CoinTradeOKX.Okex.Behavior;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX
{
    public partial class WinBehaviorParam : Form
    {
        public event Action OnSave;


        private string currency = "";
        private BehaviorBase behavior = null;

        private List<ParamView> Views = new List<ParamView>();

        public WinBehaviorParam()
        {
            InitializeComponent();
        }


        public void SetBehavior(string currency, BehaviorBase behavior)
        {
            this.currency = currency;
            this.behavior = behavior;
            var type = behavior.GetType();
            var attr = type.GetCustomAttributes(typeof(BehaviorNameAttribute), false);

            if(attr.Length > 0)
            {
                var behaviorAttr = attr[0] as BehaviorNameAttribute;
                this.Text = currency.ToUpper() + behaviorAttr.Name + "参数设置";
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(var p in properties)
            {
                var attrParam = p.GetCustomAttribute(typeof(BehaviorParameter)) as BehaviorParameter;

                if (attrParam == null)
                    continue;

                var control = new ParamView();
                control.SetProperty(behavior, p);
                control.SetOnChangedCallback(this.OnParamChanged);
                this.flowLayoutPanel1.Controls.Add(control);
                this.Views.Add(control);
            }

            foreach(ParamView v in this.Views)
            {
                this.OnParamChanged(v);
            }
        }

        private void OnParamChanged(ParamView view)
        {
            foreach(var v in this.Views)
            {
                if (v == view)
                    continue;

                if(!string.IsNullOrEmpty( v.Depend) && v.Depend == view.Property.Name)
                {
                    object val = view.GetValue();
                    if (val != null)
                    {
                        object dval = v.DependValue;
                        Type t = view.Property.PropertyType;

                        if (string.Equals(val.ToString(), dval.ToString())) // ？？？字符串作为比较的中间值是否靠谱?
                        {
                            v.Visible = true;
                        }
                        else
                        {
                            v.Visible = false;
                        }
                    }
                }
            }
            //view.Property.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var c in this.flowLayoutPanel1.Controls)
            {
                ParamView view = c as ParamView;
                string msg = view.ValidateValues();

                if(!string.IsNullOrEmpty(msg))
                {
                    MessageBox.Show(msg);
                    return;
                }
            }

            foreach (var c in this.flowLayoutPanel1.Controls)
            {
                ParamView view = c as ParamView;
                try
                {
                    view.Save();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            string errMsg = BehaviorConfig.SaveBehaviorConfig(currency, this.behavior);

            if(!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }

            this.OnSave?.Invoke();
            behavior.OnParamaterChanged();
            this.Close();
        }
    }
}
