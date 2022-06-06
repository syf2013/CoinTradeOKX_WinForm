using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX.Control
{
    public partial class WinQrCode : Form
    {
        private long id;
        private string name;
        private string url;
        private bool isLocalFile = false;
        public WinQrCode(long id,string name,string qrCodeUrl)
        {
            InitializeComponent();

            this.name   = name;
            this.id     = id;
            this.url    = qrCodeUrl;
            this.txtName.Text = name;
            var cache = this.GetLocalFileName(id, name, qrCodeUrl);
            if (File.Exists(cache))
            {
                isLocalFile = true;
                this.pictureBox1.LoadAsync(cache);
            }
            else
            {
                this.pictureBox1.LoadAsync(qrCodeUrl);
            }


            this.pictureBox1.LoadCompleted += PictureBox1_LoadCompleted;
        }

        private string GetLocalFileName(long id, string name, string url)
        {
            var dir = Application.StartupPath;

            dir = Path.Combine(dir, "qrcode_cache");

            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return Path.Combine(dir, string.Format("{0}_{1}_{2}.png", id, name, url.GetHashCode())); 
        }

        private void PictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
          if(!isLocalFile)
            {
                this.pictureBox1.Image.Save(this.GetLocalFileName(id,name,url),System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
