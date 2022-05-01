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
                    MessageBox.Show("Error Create Hoa Don");
                }
                


            }
        }
    }
}
