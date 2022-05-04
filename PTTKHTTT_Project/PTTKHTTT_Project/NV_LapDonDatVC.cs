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
    public partial class NV_LapDonDatVC : Form
    {
        public NV_LapDonDatVC()
        {
            InitializeComponent();
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
        private void NV_LapDonDatVC_Load(object sender, EventArgs e)
        {
            Form ChonVaccine_Le = new ChonVaccine_Le();
            openChildForm(ChonVaccine_Le);

            textBox2.Text = Menu_NV.MaNV;
            textBox3.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Vui long tao ma");
            }
            if(ChonVaccine_Le.dgv_Le_Public.RowCount == 1)
            {
                MessageBox.Show("Chua chon vaccine");
            }
            else
            {
                //Lap danh sach dat
                string q = "INSERT INTO dsdatvaccine VALUES (@MADS, @NGAYLAP, @MANV, '', '')";

                using (SqlCommand command = new SqlCommand(q, Menu_NV.con))
                {
                    command.Parameters.AddWithValue("@MADS", textBox1.Text);
                    command.Parameters.AddWithValue("@NGAYLAP", textBox3.Text);
                    command.Parameters.AddWithValue("@MANV", textBox2.Text);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                //
                q = "INSERT INTO CT_DSDAT VALUES (@MADS, @MAVC, @SOLUONG)";

                for (int rows = 0; rows < ChonVaccine_Le.dgv_Le_Public.Rows.Count - 1; rows++)
                {
                    using (SqlCommand command = new SqlCommand(q, Menu_NV.con))
                    {
                        command.Parameters.AddWithValue("@MADS", textBox1.Text);
                        command.Parameters.AddWithValue("@MAVC", ChonVaccine_Le.dgv_Le_Public.Rows[rows].Cells[0].Value.ToString());
                        command.Parameters.AddWithValue("@SOLUONG", ChonVaccine_Le.dgv_Le_Public.Rows[rows].Cells[1].Value.ToString());
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                MessageBox.Show("Lap thanh cong");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string q = "select top 1 MADS from dsdatvaccine order by MADS desc";
            string DS = "";
            SqlCommand com = new SqlCommand(q, Menu_NV.con);
            using (SqlDataReader read = com.ExecuteReader())
            {
                while (read.Read())
                {
                    DS = NV_ThanhToan.increment((read["MADS"].ToString()), "DS");
                }
            }

            textBox1.Text = DS;
        }


    }
}
