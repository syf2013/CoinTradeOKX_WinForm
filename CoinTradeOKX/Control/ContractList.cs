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
using CoinTradeOKX.Okex;
using CoinTradeOKX.Util;
using CoinTradeOKX.Okex.Const;
using System.IO;

namespace CoinTradeOKX.Control
{
    public partial class ContractList : UserControl,IDisposable
    {

        private bool _IsGlobalView = true;
        public bool IsGlobalView
        {
            get { return _IsGlobalView; }
            set { _IsGlobalView = value; }
        }
        private object locker = new object();
        OTCContractManager manager = null;

        OTCContractMonitor monitor = null;

        public ContractList()
        {
            InitializeComponent();

            if (IsGlobalView)
            {
                manager = OTCContractManager.Instance;
                manager.AddNewContractListener(this.OnNewContract);
                this.OnNewContract(null);
            }
            else
            {
                this.monitor = new OTCContractMonitor(string.Empty);
                monitor.OnData += Monitor_OnData;
                monitor.Interval = 900;
            }
        }

        private void RefreshContract()
        {
            this.BeginContract();
            if (this.manager != null)
            {
                this.manager.EachContract(string.Empty, EachContract);
            }
            
            if(this.monitor != null)
            {
                this.monitor.EachContract(EachContract);
            }
            this.EndContract();
        }

        private void OnNewContract(OTCContract contract)
        {
            this.RefreshContract();
        }

        private void Monitor_OnData(MonitorBase obj)
        {

            this.RefreshContract();
        }

        List<ContractView> views = new List<ContractView>();
        Queue<ContractView> pool = new Queue<ContractView>();

        public void BeginContract()
        {
            views.Clear();

            foreach(System.Windows.Forms.Control c in this.flowLayoutPanel1.Controls)
            {
                views.Add(c as ContractView);
               // c.Visible = false;
            }
        }

        public void EachContract(OTCContract contract)
        {
            bool find = false;
           
            foreach(System.Windows.Forms.Control c in this.flowLayoutPanel1.Controls)
            {
                var cv = c as ContractView;
                 if(cv.Contract.PublicOrderId == contract.PublicOrderId)
                {
                    find = true;
                    cv.Contract = contract;
                    cv.Visible = true;
                    views.Remove(cv);
                    break;
                }
            }

            if (!find)
            {
                var item = pool.Count > 0 ? pool.Dequeue() : new ContractView();
                item.OnContractComplete += Item_OnContractComplete;
                item.Contract = contract;
                item.Visible = true;
                this.flowLayoutPanel1.Controls.Add(item);

                if (contract.Side == Side.Sell)
                {
                    //PlayNewOrderSound();
                }
            }
        }

        public void RemoveContractViewByContract(long contract)
        {
            foreach (System.Windows.Forms.Control c in this.flowLayoutPanel1.Controls)
            {
                var cv = c as ContractView;
                if (cv.ContractId == contract)
                {
                    PushToPool(cv);
                    break;
                }
            }

            if(this.manager != null)
            {
                
            }
        }

        private void Item_OnContractComplete(ContractView obj)
        {
            
        }

        public static void PlayNewOrderSound()
        {
            string path =  Path.Combine(Application.StartupPath ,"Sounds", "new_order.mp3");
            SoundUtil.Play(path);
        }

        public void PushToPool(ContractView view)
        {
            view.Parent = null;
            view.Visible = false;
            view.OnContractComplete -= this.Item_OnContractComplete;
            pool.Enqueue(view);
        }

        public void EndContract()
        {
            foreach(var c in views)
            {
                c.Visible = false;
                PushToPool(c);
            }
        }

        public void Destroy()
        {
            if (manager != null)
            {
                manager.RemoveNewContractListner(this.OnNewContract);
            }

            if(this.monitor != null)
            {
                this.monitor.OnData -= this.Monitor_OnData;
                this.monitor.Destory();
            }

            this.timer1.Enabled = false;
        }

        public void EachContract(Action<OTCContract> callback)
        {
            if(IsGlobalView)
            {
                manager.EachContract(string.Empty, callback);
            }

            else if(monitor !=null)
            {
                monitor.EachContract(callback);
            }
        }


        private void ContractList_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.monitor != null)
            {
                this.monitor.Update(this.timer1.Interval);
            }
           
            this.RefreshContract();
        }
    }
}
