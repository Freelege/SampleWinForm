using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using ToolsLibrary;

namespace ABI
{
    public partial class MainForm : Form
    {
        public const int USER = 0x0400;  //Self-defined message
        private string _urlAnalysis;
        private bool _analyzing = false;
        private bool _viewingVMS = false;
        private Point _basePoint;  // the up-left point of first button 
        private int GAPBetweenButtons;

        [DllImport("user32.dll")]
        public static extern void PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case USER + 1:  //程序启动的浏览器关闭
                    //string message = string.Format("收到自己消息的参数:{0},{1}", m.WParam, m.LParam);
                    BrowserClosed();
                    break;

                case USER + 2: // VMS Closed
                    VMSClosed();
                    break;

                default:
                    base.DefWndProc(ref m);//一定要调用基类函数，以便系统处理其它消息。
                    break;
            }
        }

        void BrowserClosed()
        {
            _analyzing = false;
            this.TopMost = true;
            this.Show();
            this.TopMost = false;
        }

        void VMSClosed()
        {
            _viewingVMS = false;
            this.TopMost = true;
            this.Show();
            this.TopMost = false;
        }
        public MainForm()
        {
            InitializeComponent();
            KeepFormScale();
            this.StartPosition = FormStartPosition.CenterScreen;
            //  this.AutoScaleDimensions = new SizeF(96F, 96F);
            // this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            if (_analyzing || _viewingVMS)
            {
                if (MessageBox.Show("Analytic reports or Video Monitoring is still running. Are you sure to exit?", "ABI", MessageBoxButtons.YesNo)
                    == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }
            else
                this.Close();
        }

        private void KeepFormScale()
        {
            float scaleX, scaleY;
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                scaleX = graphics.DpiX / 96;
                scaleY = graphics.DpiY / 96;
            }

            //Set the form size to scaled background picture size
            var image = new Bitmap(Properties.Resources.bg);
            this.Width = (int)(image.Width * scaleX + 0.5);
            this.Height = (int)(image.Height * scaleY + 0.5);
            image.Dispose();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            /*  if (this.CurrentAutoScaleDimensions != this.AutoScaleDimensions)
              {
                  PerformAutoScale();
              } */

            button_Exit.BackColor = Color.Transparent;
            button_Exit.FlatStyle = FlatStyle.Flat;
            button_Exit.FlatAppearance.BorderSize = 0;

            _basePoint = new Point();
            _basePoint.X = (this.Size.Width - pictureBox_VMS.Width) / 2;
            _basePoint.Y = this.Size.Height * 1 / 2;
            GAPBetweenButtons = this.Size.Height * 3 / 20;

            if (GlobalVar.user == "admin")
            {
                pictureBox_VMS.Location = _basePoint;
                pictureBox_VMS.Show();
                pictureBox_Analysis.Location = new Point(_basePoint.X, _basePoint.Y + GAPBetweenButtons);
                pictureBox_Analysis.Show();
                pictureBox_Event.Location = new Point(_basePoint.X, _basePoint.Y + 2 * GAPBetweenButtons);
                pictureBox_Event.Show();
            }
            if (GlobalVar.user == "security")
            {
                pictureBox_VMS.Location = new Point(_basePoint.X, _basePoint.Y + GAPBetweenButtons / 2);
                pictureBox_VMS.Show();
                pictureBox_Analysis.Hide();
                pictureBox_Event.Location = new Point(_basePoint.X, _basePoint.Y + GAPBetweenButtons * 3 / 2);
                pictureBox_Event.Show();
            }
            else if (GlobalVar.user == "marketing")
            {
                pictureBox_VMS.Hide();
                pictureBox_Analysis.Location = new Point(_basePoint.X, _basePoint.Y + GAPBetweenButtons);
                pictureBox_Analysis.Show();
                pictureBox_Event.Hide();
            }

            _urlAnalysis = GlobalVar.theIniFile.IniReadValue("Main", "url");

            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 3000;
            toolTip1.InitialDelay = 800;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button.
            toolTip1.SetToolTip(pictureBox_VMS, "VIDEO MONITORING: Multi site video display");
            toolTip1.SetToolTip(pictureBox_Analysis, "ANALYTIC REPORTS: Business Intelligence analytic reports");
            toolTip1.SetToolTip(pictureBox_Event, "Monitoring centre video verified events");
        }

        private void button_Exit_MouseEnter(object sender, EventArgs e)
        {
            button_Exit.FlatStyle = FlatStyle.Popup;
        }

        private void button_Exit_MouseLeave(object sender, EventArgs e)
        {
            button_Exit.FlatStyle = FlatStyle.Flat;
        }
        private void pictureBox_VMS_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_VMS.Image = Properties.Resources.Video_btn_hover;
        }
        private void pictureBox_VMS_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_VMS.Image = Properties.Resources.Video_btn_up;
        }
        private void pictureBox_VMS_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_VMS.Image = Properties.Resources.Video_btn_click;
        }

        private void pictureBox_VMS_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (_viewingVMS)
                    {
                        MessageBox.Show("VMS is currently running, please close it and try again");
                        return;
                    }

                    this.Hide();
                    //Launch Joinfinity client
                    System.Threading.Thread VMSThread = new System.Threading.Thread(ViewVMSThread);
                    VMSThread.IsBackground = true;
                    VMSThread.Start("ABI");
                    _viewingVMS = true;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void pictureBox_Analysis_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_Analysis.Image = Properties.Resources.Analytic_btn_hover;
        }
        private void pictureBox_Analysis_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_Analysis.Image = Properties.Resources.Analytic_btn_up;
        }
        private void pictureBox_Analysis_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_Analysis.Image = Properties.Resources.Analytic_btn_click;
        }
        private void pictureBox_Analysis_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (_analyzing)
                    {
                        MessageBox.Show("Analytic reports are currently in viewing. Please close it and try again");
                        return;
                    }

                    _urlAnalysis = GlobalVar.theIniFile.IniReadValue("Main", "url");
                    if (_urlAnalysis == "")  //从ini文件中无法读到url信息
                    {
                        MessageBox.Show("ABI.ini file is damaged. Please reinstall the software package");
                        return;
                    }

                    this.Hide();
                    //Go to ACTI website
                    System.Threading.Thread AnalysisThread = new System.Threading.Thread(AnalysisReportThread);
                    AnalysisThread.IsBackground = true;
                    AnalysisThread.SetApartmentState(ApartmentState.STA);
                    AnalysisThread.Start(_urlAnalysis);
                    _analyzing = true;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void pictureBox_Event_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_Event.Image = Properties.Resources.Incident_btn_hover;
        }
        private void pictureBox_Event_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_Event.Image = Properties.Resources.Incident_btn_up;
        }

        private void pictureBox_Event_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_Event.Image = Properties.Resources.Incident_btn_click;
        }

        private void pictureBox_Event_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    this.Hide();
                    EventForm eventForm = new EventForm();
                    eventForm.ShowDialog();
                    this.TopMost = true;
                    this.Show();
                    this.TopMost = false;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }


        private void AnalysisReportThread(object obj)
        {
            try
            {
                string url = obj as string;

                //var browser = System.Diagnostics.Process.Start("iexplore.exe", url);
                //var browser = System.Diagnostics.Process.Start(url);
                //if (browser != null)
                //   browser.WaitForExit(); 
                BrowserForm theBrowser = new BrowserForm(url);
                theBrowser.ShowDialog();
                theBrowser.Dispose();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                //Notify Mainform that the browser closed.
                Invoke((Action)(() =>
                {
                    MainForm.PostMessage(this.Handle, USER + 1, 0, 0);
                }));
            }
        }

        private void ViewVMSThread(object obj)
        {
            try
            {
                string para = obj as string;
                var vms = System.Diagnostics.Process.Start("VMSClient.exe", para);
                vms.WaitForExit();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                //Notify Mainform that VMS closed
                /*
                Action InvokeDelegate;
                InvokeDelegate = () => { MainForm.PostMessage(this.Handle, USER + 2, 0, 0); };
                Invoke(InvokeDelegate);*/
                //上述注释代码可直接写为以下形式
                Invoke((Action)(
                    () =>{
                             MainForm.PostMessage(this.Handle, USER + 2, 0, 0);
                         }));
            }
        }

        private void analyticReportsUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _urlAnalysis = GlobalVar.theIniFile.IniReadValue("Main", "url");
            URLSetupForm dlg = new URLSetupForm(Cursor.Position.X, Cursor.Position.Y, _urlAnalysis);
            dlg.ShowDialog();
        }
    }
}
