using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security;
using System.Security.Permissions;



namespace ClipboardViewer
{
    public partial class Config1 : Form
    {
        private StringBuilder buffStartMode = new StringBuilder();
        private StringBuilder buffCloseButton = new StringBuilder();

        Form1 mainForm;

        public Config1(Form1 f)
        {
            mainForm = f;
            InitializeComponent();
            if (property.StartMode.ToString() == "min")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

            if (property.CloseButton.ToString() == "min")
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            property.StartMode = buffStartMode;
            property.CloseButton = buffCloseButton;

            Console.Write("\n\nclose:");
            Console.Write(property.CloseButton.ToString());

            Console.Write("\nstartmode:");
            Console.Write(property.StartMode.ToString());

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                buffStartMode.Clear();
                buffStartMode.Append("min");
            }
            else
            {
                buffStartMode.Clear();
                buffStartMode.Append("normal");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                buffCloseButton.Clear();
                buffCloseButton.Append("min");
            }
            else
            {
                buffCloseButton.Clear();
                buffCloseButton.Append("normal");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ColorDialogクラスのインスタンスを作成
            ColorDialog cd = new ColorDialog();

            //はじめに選択されている色を設定
            cd.Color = BackColor;
            //色の作成部分を表示可能にする
            //デフォルトがTrueのため必要はない
            cd.AllowFullOpen = true;
            //純色だけに制限しない
            //デフォルトがFalseのため必要はない
            cd.SolidColorOnly = false;

            //ダイアログを表示する
            if (cd.ShowDialog() == DialogResult.OK)
            {
                //選択された色の取得
                mainForm.BackColor = cd.Color;
                property.BackColor = cd.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog cd2 = new ColorDialog();
            //cd2.Color = BackColor;
            cd2.AllowFullOpen = true;
            cd2.SolidColorOnly = false;

            if (cd2.ShowDialog() == DialogResult.OK)
            {
                mainForm.textBox1.BackColor = cd2.Color;
                mainForm.textBox2.BackColor = cd2.Color;
                mainForm.textBox3.BackColor = cd2.Color;
                mainForm.textBox4.BackColor = cd2.Color;
                mainForm.textBox5.BackColor = cd2.Color;
                mainForm.textBox6.BackColor = cd2.Color;
                mainForm.textBox7.BackColor = cd2.Color;
                mainForm.textBox8.BackColor = cd2.Color;

                property.TextBoxBackColor = cd2.Color;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(
                "スタートアップに登録しますか？\n" +
                "レジストリに書き込みます。\n\n" +
                "キー = HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\n" +
                "名前 = " + Application.ProductName + "\n" +
                "値 = " + Application.ExecutablePath,
                "スタートアップ登録",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.OK)
            {
                try
                {
                    //レジストリ(スタートアップ)に登録する
                    //Runキーを開く
                    Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                            @"Software\Microsoft\Windows\CurrentVersion\Run");

                    //値の名前に製品名、値のデータに実行ファイルのパスを指定し、書き込む
                    regkey.SetValue(Application.ProductName, Application.ExecutablePath);

                    //閉じる
                    regkey.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(
                        "以下の例外が発生しました\nException: " + ee.Message +"\nレジストリへのアクセス許可がない可能性があります。",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "スタートアップを解除しますか？\n" +
                "レジストリから削除します",
                "スタートアップ解除",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2
            );


            if (result == DialogResult.OK)
            {
                try
                {
                    Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                        @"Software\Microsoft\Windows\CurrentVersion\Run");

                    regkey.DeleteValue(Application.ProductName);

                    regkey.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(
                        "以下の例外が発生しました\nException: " + ee.Message+"\nすでにスタートアップには登録されていない可能性があります。",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );

                }
            }


        }


    }
}
