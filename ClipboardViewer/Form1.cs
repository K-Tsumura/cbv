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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;   // iniファイル読み込み用

namespace ClipboardViewer
{

    public partial class Form1 : Form
    {
        private MyClipboardViewer viewer;

        //iniファイルのパスを設定
        public static string iniFileName = AppDomain.CurrentDomain.BaseDirectory + "conf.ini";

        public Form1()
        {
            viewer = new MyClipboardViewer(this);

            // イベントハンドラを登録
            viewer.ClipboardHandler += this.OnClipBoardChanged;
            InitializeComponent();

            //フォーム類の名前を変更する
            this.notifyIcon1.Text = Program.AppName;
            this.Text = Program.AppName;


            //property変数の初期化
            property.init();
            
            
            // iniファイルから文字列を取得
            iniWrite.GetPrivateProfileString(
                "SETTINGS",                 // セクション名
                "StartMode",                // キー名    
                "normal",                   // 値が取得できなかった場合に返される初期値
                property.StartMode,                             // 格納先
                Convert.ToUInt32(property.StartMode.Capacity),  // 格納先のキャパ
                iniFileName);                                   // iniファイル名

            iniWrite.GetPrivateProfileString(
                "SETTINGS","CloseButton","False",
                property.CloseButton,Convert.ToUInt32(property.StartMode.Capacity),iniFileName);

            iniWrite.GetPrivateProfileString(
                "SETTINGS", "TopMost", "False",
                property.TopMost, Convert.ToUInt32(property.TopMost.Capacity), iniFileName);


            iniWrite.GetPrivateProfileString(
                "SETTINGS", "ExitLogSave", "True",
                property.exitLogSave, Convert.ToUInt32(property.exitLogSave.Capacity), iniFileName);

            StringBuilder buff1 = new StringBuilder(10);

            iniWrite.GetPrivateProfileString(
                "SETTINGS", "PasteByHotKey", "False",
                buff1, 10, iniFileName);

            if (buff1.ToString() == "True" || buff1.ToString() == "true")
            {
                property.PasteByHotKey = true;
            }
            else
            {
                property.PasteByHotKey = false;
            }

            buff1.Clear();
            
            iniWrite.GetPrivateProfileString(
                "COLOR", "BackColor", "f0f0f0",
                buff1, 10, iniFileName);

            property.BackColor = Color.FromArgb(Convert.ToInt32("ff" + buff1.ToString(), 16));



            iniWrite.GetPrivateProfileString(
                "COLOR", "TextBoxBackColor", "ffffff",
                buff1, 10, iniFileName);


            property.TextBoxBackColor = Color.FromArgb(Convert.ToInt32("ff" + buff1.ToString(), 16));


            StringBuilder buff2 = new StringBuilder();

            iniWrite.GetPrivateProfileString(
                "HOTKEY", "Modifers", "2",
                buff2, 10, iniFileName);

            property.HotKeyModifiers = Int32.Parse(buff2.ToString());


            
            if (property.exitLogSave.ToString() == "True")
            {
                for (int i = 0; i < 8; i++)
                {
                    try
                    {
                        val.moji[i] = File.ReadAllText("log" + (i + 1).ToString() + ".txt");
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
            }


            //ホットキーの一括登録
            HotKey.EntryHotKey(this.Handle);

        }

        // クリップボードにテキストがコピーされると呼び出される
        private void OnClipBoardChanged(object sender, ClipboardEventArgs args)
        {

            //改行、スペース、タブを半角スペースに置き換えて表示する
            this.label1.Text = Regex.Replace(args.Text, "(　| |\t|\r\n)", "");

            if (val.ToClipboardButton)
            {
                val.ToClipboardButton = false;
                return;
            }

            for (int j = 0; j < val.moji.Length - 1; j++)
            {
                if (args.Text == val.moji[j])
                {

                    for (int k = j; k < val.moji.Length - 1; k++)
                    {
                        val.moji[k] = val.moji[k + 1];
                    }

                    break;
                }
            }
            for (int i = val.moji.Length-1; i >= 1; i--)
            {
                val.moji[i] = val.moji[i - 1];
            }
            val.moji[0] = args.Text;
            textBoxReDraw();
        }

        public void textBoxReDraw()
        {
            this.textBox1.Text = val.moji[0];
            this.textBox2.Text = val.moji[1];
            this.textBox3.Text = val.moji[2];
            this.textBox4.Text = val.moji[3];
            this.textBox5.Text = val.moji[4];
            this.textBox6.Text = val.moji[5];
            this.textBox7.Text = val.moji[6];
            this.textBox8.Text = val.moji[7];
        }

        //Winメッセージが来ると呼び出される。とりあえず今はホットキーについて使用してる。
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            //ホットキー検出
            if (m.Msg == 0x0312)
            {
                if((int)m.WParam == 0x01){
                    button1_Click(null,null);
                }
                if ((int)m.WParam == 0x02)
                {
                    button2_Click(null, null);
                }
                if ((int)m.WParam == 0x03)
                {
                    button3_Click(null, null);
                }
                if ((int)m.WParam == 0x04)
                {
                    button4_Click(null, null);
                }
                if ((int)m.WParam == 0x05)
                {
                    button5_Click(null, null);
                }
                if ((int)m.WParam == 0x06)
                {
                    button6_Click(null, null);
                }
                if ((int)m.WParam == 0x07)
                {
                    button7_Click(null, null);
                }
                if ((int)m.WParam == 0x08)
                {
                    button8_Click(null, null);
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.textBox1.Text, false);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            val.ToClipboardButton = true;
            Console.WriteLine(val.ToClipboardButton);
            Clipboard.SetDataObject(this.textBox2.Text, false);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            val.ToClipboardButton = true;
            Clipboard.SetDataObject(this.textBox3.Text, false);
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            val.ToClipboardButton = true;
            Clipboard.SetDataObject(this.textBox4.Text, false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            val.ToClipboardButton = true;
            Clipboard.SetDataObject(this.textBox5.Text, false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            val.ToClipboardButton = true;
            Clipboard.SetDataObject(this.textBox6.Text, false);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            val.ToClipboardButton = true;
            Clipboard.SetDataObject(this.textBox7.Text, false);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            val.ToClipboardButton = true;
            Clipboard.SetDataObject(this.textBox8.Text, false);
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                TopMost = true;
                property.TopMost.Clear();
                property.TopMost.Append("True");
            }
            else
            {
                TopMost = false;
                property.TopMost.Clear();
                property.TopMost.Append("False");
            }

        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }
        
        

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
            }
            else
            {
                this.notifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
            }

            //ホットキーの一括登録
            HotKey.EntryHotKey(this.Handle);

        }

        //通知領域アイコンをダブルクリックされたとき
        //ウインドウをもとに戻す
        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }


