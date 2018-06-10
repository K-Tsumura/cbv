using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardViewer
{
    public partial class KeyConfig : Form
    {
        Form1 mainForm;
        public KeyConfig(Form1 f)
        {
            mainForm = f;
            InitializeComponent();

            if ((property.HotKeyModifiers & 0x01) > 0)
            {
                checkBox1.Checked = true;
            }
            if ((property.HotKeyModifiers & 0x02) > 0)
            {
                checkBox2.Checked = true;
            }
            if ((property.HotKeyModifiers & 0x04) > 0)
            {
                checkBox3.Checked = true;
            }

        }

        private int ALT = 0x01;
        private int CTRL = 0x02;
        private int SHIFT = 0x04;

        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                 DialogResult re =  MessageBox.Show("キーが一つも選択されていません。動作に支障が出る可能性がありますが続行しますか？",
                    "キーが一つも選択されていません。",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Hand,
                    MessageBoxDefaultButton.Button2);

                 if (re == DialogResult.Cancel)
                 {
                     return;
                 }
            }


            property.HotKeyModifiers = 0;

            if (checkBox1.Checked)
            {
                property.HotKeyModifiers |= ALT;
            }
            if(checkBox2.Checked)
            {
                property.HotKeyModifiers |= CTRL;
            }
            if (checkBox3.Checked)
            {
                property.HotKeyModifiers |= SHIFT;
            }

            HotKey.EntryHotKey(mainForm.Handle);

            Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
