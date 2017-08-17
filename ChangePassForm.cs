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
    public partial class ChangePassForm : Form
    {
        private Dictionary<string, string> _userInfo;
        private string _AESPassString;
        public ChangePassForm(Dictionary<string, string> userInfo, string AESPassString)
        {
            InitializeComponent();
            this._userInfo = userInfo;
            this.StartPosition = FormStartPosition.CenterScreen;
            this._AESPassString = AESPassString;
        }

        private void ChangePassForm_Load(object sender, EventArgs e)
        {
            comboBox_User.Items.Add("admin");
            comboBox_User.Items.Add("security");
            comboBox_User.Items.Add("marketing");
            comboBox_User.Text = "admin";
        }
        private void button_Change_Click(object sender, EventArgs e)
        {
            if (textBox_OldPass.Text != _userInfo[comboBox_User.Text])
            {
                MessageBox.Show("Wrong old password for selected user. Please try again");
                textBox_OldPass.Focus();
                return;
            }

            if (textBox_OldPass.Text == textBox_NewPass.Text)
            {
                MessageBox.Show("New password is the same as the old password. Please try again");
                textBox_NewPass.Focus();
                return;
            }

            if (textBox_NewPass.Text != textBox_ConfirmNewPass.Text)
            {
                MessageBox.Show("New password doesn't match re-entered new password. Please try again");
                textBox_NewPass.Focus();
                return;
            }

            //OK, Encrypt and save new password
            byte[] encryptBytes = MyEncryption.AESEncrypt(textBox_NewPass.Text, _AESPassString);
            //将加密后的密文转换为Base64编码， 以全ASCII字符保存
            string encryptedPassStr = Convert.ToBase64String(encryptBytes);
            GlobalVar.theIniFile.IniWriteValue("User", comboBox_User.Text, encryptedPassStr);

            _userInfo[comboBox_User.Text] = textBox_NewPass.Text;
            MessageBox.Show("Password for user " + comboBox_User.Text + " has been successfully changed.");
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
