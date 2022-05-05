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
    public partial class KH_ThanhToan : Form
    {
        public KH_ThanhToan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String query = "INSERT INTO HOADON VALUES('@MAHD', '@HINHTHUC', @SOTIEN, '@MADON', @DOTT, 'DTT')";
            
            using (SqlCommand command = new SqlCommand(query, Menu_KH.con))
            {
                string temp = textBox2.Text;
                command.Parameters.AddWithValue("@MAHD", temp);
                temp = textBox6.Text;
                command.Parameters.AddWithValue("@HINHTHUC", temp);
                temp = textBox3.Text;
                command.Parameters.AddWithValue("@SOTIEN", Convert.ToDouble(temp));
                temp = comboBox1.Text;
                command.Parameters.AddWithValue("@MADON", temp);
                temp = textBox4.Text;
                command.Parameters.AddWithValue("@DOTT", int.Parse(temp));
                
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thanh Toan Hoa Don Thanh Cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string q = "SELECT HD.* " +
                "FROM HOADON HD JOIN DONMUAVC DMVC ON (HD.MADON = DMVC.MADON) " +
                "JOIN KHACHHANG KH ON (DMVC.MAKH = KH.MAKH)" +
                "WHERE KH.MAKH ='" + Menu_KH.MaKH + "'";

            if (textBox1.Text.Trim().Length > 0)
            {
                q = q + " AND HD.MAHD ='" + textBox1.Text + "'";
            }
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_KH.con);
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

        private void KH_ThanhToan_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //Load value for combo box
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("Select MaDon FROM DONMUAVC WHERE MAKH = '" + Menu_KH.MaKH + "'", Menu_KH.con);
                DataSet ds1 = new DataSet();
                adp.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    comboBox1.DataSource = ds1.Tables[0];
                    comboBox1.DisplayMember = "MaDon";
                    comboBox1.ValueMember = "Madon";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //String q = "SELECT HD.MAHD,DM.MADON,DM.MAKH,DM.NGAYDATMUA,DM.TONGTIEN, HD.tinhtrang, HD.DoTT FROM HOADON HD JOIN DONMUAVC DM ON HD.madon = DM.MADON";
            string q = "SELECT HD.* " +
                "FROM HOADON HD JOIN DONMUAVC DMVC ON (HD.MADON = DMVC.MADON) " +
                "JOIN KHACHHANG KH ON (DMVC.MAKH = KH.MAKH)" +
                "WHERE KH.MAKH ='" + Menu_KH.MaKH + "'";

            if (textBox1.Text.Trim().Length > 0)
            {
                q = q + " AND DM.MADON ='" + textBox1.Text + "'";
            }
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_KH.con);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaDon = dataGridView1.Rows[e.RowIndex].Cells["madon"].Value.ToString();
            String q = "select vaccine.tenvaccine, ct_dmvc.soluong, ct_dmvc.gia " +
                "from ct_dmvc, vaccine " +
                "where madon = '" + MaDon + "' and vaccine.mavc = ct_dmvc.mavc";

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_KH.con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView2.DataSource = ds.Tables[0];
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
            SqlCommand com = new SqlCommand(q, Menu_KH.con);
            using (SqlDataReader read = com.ExecuteReader())
            {
                while (read.Read())
                {
                    MaHD = NV_ThanhToan.increment((read["MAHD"].ToString()), "HD");
                }
            }
            textBox2.Text = MaHD;
        }
    }
}
