using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace WindowsForms
{
    public partial class Form3 : Form
    {
        CefSharp.WinForms.ChromiumWebBrowser webCom = null;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            webCom = new CefSharp.WinForms.ChromiumWebBrowser("www.baidu.com");
            webCom.Dock = DockStyle.Fill;
            panel1.Controls.Add(webCom);
            webCom.Load("www.baidu.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            webCom = new CefSharp.WinForms.ChromiumWebBrowser(txtUrl.Text);
            webCom.Dock = DockStyle.Fill;
            panel1.Controls.Add(webCom);
            webCom.Load(txtUrl.Text);
        }
    }
}
