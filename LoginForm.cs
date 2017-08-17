using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ToolsLibrary;

namespace ABI
{
    public partial class LoginForm : Form
    {
        private const string _AESPassString = "pleasescrumbleit"; //密钥, 128位
        private const int _failsBetweenTime = 5;   //10次连续输错之后， 5分钟之后才能Login
        private const string _defaultAdminPass = "";
        private const string _defaultSecurityPass = "";
        private const string _defaultMarketingPass = "";

        private Dictionary<string, string> _userInfo;
        private int _errTryCount = 0;
        private DateTime _errExitTime;
        public LoginForm()
        {
            try
            {
                InitializeComponent();
                //copy abi.ini to AppData Folder if necessary
                string iniSourceFileName = System.Environment.CurrentDirectory + "\\ABI.ini";
                string iniDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ABI";
                GlobalVar.iniFileName = iniDirectory +"\\ABI.ini";

                if (!Directory.Exists(iniDirectory))
                    Directory.CreateDirectory(iniDirectory);

                if (!File.Exists(GlobalVar.iniFileName))
                {
                    File.Copy(iniSourceFileName, GlobalVar.iniFileName);
                }
                GlobalVar.theIniFile = new IniFile(GlobalVar.iniFileName);

                _userInfo = new Dictionary<string, string>();
                _errExitTime = new DateTime();

                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox_User.Items.Add("admin");
                comboBox_User.Items.Add("security");
                comboBox_User.Items.Add("marketing");
                comboBox_User.Text = "admin";

                string adminPass = GlobalVar.theIniFile.IniReadValue("User", "admin");
                if (adminPass == "")  //说明是首次登陆，或者admin Pass丢失
                {
                    byte[] encryptBytes = MyEncryption.AESEncrypt(_defaultAdminPass, _AESPassString);
                    //将加密后的密文转换为Base64编码， 以全ASCII字符保存
                    string encryptedPassStr = Convert.ToBase64String(encryptBytes);
                    GlobalVar.theIniFile.IniWriteValue("User", "admin", encryptedPassStr);
                    _userInfo.Add("admin", _defaultAdminPass);
                }
                else
                {
                    byte[] outputb = Convert.FromBase64String(adminPass);
                    byte[] decryptBytes = MyEncryption.AESDecrypt(outputb, _AESPassString);
                    //将解密后的结果转换为pass字符串
                    string pass = Encoding.UTF8.GetString(decryptBytes);
                    pass = pass.Trim('\0');  //去除多余的0
                    _userInfo.Add("admin", pass);
                }

                string securityPass = GlobalVar.theIniFile.IniReadValue("User", "security");
                if (securityPass == "")  
                {
                    byte[] encryptBytes = MyEncryption.AESEncrypt(_defaultSecurityPass, _AESPassString);
                    string encryptedPassStr = Convert.ToBase64String(encryptBytes);
                    GlobalVar.theIniFile.IniWriteValue("User", "security", encryptedPassStr);
                    _userInfo.Add("security", _defaultSecurityPass);
                }
                else
                {
                    byte[] outputb = Convert.FromBase64String(securityPass);
                    byte[] decryptBytes = MyEncryption.AESDecrypt(outputb, _AESPassString);
                    string pass = Encoding.UTF8.GetString(decryptBytes);
                    pass = pass.Trim('\0');  
                    _userInfo.Add("security", pass);
                }

                string marketingPass = GlobalVar.theIniFile.IniReadValue("User", "marketing");
                if (marketingPass == "")  
                {
                    byte[] encryptBytes = MyEncryption.AESEncrypt(_defaultMarketingPass, _AESPassString);
                    string encryptedPassStr = Convert.ToBase64String(encryptBytes);
                    GlobalVar.theIniFile.IniWriteValue("User", "marketing", encryptedPassStr);
                    _userInfo.Add("marketing", _defaultMarketingPass);
                }
                else
                {
                    byte[] outputb = Convert.FromBase64String(marketingPass);
                    byte[] decryptBytes = MyEncryption.AESDecrypt(outputb, _AESPassString);
                    string pass = Encoding.UTF8.GetString(decryptBytes);
                    pass = pass.Trim('\0');  
                    _userInfo.Add("marketing", pass);
                }

                string strErrExitTime = GlobalVar.theIniFile.IniReadValue("Main", "LastErrorExitTime");
                if (strErrExitTime != "")
                    _errExitTime = Convert.ToDateTime(strErrExitTime);
                else
                    _errExitTime = DateTime.Parse("1980/8/29 10:00:00");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            try
            {
                if (_errExitTime.AddMinutes(_failsBetweenTime) > DateTime.Now)
                {
                    MessageBox.Show(String.Format("Less than {0} minutes after last failed try.\r\nPlease try again later."
                                    , _failsBetweenTime));
                    return;
                }

                string user = comboBox_User.SelectedItem.ToString();
                if (textBox_Password.Text == _userInfo[user])
                {
                    //关闭登录窗口，显示主窗口
                    GlobalVar.user = user;
                    this.Close();
                    new System.Threading.Thread(() =>
                    {
                        Application.Run(new MainForm());
                    }).Start();
                }
                else  //密码错
                {
                    if (_errTryCount >= 10)
                    {
                        MessageBox.Show(String.Format("Too many times of error password inputing. \r\nPlease try again {0} minutes later"
                                            ,_failsBetweenTime));
                        _errExitTime = DateTime.Now;
                        GlobalVar.theIniFile.IniWriteValue("Main", "LastErrorExitTime", _errExitTime.ToString());
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong password for selected user, please try again");
                        _errTryCount++;
                        textBox_Password.Focus();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void linkLabel_ChangePass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePassForm form = new ChangePassForm(_userInfo, _AESPassString);
            form.ShowDialog();
        }
    }
}
