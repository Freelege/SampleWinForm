using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolsLibrary;

namespace ABI
{
    public partial class URLSetupForm : Form
    {
        private int desiredStartLocationX;
        private int desiredStartLocationY;
        private string _url;
        public URLSetupForm(int x, int y, string url)
        {
            InitializeComponent();
            this.desiredStartLocationX = x;
            this.desiredStartLocationY = y;
            this.AcceptButton = button_Save;
            this._url = url;
        }

        private void URLSetupForm_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(desiredStartLocationX, desiredStartLocationY);
            textBox1.Text = _url;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                char[] rn =  new char[10];
                rn[0] = '\r'; rn[1] = '\n'; rn[2] = ' ';
                string url = textBox1.Text.Trim(rn);

                if (String.IsNullOrEmpty(url) || url.Equals("about:blank")) 
                {
                    MessageBox.Show("url input is invalid");
                    return;
                }
                else
                {
                    if (!url.StartsWith("http://")) 
                        url = "http://" + url;
                    GlobalVar.theIniFile.IniWriteValue("Main", "url", url);
                    MessageBox.Show("The URL is successfully saved");
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
