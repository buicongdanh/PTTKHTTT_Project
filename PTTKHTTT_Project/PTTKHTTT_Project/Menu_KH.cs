﻿using System;
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
    public partial class Menu_KH : Form
    {
        public Menu_KH()
        {
            InitializeComponent();
            panel_Menu.BackColor = System.Drawing.ColorTranslator.FromHtml("#205072");
            panel_Heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#2c6975");
            //panel_Childform.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button1.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button1.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#205072");
            button_Exit.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button_Exit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button_Exit.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#205072");
            button3.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button3.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#205072");
            button4.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button4.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button4.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#205072");
            button2.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button2.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#205072");
            button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button5.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button5.FlatAppearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#205072");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void res_btn()
        {
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button3.TextAlign = ContentAlignment.MiddleLeft;
            button3.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button4.TextAlign = ContentAlignment.MiddleLeft;
            button4.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            button5.TextAlign = ContentAlignment.MiddleLeft;
            button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            res_btn();
            button4.TextAlign = ContentAlignment.MiddleRight;
            button4.BackColor = System.Drawing.ColorTranslator.FromHtml("#47cacc");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            res_btn();
            button3.TextAlign = ContentAlignment.MiddleRight;
            button3.BackColor = System.Drawing.ColorTranslator.FromHtml("#47cacc");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            res_btn();
            button5.TextAlign = ContentAlignment.MiddleRight;
            button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#47cacc");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            res_btn();
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.BackColor = System.Drawing.ColorTranslator.FromHtml("#47cacc");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            res_btn();
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.BackColor = System.Drawing.ColorTranslator.FromHtml("#47cacc");
        }
        //Button_Exit
        /*private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }*/
    }
}
