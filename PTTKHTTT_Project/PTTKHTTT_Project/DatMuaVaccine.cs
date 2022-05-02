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
    public partial class DatMuaVaccine : Form
    {
        public string MaKH;
        public DatMuaVaccine()
        {
            InitializeComponent();
        }

        private void DatMuaVaccine_Load(object sender, EventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Mua theo gói")
            {
                Form ChonVaccine_Nhom = new ChonVaccine_Nhom();
                openChildForm(ChonVaccine_Nhom);
            }

            if (comboBox1.Text == "Mua lẻ")
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
            panel_Childform.Controls.Add(childForm);
            panel_Childform.Tag = childForm;
            childForm.Show();
        }
    }
}
