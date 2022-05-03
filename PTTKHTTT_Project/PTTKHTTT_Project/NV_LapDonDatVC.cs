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

        private void NV_LapDonDatVC_Load(object sender, EventArgs e)
        {
            string q = "select * from DSDATVACCINE where Duyet = 1;";
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
            string query = "INSERT INTO DONMUAVC VALUES" +
                "(@MADON, @MAKH, @NGAYDATMUA, @TONGTIEN)";

            using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
            {
                //Them PHIEUDK
                command.Parameters.AddWithValue("@MADON", textBox2.Text);
                command.Parameters.AddWithValue("@MAKH", textBox2.Text);
                command.Parameters.AddWithValue("@NGAYDATMUA", textBox3.Text);
                command.Parameters.AddWithValue("@TONGTIEN", textBox4.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Lap Hoa Don Thanh Cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //foreach
            {
                query = "";
                query = "INSERT INTO CT_DMVC VALUES (@MADON, @MAVC, @SOLUONG, @GIA";
                using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
                {
                    command.Parameters.AddWithValue("@MADON", textBox2.Text);
                    command.Parameters.AddWithValue("@MAVC", textBox2.Text);
                    command.Parameters.AddWithValue("@SOLUONG", textBox3.Text);
                    command.Parameters.AddWithValue("@GIA", textBox3.Text);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Lap Hoa Don Thanh Cong");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            //update du lieu [SLT] trong [Vaccine]
            //su dung sp USP_Vaccine_Add
        }
    }
}