        private Point mouseposition;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mouseposition = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    this.Left += e.X - mouseposition.X;
                    this.Top += e.Y - mouseposition.Y;
                }
            }
        }

        private void コンパクトにするToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            コンパクトにするToolStripMenuItem.Enabled = false;
        }
        private void 元の大きさに戻すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            元の大きさに戻すToolStripMenuItem.Enabled = false;
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //コンテキストメニューを表示する座標
                System.Drawing.Point p = System.Windows.Forms.Cursor.Position;

                //指定した画面上の座標位置にコンテキストメニューを表示する
                this.contextMenuStrip1.Show(p);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (FormBorderStyle == FormBorderStyle.FixedSingle)
            {
                元の大きさに戻すToolStripMenuItem.Enabled = false;
                コンパクトにするToolStripMenuItem.Enabled = true;
            }
            else
            {
                コンパクトにするToolStripMenuItem.Enabled = false;
                元の大きさに戻すToolStripMenuItem.Enabled = true;
            }
        }

        private void 終了ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //終了する
            終了ToolStripMenuItem_Click(sender, e);
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Form1_MouseClick(sender, e);
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config1 ConfForm1 = new Config1(this);
            ConfForm1.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (property.CloseButton.ToString() == "True")
            {
                //閉じる理由が、ユーザークローズだったら最小化する。
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    this.WindowState = FormWindowState.Minimized;
                }
            }
            else if (property.CloseButton.ToString() == "False")
            {
                //そのまま閉じてもらう
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (property.StartMode.ToString() == "True")
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else if (property.StartMode.ToString() == "False")
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (property.TopMost.ToString() == "True")
            {
                this.TopMost = true;
                this.checkBox1.Checked = true;
            }
            else
            {
                this.TopMost = false;
                this.checkBox1.Checked = false;
            }

            BackColor = property.BackColor;
            
            textBox1.BackColor = property.TextBoxBackColor;
            textBox2.BackColor = property.TextBoxBackColor;
            textBox3.BackColor = property.TextBoxBackColor;
            textBox4.BackColor = property.TextBoxBackColor;
            textBox5.BackColor = property.TextBoxBackColor;
            textBox6.BackColor = property.TextBoxBackColor;
            textBox7.BackColor = property.TextBoxBackColor;
            textBox8.BackColor = property.TextBoxBackColor;
            
        }

        private void 設定ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            設定ToolStripMenuItem_Click(sender, e);
        }


    }

    public class val
    {
        public static string[] moji = new string[9];
        public static bool ToClipboardButton = false;
    }

    public class property
    {
        //normal / min /
        public static StringBuilder StartMode = new StringBuilder();
        //normal / min
        public static StringBuilder CloseButton = new StringBuilder();
        //true / false
        public static StringBuilder TopMost = new StringBuilder();
        //ture / false
        public static StringBuilder exitLogSave = new StringBuilder();

        public static int HotKeyModifiers;


        public static Color BackColor;
        public static Color TextBoxBackColor;
        public static bool PasteByHotKey;


        public static void init(){
            StartMode.Append("normal");
            CloseButton.Append("exit");
            TopMost.Append("false");
            exitLogSave.Append("true");
            BackColor = Color.FromArgb(0xff,0xf0, 0xf0, 0xf0);
            TextBoxBackColor = Color.FromArgb(0xff,0xff, 0xff, 0xff);
            PasteByHotKey = true;
            HotKeyModifiers = 0x02;
        }
    }


}