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
    public partial class Config1 : Form
    {
        private StringBuilder buffStartMode = new StringBuilder();
        private StringBuilder buffCloseButton = new StringBuilder();


        public Config1()
        {
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


    }
}
