using CoinTradeOKX.Manager;
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
    public partial class WinTeam : Form
    {
        public WinTeam()
        {
            InitializeComponent();
        }

        private void WinTeam_Load(object sender, EventArgs e)
        {
            this.RefreshList();
        }

        private void RefreshList()
        {
            var members = OTCTeamManager.Instance.GetMembers();

            this.chlMembers.Items.Clear();
            foreach (var m in members)
            {
                this.chlMembers.Items.Add(m.Name);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OTCTeamManager.Instance.AddMember(this.txtName.Text);
                this.RefreshList();

                this.txtName.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach(var item in this.chlMembers.Items)
            {
                if(this.chlMembers.SelectedItems.Contains(item))
                {
                    OTCTeamManager.Instance.RemoveMember(item.ToString());
                }
            }

            this.RefreshList();
        }
    }
}
