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
    public partial class Menu_QL : Form
    {
        public Menu_QL()
        {
            InitializeComponent();
            panel_Menu.BackColor = System.Drawing.ColorTranslator.FromHtml("#205072");
            panel_Heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#2c6975");
            //panel_Childform.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");

            button_Exit.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button_Exit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button_Exit.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#205072");

            button4.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button4.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#205072");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void res_btn()
        {
            button4.TextAlign = ContentAlignment.MiddleLeft;
            button4.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            res_btn();
            button4.TextAlign = ContentAlignment.MiddleRight;
            button4.BackColor = System.Drawing.ColorTranslator.FromHtml("#47cacc");
            Form PheDuyetDonDat = new PheDuyetDonDat();
            openChildForm(PheDuyetDonDat);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
        //Button_Exit
        /*private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }*/

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Childform.Controls.Add(childForm);
            panel_Childform.Tag = childForm;
            childForm.Show();
        }
    }
}
