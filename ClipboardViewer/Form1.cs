﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;
using System.Text.RegularExpressions;

namespace ClipboardViewer
{
    public partial class Form1 : Form
    {
        private MyClipboardViewer viewer;

        public Form1()
        {
            viewer = new MyClipboardViewer(this);

            // イベントハンドラを登録
            viewer.ClipboardHandler += this.OnClipBoardChanged;
            InitializeComponent();
        }

        // クリップボードにテキストがコピーされると呼び出される
        private void OnClipBoardChanged(object sender, ClipboardEventArgs args)
        {

            //Console.WriteLine("OnClipboardChanged:" + val.ToClipboardButton);

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
            }
            else
            {
                TopMost = false;
            }

        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }
        
        
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
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




        

    }

    public class val
    {
        public static string[] moji = new string[9];
        public static bool ToClipboardButton = false;
    }

}