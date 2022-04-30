using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTKHTTT_Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            panel_Title.BackColor = System.Drawing.ColorTranslator.FromHtml("#2c6975");
            label_Exit.BackColor = System.Drawing.ColorTranslator.FromHtml("#E45046");
            label_Exit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#205072");
            button1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button1.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            
        }

        private void label_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "Username")
                textBox1.Clear();
            textBox1.TextAlign = HorizontalAlignment.Left;
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Username";
                textBox1.ForeColor = Color.Gray;
            }
            textBox1.TextAlign = HorizontalAlignment.Center;
        }
    }
}
