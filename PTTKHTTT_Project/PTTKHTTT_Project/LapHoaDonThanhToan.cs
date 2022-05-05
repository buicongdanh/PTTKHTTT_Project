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
    public partial class LapHoaDonThanhToan : Form
    {
        public LapHoaDonThanhToan()
        {
            InitializeComponent();
        }

        private void LapHoaDonThanhToan_Load(object sender, EventArgs e)
        {
            String q = "select * from DONMUAVC";

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_NV.con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String query = "INSERT INTO HOADON VALUES (@MAHD,@HINHTHUC,@SOTIEN, @madon, @dott ,@tinhtrang)";

            using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
            {
                command.Parameters.AddWithValue("@MAHD", textBox2.Text);
                command.Parameters.AddWithValue("@HINHTHUC", textBox6.Text);
                command.Parameters.AddWithValue("@SOTIEN", textBox3.Text);
                command.Parameters.AddWithValue("@madon", textBox4.Text);
                command.Parameters.AddWithValue("@dott", textBox5.Text);
                command.Parameters.AddWithValue("@tinhtrang", "DTT");
                try {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Lap Hoa Don Thanh Cong");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String q = "select * from DONMUAVC";
            if (textBox1.Text.Trim().Length > 0)
            {
                q = q + " WHERE MADON ='" + textBox1.Text + "'";
            }

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_NV.con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string q = "select top 1 MAHD from HOADON order by MAHD desc";
            string MaHD = "";
            SqlCommand com = new SqlCommand(q, Menu_NV.con);
            using (SqlDataReader read = com.ExecuteReader())
            {
                while (read.Read())
                {
                    MaHD = NV_ThanhToan.increment((read["MAHD"].ToString()), "HD");
                    textBox2.Text = MaHD;
                }
            }
        }
    }
}
