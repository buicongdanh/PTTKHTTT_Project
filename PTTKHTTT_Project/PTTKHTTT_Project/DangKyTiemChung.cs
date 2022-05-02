using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PTTKHTTT_Project
{
    public partial class DangKyTiemChung : Form
    {
        public DangKyTiemChung()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                panel_NGH.Visible = true;
            else
                panel_NGH.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Tiêm theo gói")
            {
                Form ChonVaccine_Nhom = new ChonVaccine_Nhom();
                openChildForm(ChonVaccine_Nhom);
            }

            if (comboBox1.Text == "Tiêm lẻ")
            {
                Form ChonVaccine_Le = new ChonVaccine_Le();
                openChildForm(ChonVaccine_Le);
            }
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_ChonVC.Controls.Add(childForm);
            panel_ChonVC.Tag = childForm;
            childForm.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime start = monthCalendar1.SelectionRange.Start;
            string formattedStart = start.ToString("yyyy-MM-dd");
            textBox_Date.Text = formattedStart;
        }

        private void textBox_Date_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }

        private void textBox_Date_Leave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void DangKyTiemChung_Load(object sender, EventArgs e)
        {
            String q = "select HOTEN, DIACHI, SDT from KHACHHANG WHERE MAKH ='" + Menu_KH.MaKH + "'";

            SqlCommand cmd = new SqlCommand(q);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection = Menu_KH.con;
            try
            {
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                DataTable table = dataset.Tables[0]; // Get the data table.
                textBox2.Text = Menu_KH.MaKH;
                textBox3.Text = table.Rows[0].ItemArray[0].ToString();
                textBox4.Text = table.Rows[0].ItemArray[1].ToString();
                textBox5.Text = table.Rows[0].ItemArray[2].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Execute SQL Proc
        }
    }
}
